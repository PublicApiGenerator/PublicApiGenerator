using PublicApiGeneratorTests.Examples;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace PublicApiGeneratorTests
{
    // Tests for https://github.com/PublicApiGenerator/PublicApiGenerator/issues/54
    // See also https://github.com/dotnet/roslyn/blob/master/docs/features/nullable-reference-types.md
    [Trait("NRT", "Nullable Reference Types")]
    public class NullableTests : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_Annotate_ReturnType()
        {
            AssertPublicApi<ReturnType>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ReturnType
    {
        public ReturnType() { }
        public string? ReturnProperty { get; set; }
    }
}");
        }

        [Fact]
        public void Should_Annotate_VoidReturn()
        {
            AssertPublicApi(typeof(VoidReturn),
@"namespace PublicApiGeneratorTests.Examples
{
    public static class VoidReturn
    {
        public static void ShouldBeEquivalentTo(this object? actual, object? expected) { }
    }
}");
        }

        [Fact]
        public void Should_Annotate_Derived_ReturnType()
        {
            AssertPublicApi<ReturnArgs>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ReturnArgs : System.EventArgs
    {
        public ReturnArgs() { }
        public string? Target { get; set; }
    }
}");
        }

        [Fact]
        public void Should_Annotate_Ctor_Args()
        {
            AssertPublicApi<NullableCtor>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class NullableCtor
    {
        public NullableCtor(string? nullableLabel, string nope) { }
    }
}");
        }

        [Fact]
        public void Should_Not_Annotate_Obsolete_Attribute()
        {
            AssertPublicApi<ClassWithObsolete>(
@"namespace PublicApiGeneratorTests.Examples
{
    [System.Obsolete(""Foo"")]
    public class ClassWithObsolete
    {
        [System.Obsolete(""Bar"")]
        public ClassWithObsolete(string? nullableLabel) { }
        [System.Obsolete(""Bar"")]
        public ClassWithObsolete(string? nullableLabel, string? nullableLabel2) { }
    }
}");
        }

        [Fact]
        public void Should_Annotate_Generic_Event()
        {
            AssertPublicApi<GenericEvent>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class GenericEvent
    {
           public GenericEvent() { }
           public event System.EventHandler<PublicApiGeneratorTests.Examples.ReturnArgs?> ReturnEvent;
    }
}");
        }

        [Fact]
        public void Should_Annotate_Delegate_Declaration()
        {
            AssertPublicApi<DelegateDeclaration>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class DelegateDeclaration
    {
           public DelegateDeclaration() { }
           public delegate string? OnNullableReturn(object sender, PublicApiGeneratorTests.Examples.ReturnArgs? args);
           public delegate string OnReturn(object sender, PublicApiGeneratorTests.Examples.ReturnArgs? args);
    }
}");
        }

        [Fact]
        public void Should_Annotate_Nullable_Array()
        {
            AssertPublicApi<NullableArray>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class NullableArray
    {
        public NullableArray() { }
        public PublicApiGeneratorTests.Examples.ReturnType[]? NullableMethod1() { }
        public PublicApiGeneratorTests.Examples.ReturnType[]?[]? NullableMethod2() { }
    }
}");
        }

        [Fact]
        public void Should_Annotate_Nullable_Enumerable()
        {
            AssertPublicApi<NullableEnumerable>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class NullableEnumerable
    {
        public NullableEnumerable() { }
        public System.Collections.Generic.IEnumerable<PublicApiGeneratorTests.Examples.ReturnType?>? Enumerable() { }
    }
}");
        }

        [Fact]
        public void Should_Annotate_Generic_Method()
        {
            AssertPublicApi<GenericMethod>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class GenericMethod
    {
        public GenericMethod() { }
        public PublicApiGeneratorTests.Examples.ReturnType? NullableGenericMethod<T1, T2, T3>(T1? t1, T2 t2, T3? t3)
            where T1 :  class
            where T2 :  class
            where T3 :  class { }
    }
}");
        }

        [Fact]
        public void Should_Annotate_Skeet_Examples()
        {
            AssertPublicApi<SkeetExamplesClass>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class SkeetExamplesClass
    {
        public System.Collections.Generic.Dictionary<System.Collections.Generic.List<string?>, string[]?> SkeetExample;
        public System.Collections.Generic.Dictionary<System.Collections.Generic.List<string?>, string?[]> SkeetExample2;
        public System.Collections.Generic.Dictionary<System.Collections.Generic.List<string?>, string?[]?> SkeetExample3;
        public SkeetExamplesClass() { }
    }
}");
        }

        [Fact]
        public void Should_Annotate_By_Ref()
        {
            AssertPublicApi<ByRefClass>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ByRefClass
    {
        public ByRefClass() { }
        public bool ByRefNullableReferenceParam(PublicApiGeneratorTests.Examples.ReturnType rt1, ref PublicApiGeneratorTests.Examples.ReturnType? rt2, PublicApiGeneratorTests.Examples.ReturnType rt3, PublicApiGeneratorTests.Examples.ReturnType? rt4, out PublicApiGeneratorTests.Examples.ReturnType? rt5, PublicApiGeneratorTests.Examples.ReturnType rt6) { }
    }
}");
        }

        [Fact]
        public void Should_Annotate_Different_API()
        {
            AssertPublicApi<NullableApi>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class NullableApi
    {
        public PublicApiGeneratorTests.Examples.ReturnType NonNullField;
        public PublicApiGeneratorTests.Examples.ReturnType? NullableField;
        public NullableApi() { }
        public System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<int, int?>?>>? ComplicatedDictionary { get; set; }
        public PublicApiGeneratorTests.Examples.ReturnType NonNullProperty { get; set; }
        public PublicApiGeneratorTests.Examples.ReturnType? NullableProperty { get; set; }
        public string? Convert(string source) { }
        public override bool Equals(object? obj) { }
        public override int GetHashCode() { }
        public PublicApiGeneratorTests.Examples.ReturnType? NullableParamAndReturnMethod(string? nullableParam, string nonNullParam, int? nullableValueType) { }
        public PublicApiGeneratorTests.Examples.ReturnType NullableParamMethod(string? nullableParam, string nonNullParam, int? nullableValueType) { }
        public PublicApiGeneratorTests.Examples.Data<string> NullableStruct1(PublicApiGeneratorTests.Examples.Data<string> param) { }
        public PublicApiGeneratorTests.Examples.Data<string>? NullableStruct2(PublicApiGeneratorTests.Examples.Data<string>? param) { }
        public PublicApiGeneratorTests.Examples.Data<System.Collections.Generic.KeyValuePair<string, string?>> NullableStruct3(PublicApiGeneratorTests.Examples.Data<System.Collections.Generic.KeyValuePair<string, string?>> param) { }
        public PublicApiGeneratorTests.Examples.Data<System.Collections.Generic.KeyValuePair<string, string?>?> NullableStruct4(PublicApiGeneratorTests.Examples.Data<System.Collections.Generic.KeyValuePair<string, string?>?> param) { }
        public PublicApiGeneratorTests.Examples.Data<System.Collections.Generic.KeyValuePair<string, string?>?>? NullableStruct5(PublicApiGeneratorTests.Examples.Data<System.Collections.Generic.KeyValuePair<string, string?>?>? param) { }
    }
}");
        }

        [Fact]
        public void Should_Annotate_System_Nullable()
        {
            AssertPublicApi<SystemNullable>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class SystemNullable
    {
        public readonly int? Age;
        public SystemNullable() { }
        public System.DateTime? Birth { get; set; }
        public float? Calc(double? first, decimal? second) { }
        public System.Collections.Generic.List<System.Guid?> GetSecrets(System.Collections.Generic.Dictionary<int?, System.Collections.Generic.Dictionary<bool?, byte?>> data) { }
    }
}");
        }

        [Fact]
        public void Should_Annotate_Generics()
        {
            AssertPublicApi<Generics>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class Generics
    {
           public Generics() { }
           public System.Collections.Generic.List<string?> GetSecretData0() { }
           public System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<int?>> GetSecretData1() { }
           public System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<int?>?> GetSecretData2() { }
           public System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string?, System.Collections.Generic.List<int>?>>> GetSecretData3(System.Collections.Generic.Dictionary<int?, System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.List<int?>>>>? value) { }
           public System.Collections.Generic.Dictionary<int?, string>? GetSecretData4(System.Collections.Generic.Dictionary<int?, string>? value) { }
    }
}");
        }

        [Fact]
        public void Should_Annotate_Structs()
        {
            AssertPublicApi<Structs>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class Structs
    {
           public System.Collections.Generic.KeyValuePair<string?, int?> field;
           public Structs() { }
    }
}");
        }

        [Fact]
        public void Should_Annotate_Tuples()
        {
            AssertPublicApi<Tuples>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class Tuples
    {
           public Tuples() { }
           public System.Tuple<string, string?, int, int?> Tuple1(System.Tuple<string, string?, int, int?>? tuple) { }
           public System.ValueTuple<string, string?, int, int?> Tuple2(System.ValueTuple<string, string?, int, int?>? tuple) { }
    }
}");
        }

        [Fact]
        public void Should_Annotate_Constraints()
        {
            AssertPublicApi<Constraints>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class Constraints
    {
           public Constraints() { }
           public void Print1<T>(T val)
               where T : class { }
           public void Print2<T>(T val)
               where T : class? { }
           public static void Print3<T>()
                where T : System.IO.Stream { }
           public static void Print4<T>()
                where T : System.IDisposable { }
    }
}");
        }

        [Fact]
        public void Should_Annotate_Nullable_Constraints()
        {
            AssertPublicApi(typeof(Constraints2<,>),
@"namespace PublicApiGeneratorTests.Examples
{
    public class Constraints2<X, Y>
        where X : System.IComparable<X>
        where Y : class?
    {
           public Constraints2() { }
           public T Convert<T>(T data)
               where T : System.IComparable<string?> { }
           public static void Print1<T>()
                where T : System.IO.Stream? { }
           public static void Print2<T>()
                where T : System.IDisposable? { }
    }
}");
        }

        [Fact]
        public void Should_Annotate_BaseType()
        {
            AssertPublicApi<NullableComparable>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class NullableComparable : System.Collections.Generic.List<string?>, System.IComparable<string?>
    {
           public NullableComparable() { }
           public int CompareTo(string? other) { }
    }
}");
        }

        [Fact]
        public void Should_Annotate_OpenGeneric()
        {
            AssertPublicApi(typeof(StringNullableList<,>),
@"namespace PublicApiGeneratorTests.Examples
{
    public class StringNullableList<T, U> : System.Collections.Generic.List<T?>, System.IComparable<U>
        where T : struct
        where U : class
    {
           public StringNullableList() { }
           public int CompareTo(U other) { }
    }
}");
        }

        [Fact]
        public void Should_Annotate_NotNull_Constraint()
        {
            AssertPublicApi(typeof(IDoStuff1<,>),
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IDoStuff1<TIn, TOut>
         where TIn : notnull
         where TOut : notnull
    {
           TOut DoStuff(TIn input);
    }
}");
        }

        [Fact]
        public void Should_Annotate_NotNull_And_Null_Constraint()
        {
            AssertPublicApi(typeof(IDoStuff2<,>),
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IDoStuff2<TIn, TOut>
         where TIn : class?
         where TOut : notnull
    {
           TOut DoStuff(TIn input);
    }
}");
        }

        [Fact]
        public void Should_Annotate_Without_Explicit_Constraints()
        {
            AssertPublicApi(typeof(IDoStuff3<,>),
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IDoStuff3<TIn, TOut>
    {
           TOut DoStuff(TIn input);
    }
}");
        }

        [Fact]
        public void Should_Annotate_NotNull_And_Null_Class_Constraint()
        {
            AssertPublicApi(typeof(IDoStuff4<,>),
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IDoStuff4<TIn, TOut>
         where TIn : class?
         where TOut : class
    {
           TOut DoStuff(TIn input);
    }
}");
        }

        [Fact]
        public void Should_Annotate_Nullable_Class_And_Struct_Constraint()
        {
            AssertPublicApi(typeof(IDoStuff5<,>),
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IDoStuff5<TIn, TOut>
         where TIn : class?
         where TOut : struct
    {
           TOut DoStuff(TIn input);
    }
}");
        }

        [Fact]
        public void Should_Annotate_Unmanaged_Constraint()
        {
            AssertPublicApi(typeof(IDoStuff6<,>),
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IDoStuff6<TIn, TOut>
         where TIn : notnull
         where TOut : unmanaged
    {
           TOut DoStuff(TIn input);
    }
}");
        }
    }

    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public class ReturnType
        {
            public string? ReturnProperty { get; set; }
        }

        public static class VoidReturn
        {
            public static void ShouldBeEquivalentTo(this object? actual, object? expected) { }
        }

        public class ReturnArgs : EventArgs
        {
            public string? Target { get; set; }
        }

        public class NullableCtor
        {
            public NullableCtor(string? nullableLabel, string nope) { }
        }

        [Obsolete("Foo")]
        public class ClassWithObsolete
        {
            [Obsolete("Bar")]
            public ClassWithObsolete(string? nullableLabel) { }

            [Obsolete("Bar")]
            public ClassWithObsolete(string? nullableLabel, string? nullableLabel2) { }
        }

        public class GenericEvent
        {
            public event EventHandler<ReturnArgs?> ReturnEvent { add { } remove { } }
        }

        public class DelegateDeclaration
        {
            protected delegate string OnReturn(object sender, ReturnArgs? args);
            protected delegate string? OnNullableReturn(object sender, ReturnArgs? args);
        }

        public class Structs
        {
            public KeyValuePair<string?, int?> field;
        }

        public struct Data<T>
        {
            public T Value { get; }
        }

        public class Generics
        {
            public List<string?> GetSecretData0() => null;
            public Dictionary<int, List<int?>> GetSecretData1() => null;
            public Dictionary<int, List<int?>?> GetSecretData2() => null;
            public Dictionary<int, List<KeyValuePair<string?, List<int>?>>> GetSecretData3(Dictionary<int?, List<KeyValuePair<string, List<int?>>>>? value) { return null; }
            public Dictionary<int?, string>? GetSecretData4(Dictionary<int?, string>? value) { return null; }
        }

        public class NullableArray
        {
            public ReturnType[]? NullableMethod1() { return null; }
            public ReturnType[]?[]? NullableMethod2() { return null; }
        }

        public class NullableEnumerable
        {
            public IEnumerable<ReturnType?>? Enumerable() { return null; }
        }

        public class GenericMethod
        {
            public ReturnType? NullableGenericMethod<T1, T2, T3>(T1? t1, T2 t2, T3? t3) where T1 : class where T2 : class where T3 : class { return null; }
        }

        public class SkeetExamplesClass
        {
            public Dictionary<List<string?>, string[]?> SkeetExample = new Dictionary<List<string?>, string[]?>();
            public Dictionary<List<string?>, string?[]> SkeetExample2 = new Dictionary<List<string?>, string?[]>();
            public Dictionary<List<string?>, string?[]?> SkeetExample3 = new Dictionary<List<string?>, string?[]?>();
        }

        public class ByRefClass
        {
            public bool ByRefNullableReferenceParam(ReturnType rt1, ref ReturnType? rt2, ReturnType rt3, ReturnType? rt4, out ReturnType? rt5, ReturnType rt6) { rt5 = null; return false; }
        }

        public class NullableApi
        {
            public ReturnType NonNullField = new ReturnType();
            public ReturnType? NullableField;
            public ReturnType NonNullProperty { get; protected set; } = new ReturnType();
            public ReturnType? NullableProperty { get; set; }
            public ReturnType NullableParamMethod(string? nullableParam, string nonNullParam, int? nullableValueType) { return new ReturnType(); }
            public ReturnType? NullableParamAndReturnMethod(string? nullableParam, string nonNullParam, int? nullableValueType) { return default; }
            public Dictionary<string, Dictionary<string, Dictionary<int, int?>?>>? ComplicatedDictionary { get; set; }
            public override bool Equals(object? obj) => base.Equals(obj);
            public override int GetHashCode() => base.GetHashCode();
            public string? Convert(string source) => source;
            public Data<string> NullableStruct1(Data<string> param) => default;
            public Data<string>? NullableStruct2(Data<string>? param) => default;
            public Data<KeyValuePair<string, string?>> NullableStruct3(Data<KeyValuePair<string, string?>> param) => default;
            public Data<KeyValuePair<string, string?>?> NullableStruct4(Data<KeyValuePair<string, string?>?> param) => default;
            public Data<KeyValuePair<string, string?>?>? NullableStruct5(Data<KeyValuePair<string, string?>?>? param) => default;
        }

        public class SystemNullable
        {
            public readonly int? Age;
            public DateTime? Birth { get; set; }

            public float? Calc(double? first, decimal? second) { return null; }

            public List<Guid?> GetSecrets(Dictionary<int?, Dictionary<bool?, byte?>> data) => null;
        }

        public class Tuples
        {
            public Tuple<string, string?, int, int?> Tuple1(Tuple<string, string?, int, int?>? tuple) => default;
            public ValueTuple<string, string?, int, int?> Tuple2(ValueTuple<string, string?, int, int?>? tuple) => default;
        }

        public class Constraints
        {
            public void Print1<T>(T val) where T : class
            {
                val.ToString();
            }

            public void Print2<T>(T val) where T : class?
            {
                if (val != null)
                    val.ToString();
            }

            public static void Print3<T>() where T : Stream { }
            public static void Print4<T>() where T : IDisposable { }
        }

        public class Constraints2<X, Y> where X: IComparable<X> where Y : class?
        {
            public T Convert<T>(T data) where T : IComparable<string?> => default;
            public static void Print1<T>() where T : Stream? { }
            public static void Print2<T>() where T : IDisposable? { }
        }

        public class NullableComparable : List<string?>, IComparable<string?>
        {
            public int CompareTo(string? other)
            {
                throw new NotImplementedException();
            }
        }

        public class StringNullableList<T,U> : List<T?>, IComparable<U> where T : struct where U : class
        {
            public int CompareTo(U other) => 0;
        }

        public interface IDoStuff1<TIn, TOut>
            where TIn : notnull
            where TOut : notnull
        {
            TOut DoStuff(TIn input);
        }

        public interface IDoStuff2<TIn, TOut>
           where TIn : class?
           where TOut : notnull
        {
            TOut DoStuff(TIn input);
        }

        public interface IDoStuff3<TIn, TOut>
        {
            TOut DoStuff(TIn input);
        }

        public interface IDoStuff4<TIn, TOut>
          where TIn : class?
          where TOut : class
        {
            TOut DoStuff(TIn input);
        }

        public interface IDoStuff5<TIn, TOut>
         where TIn : class?
         where TOut : struct
        {
            TOut DoStuff(TIn input);
        }

        public interface IDoStuff6<TIn, TOut>
         where TIn : notnull
         where TOut : unmanaged
        {
            TOut DoStuff(TIn input);
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedMember.Global
}
