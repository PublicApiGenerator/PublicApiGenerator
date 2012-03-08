using System;
using ApiApprover;
using ApprovalTests;
using ApprovalTests.Reporters;

namespace ApiApprover
{
    public class PublicApiApprovalTest
    {
        [Test]
        [UseReporter(typeof(DiffReporter))] 
        public void approve_public_api()
        {
            // arrange
            var assembly = typeof(SOMETYPE).Assembly;

            // act
            var publicApi = PublicApiGenerator.CreatePublicApiForAssembly(assembly);

            // assert
            Approvals.Verify(publicApi);
        }
    }
}