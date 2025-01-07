using Microsoft.CSharp;
using Mono.Cecil;
using System.CodeDom;
using Mono.Collections.Generic;

namespace PublicApiGenerator;

internal static partial class CecilEx
{
    public static IEnumerable<IMemberDefinition> GetMembers(this TypeDefinition type)
    {
        return type.Fields.Cast<IMemberDefinition>()
            .Concat(type.Methods)
            .Concat(type.Properties)
            .Concat(type.Events);
    }

    public static bool IsUnsafeSignatureType(this TypeReference typeReference)
    {
        while (true)
        {
            if (typeReference.IsPointer)
                return true;

            if (typeReference.IsArray || typeReference.IsByReference)
            {
                typeReference = typeReference.GetElementType();
                continue;
            }

            return false;
        }
    }

    public static bool IsVolatile(this TypeReference typeReference)
    {
        return typeReference is RequiredModifierType modType && modType.ModifierType.FullName == "System.Runtime.CompilerServices.IsVolatile";
    }

    public static bool IsUnmanaged(this TypeReference typeReference)
    {
        return typeReference is RequiredModifierType modType && modType.ModifierType.FullName == "System.Runtime.InteropServices.UnmanagedType";
    }

    public static bool IsExtensionMethod(this ICustomAttributeProvider method)
    {
        return method.CustomAttributes.Any(a => a.AttributeType.FullName == "System.Runtime.CompilerServices.ExtensionAttribute");
    }

    public static bool IsDelegate(this TypeDefinition publicType)
    {
        return publicType.BaseType != null && publicType.BaseType.FullName == "System.MulticastDelegate";
    }

    public static bool IsCompilerGenerated(this IMemberDefinition m)
    {
        return m.CustomAttributes.Any(a => a.AttributeType.FullName == "System.Runtime.CompilerServices.CompilerGeneratedAttribute");
    }

    public static MemberAttributes CombineAccessorAttributes(MemberAttributes first, MemberAttributes second)
    {
        MemberAttributes access = 0;
        var firstAccess = first & MemberAttributes.AccessMask;
        var secondAccess = second & MemberAttributes.AccessMask;
        if (firstAccess == MemberAttributes.Public || secondAccess == MemberAttributes.Public)
            access = MemberAttributes.Public;
        else if (firstAccess == MemberAttributes.Family || secondAccess == MemberAttributes.Family)
            access = MemberAttributes.Family;
        else if (firstAccess == MemberAttributes.FamilyAndAssembly || secondAccess == MemberAttributes.FamilyAndAssembly)
            access = MemberAttributes.FamilyAndAssembly;
        else if (firstAccess == MemberAttributes.FamilyOrAssembly || secondAccess == MemberAttributes.FamilyOrAssembly)
            access = MemberAttributes.FamilyOrAssembly;
        else if (firstAccess == MemberAttributes.Assembly || secondAccess == MemberAttributes.Assembly)
            access = MemberAttributes.Assembly;
        else if (firstAccess == MemberAttributes.Private || secondAccess == MemberAttributes.Private)
            access = MemberAttributes.Private;

        // Scope should be the same for getter and setter. If one isn't specified, it'll be 0
        var firstScope = first & MemberAttributes.ScopeMask;
        var secondScope = second & MemberAttributes.ScopeMask;
        var scope = (MemberAttributes)Math.Max((int)firstScope, (int)secondScope);

        // Vtable should be the same for getter and setter. If one isn't specified, it'll be 0
        var firstVtable = first & MemberAttributes.VTableMask;
        var secondVtable = second & MemberAttributes.VTableMask;
        var vtable = (MemberAttributes)Math.Max((int)firstVtable, (int)secondVtable);

        return access | scope | vtable;
    }

    public static MemberAttributes GetMethodAttributes(this MethodDefinition method)
    {
        MemberAttributes access = 0;
        if (method.IsFamily || method.IsFamilyOrAssembly)
            access = MemberAttributes.Family;
        if (method.IsPublic)
            access = MemberAttributes.Public;
        if (method.IsAssembly)
            access = MemberAttributes.Assembly;

        MemberAttributes scope = 0;
        if (method.IsStatic)
            scope |= MemberAttributes.Static;
        if (method.IsFinal || !method.IsVirtual)
            scope |= MemberAttributes.Final;
        if (method.IsAbstract)
            scope |= MemberAttributes.Abstract;
        if (method.IsVirtual && !method.IsNewSlot)
            scope |= MemberAttributes.Override;

        MemberAttributes vtable = 0;
        if (IsHidingMethod(method))
            vtable = MemberAttributes.New;

        return access | scope | vtable;
    }

    private static bool IsHidingMethod(MethodDefinition method)
    {
        var typeDefinition = method.DeclaringType;

        // If we're an interface, just try and find any method with the same signature
        // in any of the interfaces that we implement
        if (typeDefinition.IsInterface)
        {
            var interfaceMethods = from @interfaceReference in typeDefinition.Interfaces
                                   let interfaceDefinition = interfaceReference.InterfaceType.Resolve()
                                   where interfaceDefinition != null
                                   select interfaceDefinition.Methods;

            return interfaceMethods.Any(ms => MetadataResolver.GetMethod(ms, method) != null);
        }

        // If we're not an interface, find a base method that isn't virtual
        // https://github.com/PublicApiGenerator/PublicApiGenerator/pull/226#issuecomment-873645565
        // return !method.IsVirtual && GetBaseTypes(typeDefinition).Any(d => MetadataResolver.GetMethod(d.Methods, method) != null);
        return !method.IsVirtual && GetBaseTypes(typeDefinition).Any(d => GetMethodIgnoringReturnType(d.Methods, method) != null);
    }

    private static IEnumerable<TypeDefinition> GetBaseTypes(TypeDefinition type)
    {
        var baseType = type.BaseType;
        while (baseType != null)
        {
            var definition = baseType.Resolve();
            if (definition == null)
                yield break;
            yield return definition;

            baseType = baseType.DeclaringType;
        }
    }

    public static CodeTypeReference MakeReadonly(this CodeTypeReference typeReference) => ModifyCodeTypeReference(typeReference, "readonly");

    public static CodeTypeReference MakeUnsafe(this CodeTypeReference typeReference) => ModifyCodeTypeReference(typeReference, "unsafe");

    public static CodeTypeReference MakeVolatile(this CodeTypeReference typeReference) => ModifyCodeTypeReference(typeReference, "volatile");

    public static CodeTypeReference MakeThis(this CodeTypeReference typeReference) => ModifyCodeTypeReference(typeReference, "this");

    public static CodeTypeReference MakeReturn(this CodeTypeReference typeReference) => ModifyCodeTypeReference(typeReference, "return:");

    public static CodeTypeReference MakeGet(this CodeTypeReference typeReference) => ModifyCodeTypeReference(typeReference, "get:");

    public static CodeTypeReference MakeSet(this CodeTypeReference typeReference) => ModifyCodeTypeReference(typeReference, "set:");

    public static CodeTypeReference MakeIn(this CodeTypeReference typeReference) => ModifyCodeTypeReference(typeReference, "in");

    private static CodeTypeReference ModifyCodeTypeReference(CodeTypeReference typeReference, string modifier)
    {
        using var provider = new CSharpCodeProvider();
        return typeReference.TypeArguments.Count == 0
            // For types without generic arguments we resolve the output type directly to turn System.String into string
            ? new CodeTypeReference(modifier + " " + provider.GetTypeOutput(typeReference))
            // For types with generic types the BaseType is GenericType`<Arity>. Then we cannot resolve the output type and need to pass on the BaseType
            // to avoid falling into hardcoded assumptions in CodeTypeReference that cuts of the type after the 4th comma. i.ex. readonly Func<string, string, string, string>
            // works but readonly Func<string, string, string, string, string> would turn into readonly Func<string
            : new CodeTypeReference(modifier + " " + typeReference.BaseType, typeReference.TypeArguments.Cast<CodeTypeReference>().ToArray());
    }

    internal static bool? IsNew<TDefinition>(this TDefinition methodDefinition, Func<TypeDefinition, Collection<TDefinition>?> selector, Func<TDefinition, bool> predicate)
        where TDefinition : IMemberDefinition
    {
        bool? isNew = null;
        var baseType = methodDefinition.DeclaringType.BaseType;
        while (baseType is TypeDefinition typeDef)
        {
            isNew = selector(typeDef).Any(predicate);
            if (isNew is true)
            {
                break;
            }

            baseType = typeDef.BaseType;
        }

        return isNew;
    }

    internal sealed class ParameterTypeComparer : IEqualityComparer<ParameterDefinition>
    {
        public static ParameterTypeComparer Instance = new();

        public bool Equals(ParameterDefinition x, ParameterDefinition y) => x?.ParameterType == y?.ParameterType;

        public int GetHashCode(ParameterDefinition obj) => obj.GetHashCode();
    }
}
