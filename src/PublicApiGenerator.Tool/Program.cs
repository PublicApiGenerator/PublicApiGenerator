using System.Diagnostics;
using System.Xml.Linq;
using Process = System.Diagnostics.Process;

namespace PublicApiGenerator.Tool;

/// <summary>
/// Program for dotnet tool.
/// </summary>
public static class Program
{
    /// <summary>
    /// Public API generator tool that is useful for semantic versioning.
    /// </summary>
    /// <param name="targetFrameworks">Target frameworks to use to restore packages in. Must be a suitable target framework for executables like netcoreapp2.1. It is possible to specify multiple target frameworks like netcoreapp2.1;net461.</param>
    /// <param name="assembly">The assembly name including the extension (i.ex. PublicApiGenerator.dll) to generate a public API from in case it differs from the package name.</param>
    /// <param name="projectPath">The path to the csproj that should be used to build the public API.</param>
    /// <param name="package">The package name from which a public API should be created. The tool assumes the package name equals the assembly name. If the assembly name is different specify <paramref name="assembly"/>.</param>
    /// <param name="packageVersion">The version of the package defined in <paramref name="package"/> to be used.</param>
    /// <param name="packageSource">Package source or feed to use (multiple allowed).</param>
    /// <param name="generatorVersion">The version of the PublicApiGenerator package to use.</param>
    /// <param name="workingDirectory">The working directory to be used for temporary work artifacts. A temporary directory will be created inside the working directory and deleted once the process is done. If no working directory is specified the users temp directory is used.</param>
    /// <param name="outputDirectory">The output directory where the generated public APIs should be moved.</param>
    /// <param name="verbose">Prints to stderr detailed information about what's going on behind the scenes.</param>
    /// <param name="leaveArtifacts">Instructs to leave the temporary artifacts around for debugging and troubleshooting purposes.</param>
    /// <param name="waitTimeInSeconds">The number of seconds to wait for the API generation process to end. If multiple target frameworks are used the wait time is applied per target framework.</param>
    /// <returns></returns>
    internal static int Main(
        string targetFrameworks,
        string? assembly = null,
        string? projectPath = null,
        string? package = null,
        string? packageVersion = null,
        ICollection<string>? packageSource = null,
        string? generatorVersion = null,
        string? workingDirectory = null,
        string? outputDirectory = null,
        bool verbose = false,
        bool leaveArtifacts = false,
        int waitTimeInSeconds = 60)
    {
        var logError = Console.Error;
        var logVerbose = verbose ? Console.Error : TextWriter.Null;

        string workingArea = string.IsNullOrEmpty(workingDirectory)
            ? Path.Combine(Path.GetTempPath(), Path.GetRandomFileName())
            : Path.Combine(workingDirectory, Path.GetRandomFileName());

        logVerbose.WriteLine($"Working area: {workingArea}");

        try
        {
            AssertInputParameters(targetFrameworks, projectPath, package, packageVersion, workingArea, assembly);

            string[] frameworks = targetFrameworks.Split(";");

            if (string.IsNullOrEmpty(outputDirectory) && frameworks.Length > 1)
            {
                outputDirectory = Environment.CurrentDirectory;
            }

            if (string.IsNullOrEmpty(generatorVersion))
            {
                generatorVersion = $"{typeof(Program).Assembly.GetName().Version?.Major}.*";
            }

            var project = CreateProject(targetFrameworks, projectPath, package, packageVersion, packageSource, generatorVersion!);

            SaveProject(workingArea, project, logVerbose);

            foreach (string framework in frameworks)
            {
                GeneratePublicApi(assembly, package, workingArea, framework, outputDirectory, waitTimeInSeconds, logVerbose, logError);
            }

            return 0;
        }
        catch (ArgumentException e)
        {
            logError.WriteLine($"Configuration error: {e.Message}");
            return 1;
        }
        catch (Exception e)
        {
            logError.WriteLine($"Failed: {e}");
            return 1;
        }
        finally
        {
            if (Directory.Exists(workingArea) && !leaveArtifacts)
            {
                Directory.Delete(workingArea, true);
            }
        }
    }

    private static void GeneratePublicApi(
        string? assembly,
        string? package,
        string workingArea,
        string framework,
        string? outputDirectory,
        int waitTimeInSeconds,
        TextWriter logVerbose,
        TextWriter logError)
    {
        string name = string.IsNullOrEmpty(assembly) ? $"{package}.dll" : $"{assembly}";
        string assemblyPath = Path.GetFullPath(Path.Combine(workingArea, "bin", "Release", framework, name));

        string? apiFilePath = outputDirectory == null
            ? null
            : Path.Combine(workingArea, $"{Path.GetFileNameWithoutExtension(name)}.{framework}.received.txt");

        try
        {
            // Because we run in different appdomain we can always unload
            RunDotnet(
                workingArea,
                waitTimeInSeconds,
                logVerbose,
                apiFilePath == null ? Console.Out : null,
                "run",
                "--configuration", "Release",
                "--framework", framework,
                "--",
                assemblyPath,
                apiFilePath ?? "-");
        }
        catch (FileNotFoundException)
        {
            logError.WriteLine($"Unable to find {assemblyPath}. Consider specifying --assembly switch.");
            throw;
        }

        if (outputDirectory == null || apiFilePath == null)
        {
            return;
        }

        logVerbose.WriteLine($"Public API temporary file: {apiFilePath}");
        logVerbose.WriteLine();

        string destinationFilePath = Path.Combine(outputDirectory, Path.GetFileName(apiFilePath));

        if (File.Exists(destinationFilePath))
            File.Delete(destinationFilePath);
        File.Move(apiFilePath, destinationFilePath);

        logVerbose.WriteLine($"Public API output file: {Path.GetFullPath(destinationFilePath)}");
    }

    private static void RunDotnet(
        string workingArea,
        int waitTimeInSeconds,
        TextWriter logVerbose,
        TextWriter? stdout,
        params string[] arguments)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "dotnet",
            WorkingDirectory = workingArea,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };

        logVerbose.WriteLine($"Invoking dotnet with arguments:");
        foreach (var (i, arg) in arguments.Select((arg, i) => (i + 1, arg)))
        {
            logVerbose.WriteLine($"{i,4}. {arg}");
            psi.ArgumentList.Add(arg);
        }
        logVerbose.WriteLine();
        logVerbose.WriteLine($"Process timeout: '{waitTimeInSeconds}' seconds.");

        using var process = Process.Start(psi) ?? throw new Exception("No process resource is started.");

        if (stdout == null)
        {
            logVerbose.WriteLine("Dotnet output:");
        }

        const string indent = "  ";
        process.OutputDataReceived += DataReceivedEventHandler(stdout ?? logVerbose, stdout == null ? indent : null);
        process.ErrorDataReceived += DataReceivedEventHandler(logVerbose, indent);

        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        if (stdout == null)
        {
            logVerbose.WriteLine();
        }

        if (!process.WaitForExit(waitTimeInSeconds * 1000))
        {
            throw new TimeoutException($"Process \"{psi.FileName}\" ({process.Id}) took too long to run (timeout exceeded {waitTimeInSeconds} seconds).");
        }

        if (process.ExitCode != 0)
        {
            string pseudoCommandLine = string.Join(" ", from arg in arguments
                                                        select arg.IndexOf('"') >= 0
                                                            ? $"\"{arg.Replace("\"", "\"\"")}\""
                                                            : arg);
            throw new Exception($"dotnet exit code {process.ExitCode}. Directory: {workingArea}. Args: {pseudoCommandLine}.");
        }

        static DataReceivedEventHandler DataReceivedEventHandler(TextWriter writer, string? prefix = null) =>
            (_, args) =>
            {
                if (args.Data != null)
                    writer.WriteLine(prefix + args.Data);
            };
    }

    private static void SaveProject(string workingArea, XElement project, TextWriter logVerbose)
    {
        Directory.CreateDirectory(workingArea);
        string fullPath = Path.Combine(workingArea, "project.csproj");
        using (var output = File.CreateText(fullPath))
        {
            logVerbose.WriteLine($"Project output path: {fullPath}");
            logVerbose.WriteLine($"Project template: {project}");
            logVerbose.WriteLine();

            output.Write(project);
        }

        string programMain = typeof(Program).GetManifestResourceText("SubProgram.cs");

        fullPath = Path.Combine(workingArea, "Program.cs");
        using (var output = File.CreateText(fullPath))
        {
            logVerbose.WriteLine($"Program output path: {fullPath}");
            logVerbose.WriteLine($"Program template: {programMain}");
            logVerbose.WriteLine();

            output.Write(programMain);
        }
    }

    private static string GetManifestResourceText(this Type type, string name)
    {
        using var stream = type.Assembly.GetManifestResourceStream(type, name) ?? throw new Exception($"Resource named \"{type.Namespace}.{name}\" not found.");
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    private static XElement CreateProject(
        string targetFrameworks,
        string? projectPath,
        string? package,
        string? packageVersion,
        ICollection<string>? packageSource,
        string generatorVersion)
    {
        return
            new XElement("Project", new XAttribute("Sdk", "Microsoft.NET.Sdk"),
                new XElement("PropertyGroup",
                    new XElement("OutputType", "Exe"),
                    new XElement("TargetFrameworks", targetFrameworks),
                    new XElement("SuppressTfmSupportBuildWarnings", true),
                    new XElement("CopyLocalLockFileAssemblies", "true"),
                packageSource?.Count > 0
                    ? new XElement("RestoreAdditionalProjectSources", string.Join(";", packageSource))
                    : null),
                new XElement("ItemGroup",
                    PackageReference(nameof(PublicApiGenerator), generatorVersion),
                    string.IsNullOrEmpty(package) ? null : PackageReference(package, packageVersion!),
                    string.IsNullOrEmpty(projectPath) ? null : new XElement("ProjectReference", new XAttribute("Include", projectPath))));

        static XElement PackageReference(string id, string version) =>
            new("PackageReference", new XAttribute("Include", id), new XAttribute("Version", version));
    }

    private static void AssertInputParameters(
        string targetFrameworks,
        string? projectPath,
        string? package,
        string? packageVersion,
        string workingArea,
        string? assembly)
    {
        if (string.IsNullOrEmpty(targetFrameworks))
        {
            throw new ArgumentException("Specify the target frameworks switch like --target-frameworks \"netcoreapp2.1;net461\" or --target-frameworks netcoreapp2.1.");
        }

        if (!string.IsNullOrEmpty(package) && string.IsNullOrEmpty(packageVersion))
        {
            throw new ArgumentException("When using the --package switch the --package-version switch needs to be specified.");
        }

        if (!string.IsNullOrEmpty(package) && !string.IsNullOrEmpty(projectPath))
        {
            throw new ArgumentException("When using the --package switch the --project-path switch cannot be used or vice versa.");
        }

        if (!string.IsNullOrEmpty(projectPath) && string.IsNullOrEmpty(assembly))
        {
            throw new ArgumentException("When using the --project-path switch the output assembly name has to be specified with --assembly switch.");
        }

        if (File.Exists(workingArea) || Directory.Exists(workingArea))
        {
            throw new ArgumentException($"{workingArea} already exists, check --working-directory switch.");
        }
    }
}
