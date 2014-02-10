using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit.Extensions;
using Xunit.Sdk;

namespace ApiApprover
{
    public class ApiTests
    {
        [Theory(Skip = "Oh yes")]
        [PropertyData("AssemblyPaths")]
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
                const string path = @"C:\work\sdk\v8.0\Bin";
                return Directory.EnumerateFiles(path, "*.dll")
                    .Select(Path.GetFileName)
                    .Where(f => f.StartsWith("JetBrains", StringComparison.OrdinalIgnoreCase) && !f.EndsWith("Tests.dll", StringComparison.OrdinalIgnoreCase))
                    .Select(f => new object[]{ f, path}).Take(1);
            }
        }
    }

    //public class Thing : DelegatingTestCommand
    //{
    //    public Thing(ITestCommand innerCommand) : base(innerCommand)
    //    {
    //    }

    //    public override MethodResult Execute(object testClass)
    //    {
    //    }
    //}
}