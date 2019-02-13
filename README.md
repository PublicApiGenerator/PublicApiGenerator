[![Build status](https://ci.appveyor.com/api/projects/status/5vdwwducje0miayf?svg=true)](https://ci.appveyor.com/project/JakeGinnivan/apiapprover)

# ApiApprover

_This package is obsoleted with a compiler warning_ 

Api Approver is a simple NuGet package built on top of [ApprovalTests.Net](https://github.com/approvals/ApprovalTests.Net) which approves the public API of an assembly.

Whenever the public API changes the Api Approver test will fail. Running the test manually will pop up a diff tool allowing you to see the changes. If the changes are fine (i.e an added overload) then the changes can simply be approved and test will pass again.

![ApiApprover](http://jake.ginnivan.net/assets/posts/2012-02-19-apiapprover/ApiChange.png)

There are times though that changes to the public API is accidental. Api Approver simply adds a step that all public API changes have to be reviewed by a developer and accepted, saving accidental breaking changes being shipped.

## PublicApiGenerator

PublicApiGenerator has no dependencies and simply creates a string the represents the public API. Any approval library can be used to approve the generated public API.

## How do I use it

> Install-package PublicApiGenerator

``` csharp
var publicApi = ApiGenerator.GeneratePublicApi(typeof(Library).Assembly);
```

### Manual

``` csharp
[Fact]
public void my_assembly_has_no_public_api_changes()
{
    var publicApi = ApiGenerator.GeneratePublicApi(typeof(Library).Assembly);

    var approvedFilePath = "PublicApi.approved.txt";
    if (!File.Exists(approvedFilePath))
    {
        // Create a file to write to.
        using (var sw = File.CreateText(approvedFilePath)) { }
    }

    var approvedApi = File.ReadAllText(approvedFilePath);

    Assert.Equal(approvedApi, publicApi);
}
```

### Shoudly

> Install-package Shouldly

``` csharp
[Fact]
public void my_assembly_has_no_public_api_changes()
{
    var publicApi = ApiGenerator.GeneratePublicApi(typeof(Library).Assembly);

    //Shouldly
    publicApi.ShouldMatchApproved();
}
```

### ApprovalTests

> Install-package ApprovalTests

``` csharp
[Fact]
public void my_assembly_has_no_public_api_changes()
{
    var publicApi = ApiGenerator.GeneratePublicApi(typeof(Library).Assembly);;
    var writer = new ApprovalTextWriter(publicApi, "txt");
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
```