using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Event_modifiers : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_abstract_modifier()
        {
            AssertPublicApi<ClassWithAbstractEvent>("""
namespace PublicApiGeneratorTests.Examples
{
    public abstract class ClassWithAbstractEvent
    {
        protected ClassWithAbstractEvent() { }
        public abstract event System.EventHandler Event;
    }
}
""");
        }

        [Fact]
        public void Should_output_abstract_new_modifier()
        {
            AssertPublicApi<ClassWithAbstractNewEvent>("""
namespace PublicApiGeneratorTests.Examples
{
    public abstract class ClassWithAbstractNewEvent : PublicApiGeneratorTests.Examples.ClassWithVirtualEvent
    {
        protected ClassWithAbstractNewEvent() { }
        public new abstract event System.EventHandler Event;
    }
}
""");
        }

        [Fact]
        public void Should_output_static_modifier()
        {
            AssertPublicApi<ClassWithStaticEvent>("""
namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithStaticEvent
    {
        public ClassWithStaticEvent() { }
        public static event System.EventHandler Event;
    }
}
""");
        }

        [Fact]
        public void Should_output_protected_static_modifier()
        {
            AssertPublicApi<ClassWithProtectedStaticEvent>("""
namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedStaticEvent
    {
        public ClassWithProtectedStaticEvent() { }
        protected static event System.EventHandler Event;
    }
}
""");
        }

        [Fact]
        public void Should_output_static_new_modifier()
        {
            AssertPublicApi<ClassWithStaticNewEvent>("""
namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithStaticNewEvent : PublicApiGeneratorTests.Examples.ClassWithStaticEvent
    {
        public ClassWithStaticNewEvent() { }
        public new static event System.EventHandler Event;
    }
}
""");
        }

        [Fact]
        public void Should_output_protected_static_new_modifier()
        {
            AssertPublicApi<ClassWithProtectedStaticNewEvent>("""
namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedStaticNewEvent : PublicApiGeneratorTests.Examples.ClassWithProtectedStaticEvent
    {
        public ClassWithProtectedStaticNewEvent() { }
        protected new static event System.EventHandler Event;
    }
}
""");
        }

        [Fact]
        public void Should_output_public_static_new_modifier()
        {
            AssertPublicApi<ClassWithPublicStaticNewEvent>("""
namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithPublicStaticNewEvent : PublicApiGeneratorTests.Examples.ClassWithProtectedStaticEvent
    {
        public ClassWithPublicStaticNewEvent() { }
        public new static event System.EventHandler Event;
    }
}
""");
        }

        [Fact]
        public void Should_output_virtual_modifier()
        {
            AssertPublicApi<ClassWithVirtualEvent>("""
namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithVirtualEvent
    {
        public ClassWithVirtualEvent() { }
        public virtual event System.EventHandler Event;
    }
}
""");
        }

        [Fact]
        public void Should_output_protected_virtual_modifier()
        {
            AssertPublicApi<ClassWithProtectedVirtualEvent>("""
namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedVirtualEvent
    {
        public ClassWithProtectedVirtualEvent() { }
        protected virtual event System.EventHandler Event;
    }
}
""");
        }

        [Fact]
        public void Should_output_new_virtual_modifier()
        {
            AssertPublicApi<ClassWithNewVirtualEvent>("""
namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithNewVirtualEvent : PublicApiGeneratorTests.Examples.ClassWithVirtualEvent
    {
        public ClassWithNewVirtualEvent() { }
        public new virtual event System.EventHandler Event;
    }
}
""");
        }

        [Fact]
        public void Should_output_protected_new_virtual_modifier()
        {
            AssertPublicApi<ClassWithProtectedNewVirtualEvent>("""
namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedNewVirtualEvent : PublicApiGeneratorTests.Examples.ClassWithProtectedVirtualEvent
    {
        public ClassWithProtectedNewVirtualEvent() { }
        public new virtual event System.EventHandler Event;
    }
}
""");
        }

        [Fact]
        public void Should_output_override_modifier()
        {
            AssertPublicApi<ClassWithOverridingEvent>("""
namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithOverridingEvent : PublicApiGeneratorTests.Examples.ClassWithVirtualEvent
    {
        public ClassWithOverridingEvent() { }
        public override event System.EventHandler Event;
    }
}
""");
        }

        [Fact]
        public void Should_output_protected_override_modifier()
        {
            AssertPublicApi<ClassWithProtectedOverridingEvent>("""
namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedOverridingEvent : PublicApiGeneratorTests.Examples.ClassWithProtectedVirtualEvent
    {
        public ClassWithProtectedOverridingEvent() { }
        protected override event System.EventHandler Event;
    }
}
""");
        }

        [Fact]
        public void Should_output_abstract_override_modifier()
        {
            AssertPublicApi<ClassWithAbstractOverrideEvent>("""
namespace PublicApiGeneratorTests.Examples
{
    public abstract class ClassWithAbstractOverrideEvent : PublicApiGeneratorTests.Examples.ClassWithVirtualEvent
    {
        protected ClassWithAbstractOverrideEvent() { }
        public abstract override event System.EventHandler Event;
    }
}
""");
        }

        [Fact]
        public void Should_output_sealed_modifier()
        {
            AssertPublicApi<ClassWithSealedOverridingEvent>("""
namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithSealedOverridingEvent : PublicApiGeneratorTests.Examples.ClassWithVirtualEvent
    {
        public ClassWithSealedOverridingEvent() { }
        public override sealed event System.EventHandler Event;
    }
}
""");
        }

        [Fact]
        public void Should_output_protected_sealed_modifier()
        {
            AssertPublicApi<ClassWithProtectedSealedOverridingEvent>("""
namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedSealedOverridingEvent : PublicApiGeneratorTests.Examples.ClassWithProtectedVirtualEvent
    {
        public ClassWithProtectedSealedOverridingEvent() { }
        protected override sealed event System.EventHandler Event;
    }
}
""");
        }

        [Fact]
        public void Should_output_new_modifier()
        {
            AssertPublicApi<ClassWithEventHiding>("""
namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithEventHiding : PublicApiGeneratorTests.Examples.ClassWithVirtualEvent
    {
        public ClassWithEventHiding() { }
        public new event System.EventHandler Event;
    }
}
""");
        }

        [Fact]
        public void Should_output_protected_new_modifier()
        {
            AssertPublicApi<ClassWithProtectedEventHiding>("""
namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedEventHiding : PublicApiGeneratorTests.Examples.ClassWithProtectedVirtualEvent
    {
        public ClassWithProtectedEventHiding() { }
        protected new event System.EventHandler Event;
    }
}
""");
        }

        [Fact]
        public void Should_output_public_new_modifier()
        {
            AssertPublicApi<ClassWithPublicEventHiding>("""
namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithPublicEventHiding : PublicApiGeneratorTests.Examples.ClassWithProtectedVirtualEvent
    {
        public ClassWithPublicEventHiding() { }
        public new event System.EventHandler Event;
    }
}
""");
        }

        [Fact]
        public void Should_output_new_modifier_even_under_evil_circumstances()
        {
            AssertPublicApi<ClassBeingReallyEvilWithInheritingEvent>("""
namespace PublicApiGeneratorTests.Examples
{
    public abstract class ClassBeingReallyEvilWithInheritingEvent : PublicApiGeneratorTests.Examples.ClassInheritingVirtualGenericEvent
    {
        public ClassBeingReallyEvilWithInheritingEvent() { }
        public new abstract event System.EventHandler Event;
    }
}
""");
        }
    }

    namespace Examples
    {
        public abstract class ClassWithAbstractEvent
        {
            public abstract event EventHandler Event;
        }

        public abstract class ClassWithAbstractNewEvent : ClassWithVirtualEvent
        {
            public new abstract event EventHandler Event;
        }

        public abstract class ClassWithAbstractOverrideEvent : ClassWithVirtualEvent
        {
            public abstract override event EventHandler Event;
        }

        public class ClassWithStaticEvent
        {
            public static event EventHandler Event;
        }

        public class ClassWithProtectedStaticEvent
        {
            protected static event EventHandler Event;
        }

        public class ClassWithStaticNewEvent : ClassWithStaticEvent
        {
            public static new event EventHandler Event;
        }

        public class ClassWithProtectedStaticNewEvent : ClassWithProtectedStaticEvent
        {
            protected static new event EventHandler Event;
        }

        public class ClassWithPublicStaticNewEvent : ClassWithProtectedStaticEvent
        {
            public static new event EventHandler Event;
        }

        public class ClassWithVirtualEvent
        {
            public virtual event EventHandler Event;
        }

        public class ClassWithNewVirtualEvent : ClassWithVirtualEvent
        {
            public new virtual event EventHandler Event;
        }

        public class ClassWithProtectedNewVirtualEvent : ClassWithProtectedVirtualEvent
        {
            public new virtual event EventHandler Event;
        }

        public class ClassWithOverridingEvent : ClassWithVirtualEvent
        {
            public override event EventHandler Event;
        }

        public class ClassWithProtectedOverridingEvent : ClassWithProtectedVirtualEvent
        {
            protected override event EventHandler Event;
        }

        public class ClassWithSealedOverridingEvent : ClassWithVirtualEvent
        {
            public sealed override event EventHandler Event;
        }

        public class ClassWithProtectedSealedOverridingEvent : ClassWithProtectedVirtualEvent
        {
            protected sealed override event EventHandler Event;
        }

        public class ClassWithEventHiding : ClassWithVirtualEvent
        {
            public new event EventHandler Event;
        }

        public class ClassWithProtectedEventHiding : ClassWithProtectedVirtualEvent
        {
            protected new event EventHandler Event;
        }

        public class ClassWithPublicEventHiding : ClassWithProtectedVirtualEvent
        {
            public new event EventHandler Event;
        }

        public class ClassWithVirtualGenericEvent
        {
            public virtual event EventHandler<EventArgs> Event;
        }

        public class ClassInheritingVirtualGenericEvent : ClassWithVirtualGenericEvent
        {
        }

        public abstract class ClassBeingReallyEvilWithInheritingEvent : ClassInheritingVirtualGenericEvent
        {
            public ClassBeingReallyEvilWithInheritingEvent()
            {
            }
            public new abstract event EventHandler Event;
        }

        public class ClassWithProtectedVirtualEvent
        {
            protected virtual event EventHandler Event;
        }
    }
}
