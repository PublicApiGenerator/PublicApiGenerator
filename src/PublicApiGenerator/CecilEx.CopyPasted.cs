using System.Reflection;
using Mono.Cecil;
using Mono.Collections.Generic;

namespace PublicApiGenerator;

// See https://github.com/PublicApiGenerator/PublicApiGenerator/pull/226, all this code
// was taken from Mono.Cecil and adopted to solve this issue. The main change here is
// commenting out !AreSame(methodDefinition.ReturnType, reference.ReturnType) condition.
internal static partial class CecilEx
{
    private enum ElementType : byte
    {
        None = 0x00,
        Void = 0x01,
        Boolean = 0x02,
        Char = 0x03,
        I1 = 0x04,
        U1 = 0x05,
        I2 = 0x06,
        U2 = 0x07,
        I4 = 0x08,
        U4 = 0x09,
        I8 = 0x0a,
        U8 = 0x0b,
        R4 = 0x0c,
        R8 = 0x0d,
        String = 0x0e,
        Ptr = 0x0f,   // Followed by <type> token
        ByRef = 0x10,   // Followed by <type> token
        ValueType = 0x11,   // Followed by <type> token
        Class = 0x12,   // Followed by <type> token
        Var = 0x13,   // Followed by generic parameter number
        Array = 0x14,   // <type> <rank> <boundsCount> <bound1>  <loCount> <lo1>
        GenericInst = 0x15,   // <type> <type-arg-count> <type-1> ... <type-n> */
        TypedByRef = 0x16,
        I = 0x18,   // System.IntPtr
        U = 0x19,   // System.UIntPtr
        FnPtr = 0x1b,   // Followed by full method signature
        Object = 0x1c,   // System.Object
        SzArray = 0x1d,   // Single-dim array with 0 lower bound
        MVar = 0x1e,   // Followed by generic parameter number
        CModReqD = 0x1f,   // Required modifier : followed by a TypeDef or TypeRef token
        CModOpt = 0x20,   // Optional modifier : followed by a TypeDef or TypeRef token
        Internal = 0x21,   // Implemented within the CLI
        Modifier = 0x40,   // Or'd with following element types
        Sentinel = 0x41,   // Sentinel for varargs method signature
        Pinned = 0x45,   // Denotes a local variable that points at a pinned object

        // special undocumented constants
        Type = 0x50,
        Boxed = 0x51,
        Enum = 0x55
    }

    private static readonly FieldInfo _etype = typeof(TypeReference).GetField("etype", BindingFlags.Instance | BindingFlags.NonPublic);

    private static ElementType eType(TypeReference a) => (ElementType)(byte)_etype.GetValue(a);

    private static bool IsTypeSpecification(TypeReference type)
    {
        switch (eType(type))
        {
            case ElementType.Array:
            case ElementType.ByRef:
            case ElementType.CModOpt:
            case ElementType.CModReqD:
            case ElementType.FnPtr:
            case ElementType.GenericInst:
            case ElementType.MVar:
            case ElementType.Pinned:
            case ElementType.Ptr:
            case ElementType.SzArray:
            case ElementType.Sentinel:
            case ElementType.Var:
                return true;
        }

        return false;
    }

    private static bool AreSame(TypeReference a, TypeReference b)
    {
        if (ReferenceEquals(a, b))
            return true;

        if (a == null || b == null)
            return false;

        if (eType(a) != eType(b))
            return false;

        if (a.IsGenericParameter)
            return string.Equals(a.Name, b.Name);

        if (IsTypeSpecification(a))
            return AreSame((TypeSpecification)a, (TypeSpecification)b);

        if (a.Name != b.Name || a.Namespace != b.Namespace)
            return false;

        //TODO: check scope

        return AreSame(a.DeclaringType, b.DeclaringType);
    }

    private static bool AreSame(Collection<ParameterDefinition> a, Collection<ParameterDefinition> b)
    {
        var count = a.Count;

        if (count != b.Count)
            return false;

        if (count == 0)
            return true;

        for (int i = 0; i < count; i++)
            if (!AreSame(a[i].ParameterType, b[i].ParameterType))
                return false;

        return true;
    }

    private static bool IsVarArg(IMethodSignature self) => self.CallingConvention == MethodCallingConvention.VarArg;

    private static int GetSentinelPosition(IMethodSignature self)
    {
        if (!self.HasParameters)
            return -1;

        var parameters = self.Parameters;
        for (int i = 0; i < parameters.Count; i++)
            if (parameters[i].ParameterType.IsSentinel)
                return i;

        return -1;
    }

    private static bool IsVarArgCallTo(MethodDefinition method, MethodReference reference)
    {
        if (method.Parameters.Count >= reference.Parameters.Count)
            return false;

        if (GetSentinelPosition(reference) != method.Parameters.Count)
            return false;

        for (int i = 0; i < method.Parameters.Count; i++)
            if (!AreSame(method.Parameters[i].ParameterType, reference.Parameters[i].ParameterType))
                return false;

        return true;
    }

    public static MethodDefinition? GetMethodIgnoringReturnType(Collection<MethodDefinition> methods, MethodReference reference)
    {
        for (int i = 0; i < methods.Count; i++)
        {
            MethodDefinition methodDefinition = methods[i];
            // SEE COMMENTED OUT CONDITION HERE
            // https://github.com/PublicApiGenerator/PublicApiGenerator/pull/226#issuecomment-873645565
            if (methodDefinition.Name != reference.Name || methodDefinition.HasGenericParameters != reference.HasGenericParameters || (methodDefinition.HasGenericParameters && methodDefinition.GenericParameters.Count != reference.GenericParameters.Count) /*|| !AreSame(methodDefinition.ReturnType, reference.ReturnType)*/ || methodDefinition.HasThis != reference.HasThis || IsVarArg(methodDefinition) != IsVarArg(reference))
            {
                continue;
            }
            if (IsVarArg(methodDefinition) && IsVarArgCallTo(methodDefinition, reference))
            {
                return methodDefinition;
            }
            if (methodDefinition.HasParameters == reference.HasParameters)
            {
                if (!methodDefinition.HasParameters && !reference.HasParameters)
                {
                    return methodDefinition;
                }
                if (AreSame(methodDefinition.Parameters, reference.Parameters))
                {
                    return methodDefinition;
                }
            }
        }
        return null;
    }
}
