using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ApprovalTests.Reporters;

// TODO: Example requires xunit + xunit.extensions to be installed
using Xunit.Extensions;
using Xunit.Sdk;

namespace ApiApprover
{
    public class ExampleApiApprovalTest
    {
        [TheoryWithLimitedFailures(20)]
        [PropertyData("AssemblyPaths")]
        [UseReporter(typeof(DiffReporter))]
        public void approve_public_api(string assembly, string path)
        {
            PublicApiApprover.ApprovePublicApi(Path.Combine(path, assembly), "results");
        }

        public static IEnumerable<object[]> AssemblyPaths
        {
            get
            {
                // Note that these are split to get nicer reporting in the test runner
                // The assembly name comes first so we can read it, rather than the path
                const string path = @"C:\temp";
                Directory.SetCurrentDirectory(path);
                return Directory.EnumerateFiles(path, "*.dll").OrderBy(p => p)
                    .Select(Path.GetFileName)
                    .Where(ShouldInclude)
                    .Select(f => new object[] { f, path });
                //yield return new object[] { "MyApi.dll", path };
            }
        }

        private static bool ShouldInclude(string assembly)
        {
            return assembly.StartsWith("MyCompany", StringComparison.OrdinalIgnoreCase) &&
                   !assembly.EndsWith("Tests.dll", StringComparison.OrdinalIgnoreCase);
        }
    }

    /// <summary>
    /// Use instead of xunit's <see cref="TheoryAttribute"/> to consume multiple data items,
    /// but to stop if more than <see cref="maxFailureCount"/> data items fail.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TheoryWithLimitedFailuresAttribute : TheoryAttribute
    {
        private readonly int maxFailureCount;
        private int failureCount;

        public TheoryWithLimitedFailuresAttribute(int maxFailureCount)
        {
            this.maxFailureCount = maxFailureCount;
        }

        protected override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo method)
        {
            return base.EnumerateTestCommands(method).Select(c => new CancellingCommand(c, this)).TakeWhile(_ => failureCount < maxFailureCount);
        }

        private void OnFailure()
        {
            failureCount++;
        }

        private class CancellingCommand : DelegatingTestCommand
        {
            private readonly TheoryWithLimitedFailuresAttribute owner;

            public CancellingCommand(ITestCommand innerCommand, TheoryWithLimitedFailuresAttribute owner)
                : base(innerCommand)
            {
                this.owner = owner;
            }

            public override MethodResult Execute(object testClass)
            {
                try
                {
                    return InnerCommand.Execute(testClass);
                }
                catch
                {
                    owner.OnFailure();
                    throw;
                }
            }
        }
    }
}