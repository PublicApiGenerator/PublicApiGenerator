using System.Runtime.CompilerServices;
using OtherAssembly;

[assembly: TypeForwardedTo(typeof(ForwardedClass))]

namespace InitialAssembly;

/// <summary>
/// Just to have something in assembly.
/// </summary>
public class SomeClass
{
}
