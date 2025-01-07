using System.CodeDom;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.CSharp;
using Mono.Cecil;
using Mono.Cecil.Rocks;
using ICustomAttributeProvider = Mono.Cecil.ICustomAttributeProvider;
using TypeAttributes = System.Reflection.TypeAttributes;

namespace PublicApiGenerator;

/// <summary>
/// Extensions methods to generate a public API from provided assemblies or types.
/// </summary>
public static class ApiGenerator
{
    /// <summary>
    /// Generates a public API from the specified assembly.
    /// </summary>
    /// <param name="assembly">The assembly to generate an API from.</param>
    /// <param name="options">The options to control the API output.</param>
    /// <returns>The API output.</returns>
    public static string GeneratePublicApi(this Assembly assembly, ApiGeneratorOptions? options = null)
    {
        options ??= new ApiGeneratorOptions();

        using (var assemblyResolver = new DefaultAssemblyResolver())
        {
            var assemblyPath = assembly.Location;
            assemblyResolver.AddSearchDirectory(Path.GetDirectoryName(assemblyPath));
            assemblyResolver.AddSearchDirectory(AppDomain.CurrentDomain.BaseDirectory);

            var readSymbols = File.Exists(Path.ChangeExtension(assemblyPath, ".pdb"));
            using (var asm = AssemblyDefinition.ReadAssembly(assemblyPath, new ReaderParameters(ReadingMode.Deferred)
            {
                ReadSymbols = readSymbols,
                AssemblyResolver = assemblyResolver
            }))
            {
                return CreatePublicApiForAssembly(
                    asm,
                    options,
                    typeDefinition => !typeDefinition.IsNested &&
                                      (options.ExcludeTypes == null || !options.ExcludeTypes.Any(type => type.FullName == typeDefinition.FullName && type.Assembly.FullName == typeDefinition.Module.Assembly.FullName)) &&
                                      ShouldIncludeType(typeDefinition, options.DenyNamespacePrefixes, options.AllowNamespacePrefixes, options.UseDenyNamespacePrefixesForExtensionMethods) &&
                                      (options.IncludeTypes == null || options.IncludeTypes.Any(type => type.FullName == typeDefinition.FullName && type.Assembly.FullName == typeDefinition.Module.Assembly.FullName))
                );
            }
        }
    }

    /// <summary>
    /// Generates a public API from the specified types.
    /// </summary>
    /// <param name="types">The types to generate an API from.</param>
    /// <param name="options">The options to control the API output.</param>
    /// <remarks>This method assumes all the types belong to the same assembly. The assembly of the first type <c>types[0].Assembly</c> is used.</remarks>
    /// <returns>The API output.</returns>
    public static string GeneratePublicApi(this Type[] types, ApiGeneratorOptions? options = null)
    {
        if (types.Length == 0)
        {
            return string.Empty;
        }
        (options ??= new ApiGeneratorOptions()).IncludeTypes = types;
        return types[0].Assembly.GeneratePublicApi(options);
    }

    /// <summary>
    /// Generates a public API from the specified type.
    /// </summary>
    /// <param name="type">The type to generate an API from.</param>
    /// <param name="options">The options to control the API output.</param>
    /// <returns>The API output.</returns>
    public static string GeneratePublicApi(this Type type, ApiGeneratorOptions? options = null)
    {
        (options ??= new ApiGeneratorOptions()).IncludeTypes = [type];
        return type.Assembly.GeneratePublicApi(options);
    }

    // TODO: Assembly references?
    // TODO: Better handle namespaces - using statements? - requires non-qualified type names
    private static string CreatePublicApiForAssembly(AssemblyDefinition assembly, ApiGeneratorOptions options, Func<TypeDefinition, bool> shouldIncludeType)
    {
        var attributeFilter = new AttributeFilter(options.ExcludeAttributes);

        var compileUnit = new CodeCompileUnit();
        if (options.IncludeAssemblyAttributes && assembly.HasCustomAttributes)
        {
            PopulateCustomAttributes(assembly, compileUnit.AssemblyCustomAttributes, attributeFilter);
        }

        var publicTypes = assembly.Modules.SelectMany(m => m.GetTypes())
            .Where(shouldIncludeType)
            .OrderBy(t => t, options.TypeComparer);
        foreach (var publicType in publicTypes)
        {
            var @namespace = compileUnit.Namespaces.Cast<CodeNamespace>().FirstOrDefault(n => n.Name == publicType.Namespace);
            if (@namespace == null)
            {
                @namespace = new CodeNamespace(publicType.Namespace);
                compileUnit.Namespaces.Add(@namespace);
            }

            using (NullableContext.Push(publicType))
            {
                var typeDeclaration = CreateTypeDeclaration(publicType, options.DenyNamespacePrefixes, options.AllowNamespacePrefixes, options.UseDenyNamespacePrefixesForExtensionMethods, attributeFilter);
                @namespace.Types.Add(typeDeclaration);
            }
        }

        using (var writer = new StringWriter())
        {
            var cgo = new CodeGeneratorOptions
            {
                BracingStyle = options.BracingStyle,
                BlankLinesBetweenMembers = false,
                VerbatimOrder = false,
                IndentString = options.IndentString
            };

            new CSharpCodeGenerator(options).GenerateCodeFromCompileUnit(compileUnit, writer, cgo);
            return CodeNormalizer.NormalizeGeneratedCode(writer);
        }
    }

    private static bool ShouldIncludeType(TypeDefinition t, string[] denyNamespacePrefixes, string[] allowNamespacePrefixes, bool useDenyNamespacePrefixesForExtensionMethods)
    {
        if (t.IsCompilerGenerated())
            return false;

        if (!t.IsPublic && !t.IsNestedPublic && !t.IsNestedFamily && !t.IsNestedFamilyOrAssembly)
            return false;

        if (!useDenyNamespacePrefixesForExtensionMethods)
        {
            if (t.GetMembers().Any(m => ShouldIncludeMember(m, denyNamespacePrefixes, allowNamespacePrefixes, useDenyNamespacePrefixesForExtensionMethods)))
                return true;
        }

        if (denyNamespacePrefixes.Any(t.FullName.StartsWith) && !allowNamespacePrefixes.Any(t.FullName.StartsWith))
            return false;

        return true;
    }

    private static bool ShouldIncludeMember(IMemberDefinition m, string[] denyNamespacePrefixes, string[] allowNamespacePrefixes, bool useDenyNamespacePrefixesForExtensionMethods)
    {
        // https://github.com/PublicApiGenerator/PublicApiGenerator/issues/245
        bool isRecord = m.DeclaringType.GetMethods().Any(m => m.Name == "<Clone>$");
        if (isRecord && m.Name == "EqualityContract")
            return false;

        if (m.IsCompilerGenerated())
            return false;

        if (m is FieldDefinition)
            return false;

        if (m.DeclaringType?.FullName == null)
            return false;

        if (!useDenyNamespacePrefixesForExtensionMethods && m is MethodDefinition md && md.IsExtensionMethod())
            return true;

        if (denyNamespacePrefixes.Any(m.DeclaringType.FullName.StartsWith) && !allowNamespacePrefixes.Any(m.DeclaringType.FullName.StartsWith))
            return false;

        return true;
    }

    private static bool ShouldIncludeMember(MemberAttributes memberAttributes)
        => (memberAttributes & MemberAttributes.AccessMask) switch
        {
            // 0 represents no CodeDOM keyword being specified.
            0 or MemberAttributes.Private or MemberAttributes.Assembly or MemberAttributes.FamilyAndAssembly => false,
            _ => true,
        };

    private static void AddMemberToTypeDeclaration(CodeTypeDeclaration typeDeclaration,
        IMemberDefinition typeDeclarationInfo,
        IMemberDefinition memberInfo,
        AttributeFilter attributeFilter)
    {
        using (NullableContext.Push(memberInfo))
        {
            if (memberInfo is MethodDefinition methodDefinition)
            {
                if (methodDefinition.IsConstructor)
                    AddCtorToTypeDeclaration(typeDeclaration, methodDefinition, attributeFilter);
                else
                    AddMethodToTypeDeclaration(typeDeclaration, methodDefinition, attributeFilter);
            }
            else if (memberInfo is PropertyDefinition propertyDefinition)
            {
                AddPropertyToTypeDeclaration(typeDeclaration, typeDeclarationInfo, propertyDefinition, attributeFilter);
            }
            else if (memberInfo is EventDefinition eventDefinition)
            {
                AddEventToTypeDeclaration(typeDeclaration, eventDefinition, attributeFilter);
            }
            else if (memberInfo is FieldDefinition fieldDefinition)
            {
                AddFieldToTypeDeclaration(typeDeclaration, fieldDefinition, attributeFilter);
            }
        }
    }

    private static CodeTypeDeclaration CreateTypeDeclaration(TypeDefinition publicType, string[] denyNamespacePrefixes, string[] allowNamespacePrefixes, bool useDenyNamespacePrefixesForExtensionMethods, AttributeFilter attributeFilter)
    {
        if (publicType.IsDelegate())
            return CreateDelegateDeclaration(publicType, attributeFilter);

        var @static = false;
        TypeAttributes attributes = 0;
        if (publicType.IsPublic || publicType.IsNestedPublic)
            attributes |= TypeAttributes.Public;
        if (publicType.IsNestedFamily || publicType.IsNestedFamilyOrAssembly)
            attributes |= TypeAttributes.NestedFamily;
        if (publicType.IsSealed && !publicType.IsAbstract)
            attributes |= TypeAttributes.Sealed;
        else if (!publicType.IsSealed && publicType.IsAbstract && !publicType.IsInterface)
            attributes |= TypeAttributes.Abstract;
        else if (publicType.IsSealed && publicType.IsAbstract)
            @static = true;

        // Static support is a hack. CodeDOM does support it, and this isn't
        // correct C#, but it's good enough for our API outline
        var name = publicType.Name;

        var isStruct = publicType.IsValueType && !publicType.IsPrimitive && !publicType.IsEnum;

        var @readonly = isStruct && publicType.CustomAttributes.Any(a =>
                             a.AttributeType.FullName == "System.Runtime.CompilerServices.IsReadOnlyAttribute");

        var index = name.IndexOf('`');
        if (index != -1)
            name = name.Substring(0, index);

        var declaration = new CodeTypeDeclarationEx(name)
        {
            CustomAttributes = CreateCustomAttributes(publicType, attributeFilter),
            // TypeAttributes must be specified before the IsXXX as they manipulate TypeAttributes!
            TypeAttributes = attributes,
            IsClass = publicType.IsClass,
            IsEnum = publicType.IsEnum,
            IsInterface = publicType.IsInterface,
            IsStruct = isStruct,
            IsStatic = @static,
            IsReadonly = @readonly,
        };

        if (declaration.IsInterface && publicType.BaseType != null)
            throw new NotImplementedException("Base types for interfaces needs testing");

        PopulateGenericParameters(publicType, declaration.TypeParameters, attributeFilter, parameter =>
        {
            var declaringType = publicType.DeclaringType;

            while (declaringType != null)
            {
                if (declaringType.GenericParameters.Any(p => p.Name == parameter.Name))
                    return false; // https://github.com/PublicApiGenerator/PublicApiGenerator/issues/108

                declaringType = declaringType.DeclaringType;
            }

            return true;
        });

        if (publicType.BaseType != null && ShouldOutputBaseType(publicType))
        {
            if (publicType.BaseType.FullName == "System.Enum")
            {
                var underlyingType = publicType.GetEnumUnderlyingType();
                if (underlyingType.FullName != "System.Int32")
                    declaration.BaseTypes.Add(underlyingType.CreateCodeTypeReference());
            }
            else
            {
                declaration.BaseTypes.Add(publicType.BaseType.CreateCodeTypeReference(publicType));
            }
        }
        foreach (var @interface in publicType.Interfaces.OrderBy(i => i.InterfaceType.FullName, StringComparer.Ordinal)
            .Select(t => new { Reference = t, Definition = t.InterfaceType.Resolve() })
            .Where(t => ShouldIncludeType(t.Definition, [], [], true))
            .Select(t => t.Reference))
        {
            declaration.BaseTypes.Add(@interface.InterfaceType.CreateCodeTypeReference(@interface));
        }

        foreach (var memberInfo in publicType.GetMembers().Where(memberDefinition => ShouldIncludeMember(memberDefinition, denyNamespacePrefixes, allowNamespacePrefixes, useDenyNamespacePrefixesForExtensionMethods)).OrderBy(m => m.Name, StringComparer.Ordinal))
            AddMemberToTypeDeclaration(declaration, publicType, memberInfo, attributeFilter);

        // Fields should be in defined order for an enum
        var fields = !publicType.IsEnum
            ? publicType.Fields.OrderBy(f => f.Name, StringComparer.Ordinal)
            : (IEnumerable<FieldDefinition>)publicType.Fields;
        foreach (var field in fields)
            AddMemberToTypeDeclaration(declaration, publicType, field, attributeFilter);

        foreach (var nestedType in publicType.NestedTypes.Where(t => ShouldIncludeType(t, denyNamespacePrefixes, allowNamespacePrefixes, useDenyNamespacePrefixesForExtensionMethods)).OrderBy(t => t.FullName, StringComparer.Ordinal))
        {
            using (NullableContext.Push(nestedType))
            {
                var nestedTypeDeclaration = CreateTypeDeclaration(nestedType, denyNamespacePrefixes, allowNamespacePrefixes, useDenyNamespacePrefixesForExtensionMethods, attributeFilter);
                declaration.Members.Add(nestedTypeDeclaration);
            }
        }

        return declaration.Sort();
    }

    private static CodeTypeDelegate CreateDelegateDeclaration(TypeDefinition publicType, AttributeFilter attributeFilter)
    {
        var invokeMethod = publicType.Methods.Single(m => m.Name == "Invoke");
        using (NullableContext.Push(invokeMethod)) // for delegates NullableContextAttribute is stored on Invoke method
        {
            var name = publicType.Name;
            var index = name.IndexOf('`');
            if (index != -1)
                name = name.Substring(0, index);
            var declaration = new CodeTypeDelegate(name)
            {
                Attributes = MemberAttributes.Public,
                CustomAttributes = CreateCustomAttributes(publicType, attributeFilter),
                ReturnType = invokeMethod.ReturnType.CreateCodeTypeReference(invokeMethod.MethodReturnType),
            };

            // CodeDOM. No support. Return type attributes.
            PopulateCustomAttributes(invokeMethod.MethodReturnType, declaration.CustomAttributes, type => type.MakeReturn(), attributeFilter);
            PopulateGenericParameters(publicType, declaration.TypeParameters, attributeFilter, _ => true);
            PopulateMethodParameters(invokeMethod, declaration.Parameters, attributeFilter);

            // Of course, CodeDOM doesn't support generic type parameters for delegates. Of course.
            if (declaration.TypeParameters.Count > 0)
            {
                var parameterNames = from parameterType in declaration.TypeParameters.Cast<CodeTypeParameter>()
                                     select parameterType.Name;
                declaration.Name = string.Format(CultureInfo.InvariantCulture, "{0}<{1}>", declaration.Name, string.Join(", ", parameterNames));
            }

            return declaration;
        }
    }

    private static bool ShouldOutputBaseType(TypeDefinition publicType)
    {
        return publicType.BaseType.FullName != "System.Object" && publicType.BaseType.FullName != "System.ValueType";
    }

    private static void PopulateGenericParameters(IGenericParameterProvider publicType, CodeTypeParameterCollection parameters, AttributeFilter attributeFilter, Func<GenericParameter, bool> shouldUseParameter)
    {
        foreach (var parameter in publicType.GenericParameters.Where(shouldUseParameter))
        {
            // A little hacky. Means we get "in" and "out" prefixed on any constraints, but it's either that
            // or add it as a custom attribute
            var name = parameter.Name;
            if (parameter.IsCovariant)
                name = "out " + name;
            if (parameter.IsContravariant)
                name = "in " + name;

            var attributeCollection = new CodeAttributeDeclarationCollection();
            if (parameter.HasCustomAttributes)
            {
                PopulateCustomAttributes(parameter, attributeCollection, attributeFilter);
            }

            var typeParameter = new CodeTypeParameter(name)
            {
                HasConstructorConstraint =
                    parameter.HasDefaultConstructorConstraint && !parameter.HasNotNullableValueTypeConstraint
            };

            typeParameter.CustomAttributes.AddRange(attributeCollection.OfType<CodeAttributeDeclaration>().ToArray());

            var nullableConstraint = parameter.GetNullabilityMap().First();
            var unmanagedConstraint = parameter.CustomAttributes.Any(attr => attr.AttributeType.FullName == "System.Runtime.CompilerServices.IsUnmanagedAttribute");

            if (parameter.HasNotNullableValueTypeConstraint)
                typeParameter.Constraints.Add(unmanagedConstraint ? " unmanaged" : " struct");

            if (parameter.HasReferenceTypeConstraint)
                typeParameter.Constraints.Add(nullableConstraint == true ? " class?" : " class");
            else if (nullableConstraint == false)
                typeParameter.Constraints.Add(" notnull");

            using (NullableContext.Push(parameter))
            {
                foreach (var constraint in parameter.Constraints.Where(constraint => !IsSpecialConstraint(constraint)))
                {
                    // for generic constraints like IEnumerable<T> call to GetElementType() returns TypeReference with Name = !0
                    var typeReference = constraint.ConstraintType /*.GetElementType()*/.CreateCodeTypeReference(constraint);
                    typeParameter.Constraints.Add(typeReference);
                }
            }
            parameters.Add(typeParameter);
        }

        static bool IsSpecialConstraint(GenericParameterConstraint constraint)
        {
            // struct
            if (constraint.ConstraintType is TypeReference reference && reference.FullName == "System.ValueType")
                return true;

            // unmanaged
            if (constraint.ConstraintType.IsUnmanaged())
                return true;

            return false;
        }
    }

    private static CodeAttributeDeclarationCollection CreateCustomAttributes(ICustomAttributeProvider type,
        AttributeFilter attributeFilter)
    {
        var attributes = new CodeAttributeDeclarationCollection();
        PopulateCustomAttributes(type, attributes, attributeFilter);
        return attributes;
    }

    private static void PopulateCustomAttributes(ICustomAttributeProvider type,
        CodeAttributeDeclarationCollection attributes,
        AttributeFilter attributeFilter)
    {
        PopulateCustomAttributes(type, attributes, ctr => ctr, attributeFilter);
    }

    private static void PopulateCustomAttributes(ICustomAttributeProvider type,
        CodeAttributeDeclarationCollection attributes,
        Func<CodeTypeReference, CodeTypeReference> codeTypeModifier,
        AttributeFilter attributeFilter)
    {
        foreach (var customAttribute in type.CustomAttributes.Where(attributeFilter.ShouldIncludeAttribute).OrderBy(a => a.AttributeType.FullName, StringComparer.Ordinal).ThenBy(a => ConvertAttributeToCode(codeTypeModifier, a), StringComparer.Ordinal))
        {
            var attribute = GenerateCodeAttributeDeclaration(codeTypeModifier, customAttribute);
            attributes.Add(attribute);
        }

        // so this is not very cool but at least all the attribute creation is in the same place
        PopulateAttributesThatDontAppearInCustomAttributes(type, attributes);
    }

    private static void PopulateAttributesThatDontAppearInCustomAttributes(ICustomAttributeProvider type,
        CodeAttributeDeclarationCollection attributes)
    {
        if (type is TypeDefinition typeDefinition && typeDefinition.Attributes.HasFlag(Mono.Cecil.TypeAttributes.Serializable))
        {
            var attribute = new CodeAttributeDeclaration("System.SerializableAttribute");
            attribute.Name = AttributeNameBuilder.Get(attribute.Name);
            attributes.Add(attribute);
        }
    }

    private static CodeAttributeDeclaration GenerateCodeAttributeDeclaration(Func<CodeTypeReference, CodeTypeReference> codeTypeModifier, CustomAttribute customAttribute)
    {
        var attribute = new CodeAttributeDeclaration(codeTypeModifier(customAttribute.AttributeType.CreateCodeTypeReference(mode: NullableMode.Disable)));
        attribute.Name = AttributeNameBuilder.Get(attribute.Name);
        foreach (var arg in customAttribute.ConstructorArguments)
        {
            attribute.Arguments.Add(new CodeAttributeArgument(CreateInitialiserExpression(arg)));
        }
        foreach (var field in customAttribute.Fields.OrderBy(f => f.Name, StringComparer.Ordinal))
        {
            attribute.Arguments.Add(new CodeAttributeArgument(field.Name, CreateInitialiserExpression(field.Argument)));
        }
        foreach (var property in customAttribute.Properties.OrderBy(p => p.Name, StringComparer.Ordinal))
        {
            attribute.Arguments.Add(new CodeAttributeArgument(property.Name, CreateInitialiserExpression(property.Argument)));
        }
        return attribute;
    }

    // Litee: This method is used for additional sorting of custom attributes when multiple values are allowed
    private static string ConvertAttributeToCode(Func<CodeTypeReference, CodeTypeReference> codeTypeModifier, CustomAttribute customAttribute)
    {
        using var provider = new CSharpCodeProvider();

        var cgo = new CodeGeneratorOptions
        {
            BracingStyle = "C",
            BlankLinesBetweenMembers = false,
            VerbatimOrder = false
        };
        var attribute = GenerateCodeAttributeDeclaration(codeTypeModifier, customAttribute);
        var declaration = new CodeTypeDeclaration("DummyClass")
        {
            CustomAttributes = new CodeAttributeDeclarationCollection(new[] { attribute }),
        };
        using (var writer = new StringWriter())
        {
            provider.GenerateCodeFromType(declaration, writer, cgo);
            return writer.ToString();
        }
    }

    private static CodeExpression CreateInitialiserExpression(CustomAttributeArgument attributeArgument)
    {
        if (attributeArgument.Value is CustomAttributeArgument customAttributeArgument)
        {
            return CreateInitialiserExpression(customAttributeArgument);
        }

        if (attributeArgument.Value is CustomAttributeArgument[] customAttributeArguments)
        {
            var initialisers = from argument in customAttributeArguments
                               select CreateInitialiserExpression(argument);
            return new CodeArrayCreateExpression(attributeArgument.Type.CreateCodeTypeReference(), initialisers.ToArray());
        }

        var type = attributeArgument.Type.Resolve();
        var value = attributeArgument.Value;
        if (type.BaseType != null && type.BaseType.FullName == "System.Enum")
        {
            var originalValue = Convert.ToInt64(value);
            if (type.CustomAttributes.Any(a => a.AttributeType.FullName == "System.FlagsAttribute"))
            {
                //var allFlags = from f in type.Fields
                //    where f.Constant != null
                //    let v = Convert.ToInt64(f.Constant)
                //    where v == 0 || (originalValue & v) != 0
                //    select (CodeExpression)new CodeFieldReferenceExpression(typeExpression, f.Name);
                //return allFlags.Aggregate((current, next) => new CodeBinaryOperatorExpression(current, CodeBinaryOperatorType.BitwiseOr, next));

                var exactMatch = type.Fields.FirstOrDefault(f => Convert.ToInt64(f.Constant) == originalValue);
                if (exactMatch != null)
                    return new CodeSnippetExpression(type.FullName + "." + exactMatch.Name);

                // I'd rather use the above, as it's just using the CodeDOM, but it puts
                // brackets around each CodeBinaryOperatorExpression
                var flags = from f in type.Fields
                            where f.Constant != null
                            let v = Convert.ToInt64(f.Constant)
                            where v == 0 || (originalValue & v) == v
                            select type.FullName + "." + f.Name;
                return new CodeSnippetExpression(flags.Aggregate((current, next) => current + " | " + next));
            }

            var allFlags = from f in type.Fields
                           where f.Constant != null
                           let v = Convert.ToInt64(f.Constant)
                           where v == originalValue
                           select new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(type.CreateCodeTypeReference()), f.Name);
            return allFlags.FirstOrDefault();
        }

        if (type.FullName == "System.Type" && value is TypeReference typeRef)
        {
            return new CodeTypeOfExpression(typeRef.CreateCodeTypeReference());
        }

        if (value is string s)
            value = NormalizeString(s);

        return new CodePrimitiveExpression(value);
    }

    private static void AddCtorToTypeDeclaration(CodeTypeDeclaration typeDeclaration, MethodDefinition member, AttributeFilter attributeFilter)
    {
        var attributes = member.GetMethodAttributes();
        if (!ShouldIncludeMember(attributes))
            return;

        var method = new CodeConstructor
        {
            CustomAttributes = CreateCustomAttributes(member, attributeFilter),
            Name = member.Name,
            Attributes = attributes
        };
        PopulateMethodParameters(member, method.Parameters, attributeFilter);

        typeDeclaration.Members.Add(method);
    }

    private static void AddMethodToTypeDeclaration(CodeTypeDeclaration typeDeclaration, MethodDefinition member, AttributeFilter attributeFilter)
    {
        var attributes = member.GetMethodAttributes();
        if (!ShouldIncludeMember(attributes))
            return;

        if (member.IsSpecialName && !member.Name.StartsWith("op_"))
            return;

        var returnType = member.ReturnType.CreateCodeTypeReference(member.MethodReturnType);

        if (member.ReturnType.IsUnsafeSignatureType() || member.Parameters.Any(p => p.ParameterType.IsUnsafeSignatureType()))
            returnType = returnType.MakeUnsafe();

        var method = new CodeMemberMethodEx
        {
            MethodDefinition = member,
            Name = CSharpOperatorKeyword.Get(member.Name),
            Attributes = attributes,
            CustomAttributes = CreateCustomAttributes(member, attributeFilter),
            ReturnType = returnType,
        };
        PopulateCustomAttributes(member.MethodReturnType, method.ReturnTypeCustomAttributes, attributeFilter);
        PopulateGenericParameters(member, method.TypeParameters, attributeFilter, _ => true);
        PopulateMethodParameters(member, method.Parameters, attributeFilter, member.IsExtensionMethod());

        typeDeclaration.Members.Add(method);
    }

    private static void PopulateMethodParameters(IMethodSignature member,
        CodeParameterDeclarationExpressionCollection parameters,
        AttributeFilter attributeFilter,
        bool isExtension = false)
    {
        foreach (var parameter in member.Parameters)
        {
            FieldDirection direction = 0;
            if (parameter.IsOut)
                direction |= FieldDirection.Out;
            else if (parameter.ParameterType.IsByReference)
                direction |= FieldDirection.Ref;

            var parameterType = parameter.ParameterType;
            if (parameterType is RequiredModifierType requiredModifierType)
            {
                parameterType = requiredModifierType.ElementType;
            }
            // order is crucial because a RequiredModifierType can be a ByReferenceType
            if (parameterType is ByReferenceType byReferenceType)
            {
                parameterType = byReferenceType.ElementType;
            }

            var type = parameterType.CreateCodeTypeReference(parameter);

            if (isExtension)
            {
                type = type.MakeThis();
                isExtension = false;
            }

            // special case of ref is in
            // TODO: Move CustomAttributes.Any(a => a.AttributeType.FullName == "System.Runtime.CompilerServices.IsReadOnlyAttribute") to extension method once other PR is merged
            if (parameter.CustomAttributes.Any(a =>
                    a.AttributeType.FullName == "System.Runtime.CompilerServices.IsReadOnlyAttribute"))
            {
                type = type.MakeIn();
                direction = FieldDirection.In;
            }

            var name = parameter.HasConstant
                ? string.Format(CultureInfo.InvariantCulture, "{0} = {1}", parameter.Name, FormatParameterConstant(parameter))
                : parameter.Name;
            var expression = new CodeParameterDeclarationExpression(type, name)
            {
                Direction = direction,
                CustomAttributes = CreateCustomAttributes(parameter, attributeFilter)
            };
            parameters.Add(expression);
        }
    }

    private static object FormatParameterConstant(ParameterDefinition parameter)
    {
        if (parameter.Constant is string)
            return string.Format(CultureInfo.InvariantCulture, "\"{0}\"", parameter.Constant);

        if (parameter.Constant is bool b)
            return b ? "true" : "false";

        if (parameter.Constant != null)
            return parameter.Constant;

        if (parameter.ParameterType is GenericParameter genericParam)
        {
            if (genericParam.HasReferenceTypeConstraint)
                return "null";

            if (genericParam.HasNotNullableValueTypeConstraint)
                return "default";

            // this seems right for default
            return "default";
        }

        return parameter.ParameterType.IsValueType ? "default" : "null";
    }

    private static void AddPropertyToTypeDeclaration(CodeTypeDeclaration typeDeclaration, IMemberDefinition typeDeclarationInfo, PropertyDefinition member, AttributeFilter attributeFilter)
    {
        var getterAttributes = member.GetMethod?.GetMethodAttributes() ?? 0;
        var setterAttributes = member.SetMethod?.GetMethodAttributes() ?? 0;

        var hasGet = ShouldIncludeMember(getterAttributes);
        var hasSet = ShouldIncludeMember(setterAttributes);

        if (!(hasGet | hasSet))
            return;

        var propertyAttributes = CecilEx.CombineAccessorAttributes(getterAttributes, setterAttributes);

        var propertyType = member.PropertyType.IsGenericParameter
            ? new CodeTypeReference(member.PropertyType.Name)
            : member.PropertyType.CreateCodeTypeReference(member);

        if (member.PropertyType.IsUnsafeSignatureType())
            propertyType = propertyType.MakeUnsafe();

        var property = new CodeMemberPropertyEx
        {
            PropertyDefinition = member,
            Name = member.Name,
            Type = propertyType,
            Attributes = propertyAttributes,
            CustomAttributes = CreateCustomAttributes(member, attributeFilter),
            HasGet = hasGet,
            HasSet = hasSet
        };

        // DefaultMemberAttribute on type gets propagated to IndexerNameAttribute
        var defaultMemberAttributeValue = typeDeclarationInfo.CustomAttributes.SingleOrDefault(x =>
                x.AttributeType.FullName == "System.Reflection.DefaultMemberAttribute")
            ?.ConstructorArguments.Select(x => x.Value).OfType<string>().SingleOrDefault();
        if (!string.IsNullOrEmpty(defaultMemberAttributeValue) && member.Name == defaultMemberAttributeValue && member.Name != "Item")
        {
            property.Name = "Item";
            property.CustomAttributes.Add(
                new CodeAttributeDeclaration(
                    AttributeNameBuilder.Get("System.Runtime.CompilerServices.IndexerNameAttribute"))
                {
                    Arguments = { new CodeAttributeArgument(new CodePrimitiveExpression(defaultMemberAttributeValue)) }
                });
        }

        // Here's a nice hack, because hey, guess what, the CodeDOM doesn't support
        // attributes on getters or setters
        if (member.GetMethod != null && member.GetMethod.HasCustomAttributes)
        {
            PopulateCustomAttributes(member.GetMethod, property.CustomAttributes, type => type.MakeGet(), attributeFilter);
        }
        if (member.SetMethod != null && member.SetMethod.HasCustomAttributes)
        {
            PopulateCustomAttributes(member.SetMethod, property.CustomAttributes, type => type.MakeSet(), attributeFilter);
        }

        foreach (var parameter in member.Parameters)
        {
            property.Parameters.Add(
                new CodeParameterDeclarationExpression(parameter.ParameterType.CreateCodeTypeReference(parameter),
                    parameter.Name));
        }

        // TODO: CodeDOM has no support for different access modifiers for getters and setters
        // TODO: CodeDOM has no support for attributes on setters or getters - promote to property?

        if (hasSet && member.SetMethod?.ReturnType is RequiredModifierType reqmod && reqmod.ModifierType.FullName == "System.Runtime.CompilerServices.IsExternalInit")
        {
            property.Name = string.Format(CodeNormalizer.PROPERTY_INIT_ONLY_SETTER_TEMPLATE, property.Name);
        }

        typeDeclaration.Members.Add(property);
    }

    private static void AddEventToTypeDeclaration(CodeTypeDeclaration typeDeclaration, EventDefinition eventDefinition, AttributeFilter attributeFilter)
    {
        var addAccessorAttributes = eventDefinition.AddMethod.GetMethodAttributes();
        var removeAccessorAttributes = eventDefinition.RemoveMethod.GetMethodAttributes();

        if (!(ShouldIncludeMember(addAccessorAttributes) || ShouldIncludeMember(removeAccessorAttributes)))
            return;

        var @event = new CodeMemberEvent
        {
            Name = EventNameBuilder.AugmentEventNameWithEventModifierMarkerTemplate(eventDefinition, addAccessorAttributes, removeAccessorAttributes),
            Attributes = CecilEx.CombineAccessorAttributes(addAccessorAttributes, removeAccessorAttributes),
            CustomAttributes = CreateCustomAttributes(eventDefinition, attributeFilter),
            Type = eventDefinition.EventType.CreateCodeTypeReference(eventDefinition)
        };

        typeDeclaration.Members.Add(@event);
    }

    private static void AddFieldToTypeDeclaration(CodeTypeDeclaration typeDeclaration, FieldDefinition memberInfo, AttributeFilter attributeFilter)
    {
        if (memberInfo.IsPrivate || memberInfo.IsAssembly || memberInfo.IsFamilyAndAssembly || memberInfo.IsSpecialName)
            return;

        MemberAttributes attributes = 0;
        if (memberInfo.HasConstant)
            attributes |= MemberAttributes.Const;
        if (memberInfo.IsFamily || memberInfo.IsFamilyOrAssembly)
            attributes |= MemberAttributes.Family;
        if (memberInfo.IsPublic)
            attributes |= MemberAttributes.Public;
        if (memberInfo.IsStatic && !memberInfo.HasConstant)
            attributes |= MemberAttributes.Static;

        // TODO: Values for readonly fields are set in the ctor
        var codeTypeReference = memberInfo.FieldType.CreateCodeTypeReference(memberInfo);
        if (memberInfo.IsInitOnly)
            codeTypeReference = codeTypeReference.MakeReadonly();
        if (memberInfo.FieldType.IsUnsafeSignatureType())
            codeTypeReference = codeTypeReference.MakeUnsafe();
        if (memberInfo.FieldType.IsVolatile())
            codeTypeReference = codeTypeReference.MakeVolatile();

        var field = new CodeMemberField(codeTypeReference, memberInfo.Name)
        {
            Attributes = attributes,
            CustomAttributes = CreateCustomAttributes(memberInfo, attributeFilter)
        };

        if (memberInfo.HasConstant)
        {
            var value = memberInfo.Constant;
            if (value is string s)
                value = NormalizeString(s);
            field.InitExpression = new CodePrimitiveExpression(value);
        }

        typeDeclaration.Members.Add(field);
    }

    private static string NormalizeString(string s)
    {
        // CodeDOM outputs a verbatim string. Any string with \n is treated as such, so normalize
        // it to make it easier for comparisons
        s = Regex.Replace(s, @"\n", "\\n");
        s = Regex.Replace(s, @"\r\n|\r\\n", "\\r\\n");
        return s;
    }
}
