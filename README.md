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

### Shouldly

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

## PublicApi Generator Global Tool

### Install

```
dotnet tool install -g PublicApiGenerator.Tool
```

### Update

```
dotnet tool update -g PublicApiGenerator.Tool
```

### Remove

```
dotnet tool uninstall -g PublicApiGenerator.Tool
```

### Examples

Generate public API for fluent assertions 5.6.0 for runtime framework `netcoreapp2.1` and `net461`

```
generate-public-api --target-frameworks 'netcoreapp2.1;net461' --package FluentAssertions --package-version 5.6.0
```

Generate public API for fluent assertions 5.* for runtime framework `net47`

```
generate-public-api --target-frameworks 'netcoreapp2.1;net461' --package FluentAssertions --package-version 5.*
```

Generate public API for NServiceBus 7.1.4 for runtime framework `netcoreapp2.2` and `net452`. Note NServiceBus package doesn't contain NServiceBus.dll and therefore it is required to specify the assembly that contains the public API.

```
generate-public-api --target-frameworks 'netcoreapp2.2;net452' --package NServiceBus --package-version 7.1.4 --assembly NServiceBus.Core.dll
```

### Command line arguments

```
--target-frameworks framework|'framework;framework'
```

The target framework in which the package will be restored. The target framework is also used as runtime to generate the public API. It is required to specify a valid runtime framework. For example

- 'netcoreapp2.2;net452' to build a public API for `netcoreapp2.2` and `net452`
- 'netcoreapp2.1;net461' to build a public API for `netcoreapp2.1` and `net461`
- `net47` to build a public API for `net47`

It is not possible to use `netstandard2.0` because it is not a valid runtime framework.

```
--package-name PackageName
```

A valid nuget package name. For example

- FluentAssertions
- NServiceBus

When the `--package-name` switch is used the `--package-version` switch is mandatory.

```
--package-version Version
```

A nuget package version or floating versions as specified by https://docs.microsoft.com/en-us/nuget/consume-packages/package-references-in-project-files. For example

- 8.1.1
- 8.0
- 8.0-*

```
--assembly Assembly.dll
```

The assembly name including the extension to generate a public API from in case in differs from the package name. For example

- PublicApiGenerator.dll
- NServiceBus.Core.dll

```
--project-path
``` 

A path to a csproj file that contains the public API that needs to be built. By default a release build will be generated. When the project-path switch is used it is required to specify the `--assembly` switch to point to the output assembly that contains the public API. For example

- '..\PublicApiGenerator\PublicApiGenerator.csproj`'

```
--working-directory Path
```

The temporary working directory to be used to generate the work artifacts.

```
--output-directory Path
```

The output directory where public API text-files should be moved. The end with `*.received.txt`

```
--generator-version Version
```

By default latest stable release version of PubliApiGenerator will be used in the global tool to generate the public API with in the major range of the global tool. It is possible to override the PublicApiGenerator version by specifying the version to be used in this switch. For example

- 8.1.0

```
--verbose
```

Detailed information about what's going on behind the scenes

```
leave-artifacts
```

For troubleshooting purposes it might be necessary to look into the temporary work artifacts. By specifying this switch the temporary csproj files and all the temp folders are not deleted after a run. Be aware this might significantly decrease the available disk space because all artifacts including the compile time artifacts are not deleted.