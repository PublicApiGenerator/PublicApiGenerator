using System;
using System.IO;
using System.Reflection;
using ApprovalTests;
using ApprovalTests.Namers;

namespace ApiApprover
{
    [Obsolete("The package `ApiApprover` will be removed in the next major version. Install the package `PublicApiGenerator` and use the `ApiGenerator` directly or copy https://github.com/JakeGinnivan/ApiApprover/blob/master/src/ApiApprover/PublicApiApprover.cs into your repository if you plan to continue to use ApprovalTests in combination with the `ApiGenerator`.", false)]
    public static class PublicApiApprover
    {
        [Obsolete("The package `ApiApprover` will be removed in the next major version. Install the package `PublicApiGenerator` and use the `ApiGenerator` directly or copy https://github.com/JakeGinnivan/ApiApprover/blob/master/src/ApiApprover/PublicApiApprover.cs into your repository if you plan to continue to use ApprovalTests in combination with the `ApiGenerator`.", false)]
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

            public override string Name
            {
                get { return name; }
            }
        }
    }
}