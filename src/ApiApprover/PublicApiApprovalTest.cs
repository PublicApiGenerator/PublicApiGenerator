using System.Collections.Generic;
using System.IO;
using Xunit.Extensions;

namespace ApiApprover
{
    public class ApiTests
    {
        [Theory]
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
                const string path = @"C:\temp";
                yield return new object[] { "myAssembly.dll", path };
            }
        }
    }}