using System;
using System.IO;
using System.Reflection;
using ApprovalTests;
using ApprovalTests.Namers;

namespace ApiApprover
{
    [Obsolete("The `ApiApprover` package will be removed in the next major version. Install the `PublicApiGenerator` package and use the `ApiGenerator` class directly instead. More information can be found in README.", false)]
    public static class PublicApiApprover
    {
        [Obsolete("The `ApiApprover` package will be removed in the next major version. Install the `PublicApiGenerator` package and use the `ApiGenerator` class directly instead. More information can be found in README.", false)]
        public static void ApprovePublicApi(Assembly assembly)
        {
            var publicApi = PublicApiGenerator.ApiGenerator.GeneratePublicApi(assembly);
            var writer = new ApprovalTextWriter(publicApi, "cs");
            var approvalNamer = new AssemblyPathNamer(assembly.Location);
            Approvals.Verify(writer, approvalNamer, Approvals.GetReporter());
        }

        private class AssemblyPathNamer : UnitTestFrameworkNamer
        {
            private readonly string name;

            public AssemblyPathNamer(string assemblyPath)
            {
                name = Path.GetFileNameWithoutExtension(assemblyPath);
            }

            public override string Name => name;
        }
    }
}
