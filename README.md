# ApiApprover
Api Approver is a simple NuGet package built on top of [ApprovalTests.Net](https://github.com/approvals/ApprovalTests.Net) which approves the public API of an assembly.

Whenever the public API changes the Api Approver test will fail. Running the test manually will pop up a diff tool allowing you to see the changes. If the changes are fine (i.e an added overload) then the changes can simply be approved and test will pass again.

![ApiApprover](http://jake.ginnivan.net/assets/posts/2012-02-19-apiapprover/ApiChange.png)

There are times though that changes to the public API is accidental. Api Approver simply adds a step that all public API changes have to be reviewed by a developer and accepted, saving accidental breaking changes being shipped.

## How do I use it
> Install-package ApiApprover

``` csharp
[Fact]
[UseReporter(typeof(DiffReporter))]
public void my_assembly_has_no_public_api_changes()
{
	PublicApiApprover.ApprovePublicApi(typeof(Application).Assembly.Location);
}
```

[See this example](https://github.com/JakeGinnivan/ApiApprover/blob/master/src/ApiApprover/ExampleApiApprovalTest.cs) of a more advanced test you can write using this library.
