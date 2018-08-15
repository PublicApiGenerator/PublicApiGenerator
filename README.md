[![Build status](https://ci.appveyor.com/api/projects/status/5vdwwducje0miayf?svg=true)](https://ci.appveyor.com/project/JakeGinnivan/apiapprover)

# ApiApprover
Api Approver is a simple NuGet package built on top of [ApprovalTests.Net](https://github.com/approvals/ApprovalTests.Net) which approves the public API of an assembly.

Whenever the public API changes the Api Approver test will fail. Running the test manually will pop up a diff tool allowing you to see the changes. If the changes are fine (i.e an added overload) then the changes can simply be approved and test will pass again.

![ApiApprover](http://jake.ginnivan.net/assets/posts/2012-02-19-apiapprover/ApiChange.png)

There are times though that changes to the public API is accidental. Api Approver simply adds a step that all public API changes have to be reviewed by a developer and accepted, saving accidental breaking changes being shipped.

## PublicApiGenerator

I am moving away from ApiApprover and just publishing PublicAPiGenerator, this has no dependencies and you can choose your own approval library.

Simply install `PublicApiGenerator`


## How do I use it
> Install-package ApiApprover

or

> Install-package Shouldly

``` csharp
[Fact]
public void my_assembly_has_no_public_api_changes()
{
	var publicApi = PublicApiApprover.GeneratePublicApi(typeof(Application).Assembly);

    // Use an approval framework like

    //Shouldly
    publicApi.ShouldMatchApproved();

    //ApprovalTests
    Approvals.Verify(publicApi);
}
```
