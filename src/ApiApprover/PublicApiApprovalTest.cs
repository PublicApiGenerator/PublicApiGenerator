using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;

namespace ApiApprover
{
    public class PublicApiApprovalTest
    {
        [Test]
        public void approve_public_api()
        {
            // arrange
            var assembly = typeof(SOMETYPE).Assembly;

            // act
            var publicApi = PublicApiGenerator.CreatePublicApiForAssembly(assembly);

            // assert
            var unitTestFrameworkNamer = new UnitTestFrameworkNamer();
            var diffReporter = new DiffReporter();
            Approvals.Approve(new ApprovalTextWriter(publicApi), unitTestFrameworkNamer, diffReporter);
        }
    }
}