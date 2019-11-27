namespace PublicApiGenerator.Tool
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using Process = System.Diagnostics.Process;

    /// <summary>
    /// Program for dotnet tool
    /// </summary>
    public static class Program
    {
        /// <param name="targetFrameworks">Target frameworks to use to restore packages in. Must be a suitable target framework for executables like netcoreapp2.1. It is possible to specify multiple target frameworks like netcoreapp2.1;net461</param>
        /// <param name="assembly">The assembly name including the extension (i.ex. PublicApiGenerator.dll) to generate a public API from in case in differs from the package name.</param>
        /// <param name="projectPath">The path to the csproj that should be used to build the public API.</param>
        /// <param name="package">The package name from which a public API should be created. The tool assumes the package name equals the assembly name. If the assembly name is different specify <paramref name="assembly"/></param>
        /// <param name="packageVersion">The version of the package defined in <paramref name="package"/> to be used.</param>
        /// <param name="generatorVersion">The version of the PublicApiGenerator package to use.</param>
        /// <param name="workingDirectory">The working directory to be used for temporary work artifacts. A temporary directory will be created inside the working directory and deleted once the process is done. If no working directory is specified the users temp directory is used.</param>
        /// <param name="outputDirectory">The output directory where the generated public APIs should be moved.</param>
        /// <param name="verbose"></param>
        /// <param name="leaveArtifacts"></param>
        /// <returns></returns>
        static int Main(string targetFrameworks, string? assembly = null, string? projectPath = null, string? package = null, string? packageVersion = null, string? generatorVersion = null, string? workingDirectory = null, string? outputDirectory = null, bool verbose = false, bool leaveArtifacts = false)
        {
            if (string.IsNullOrEmpty(outputDirectory))
            {
                outputDirectory = Environment.CurrentDirectory;
            }

            if (string.IsNullOrEmpty(generatorVersion))
            {
                generatorVersion = $"{Assembly.GetEntryAssembly().GetName().Version.Major}.*";
            }

            var workingArea = !string.IsNullOrEmpty(workingDirectory) ?
                Path.Combine(workingDirectory, Path.GetRandomFileName()) :
                Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            if (verbose)
            {
                Console.WriteLine($"Working area: {workingArea}");
            }

            try
            {
                AssertInputParameters(targetFrameworks, projectPath, package, packageVersion, workingArea, assembly);

                var template = CreateProjectTemplate(targetFrameworks, projectPath, package, packageVersion, generatorVersion!);

                SaveProjectTemplate(workingArea, template, verbose);

                foreach (var framework in targetFrameworks.Split(";"))
                {
                    GeneratePublicApi(assembly, package, workingArea, framework, outputDirectory, verbose);
                }

                return 0;
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"Configuration error: {e.Message}");
                return 1;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Failed: {e}");
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

        private static void GeneratePublicApi(string? assembly, string? package, string workingArea, string framework, string? outputDirectory, bool verbose)
        {
            var relativePath = Path.Combine(workingArea, "bin", "Release", framework);
            var name = !string.IsNullOrEmpty(assembly) ? $"{assembly}" : $"{package}.dll";
            relativePath = Path.Combine(relativePath, name);
            var fullPath = Path.GetFullPath(relativePath);
            var outputPath = Path.Combine(Path.GetDirectoryName(relativePath), $"{Path.GetFileNameWithoutExtension(name)}.{framework}.received.txt");

            try
            {
                // Because we run in different appdomain we can always unload
                RunDotnet(workingArea, verbose, $"run --configuration Release --framework {framework} -- {fullPath} {outputPath} {outputDirectory}");

                if (verbose)
                {
                    Console.WriteLine($"Public API file: {outputPath}");
                    Console.WriteLine();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Unable to find {fullPath}. Consider specifying --assembly");
                throw;
            }
        }

        private static void RunDotnet(string workingArea, bool verbose, string arguments)
        {
            if (verbose)
            {
                Console.WriteLine($"Dotnet arguments: {arguments}");
                Console.WriteLine();
            }

            var psi = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = arguments,
                WorkingDirectory = workingArea,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            var process = Process.Start(psi);
            process.WaitForExit(10000);

            var output = process.StandardOutput.ReadToEnd();

            if (verbose)
            {
                Console.WriteLine($"Dotnet output: {output}");
                Console.WriteLine();
            }

            if (process.ExitCode != 0)
            {
                var error = process.StandardError.ReadToEnd();
                throw new Exception(
                    $"dotnet exit code {process.ExitCode}. Directory: {workingArea}. Args: {arguments}. Output: {output}. Error: {error}");
            }
        }

        private static void SaveProjectTemplate(string workingArea, string template, bool verbose)
        {
            Directory.CreateDirectory(workingArea);
            var fullPath = Path.Combine(workingArea, "project.csproj");
            using (var output = File.CreateText(fullPath))
            {
                if (verbose)
                {
                    Console.WriteLine($"Project output path: {fullPath}");
                    Console.WriteLine($"Project template: {template}");
                    Console.WriteLine();
                }

                output.Write(template);
            }

            fullPath = Path.Combine(workingArea, "Program.cs");
            using (var output = File.CreateText(fullPath))
            {
                if (verbose)
                {
                    Console.WriteLine($"Program output path: {fullPath}");
                    Console.WriteLine($"Program template: {ProgramMain}");
                    Console.WriteLine();
                }

                output.Write(ProgramMain);
            }
        }

        private static string CreateProjectTemplate(string targetFrameworks, string? project, string? package, string? packageVersion, string generatorVersion)
        {
            return ProjectTemplate
                .Replace("{TargetFrameworks}", targetFrameworks)
                .Replace("{PublicApiGeneratorVersion}", generatorVersion)
                .Replace("{PackageReference}", string.IsNullOrEmpty(package) ? string.Empty : PackageReferenceTemplate
                    .Replace("{PackageName}", package)
                    .Replace("{PackageVersion}", packageVersion)
                )
                .Replace("{ProjectReference}", string.IsNullOrEmpty(project) ? string.Empty : ProjectReferenceTemplate
                    .Replace("{ProjectFile}", Path.GetFullPath(project))
                );
        }

        private static void AssertInputParameters(string targetFrameworks, string? project, string? package,
            string? packageVersion, string workingArea, string? assembly)
        {
            if (string.IsNullOrEmpty(targetFrameworks))
            {
                throw new InvalidOperationException("Specify the target frameworks like 'netcoreapp2.1;net461' or 'netcoreapp2.1'.");
            }

            if (!string.IsNullOrEmpty(package) && string.IsNullOrEmpty(packageVersion))
            {
                throw new InvalidOperationException("When using the package switch the package-version switch needs to be specified.");
            }

            if (!string.IsNullOrEmpty(package) && !string.IsNullOrEmpty(project))
            {
                throw new InvalidOperationException(
                    "When using the package name the project-path switch cannot be used or vice versa.");
            }

            if (!string.IsNullOrEmpty(project) && string.IsNullOrEmpty(assembly))
            {
                throw new InvalidOperationException(
                    "When using the project-path switch the output assembly name has to be specified with --assembly.");
            }

            if (File.Exists(workingArea) || Directory.Exists(workingArea))
            {
                throw new InvalidOperationException($"{workingArea} already exists");
            }
        }

        private static readonly string ProjectTemplate = @"<Project Sdk=""Microsoft.NET.Sdk"">
    <PropertyGroup>
        <OutputType>Exe</OutputType>
        <TargetFrameworks>{TargetFrameworks}</TargetFrameworks>
        <CopyLocalLockFileAssemblies>true</CopyLocalLockFileAssemblies>
    </PropertyGroup>

    <ItemGroup>
        <PackageReference Include=""PublicApiGenerator"" Version=""{PublicApiGeneratorVersion}"" />{PackageReference}
    </ItemGroup >
    {ProjectReference}
</Project >";

        private static readonly string PackageReferenceTemplate =
            @"
        <PackageReference Include=""{PackageName}"" Version=""{PackageVersion}"" />";

        private static readonly string ProjectReferenceTemplate =
            @"<ItemGroup>
        <ProjectReference Include=""{ProjectFile}"" />
    </ItemGroup >";

        private static readonly string ProgramMain = @"using System;
using System.Reflection;
using System.IO;
using PublicApiGenerator;

public static class Program
{
    public static void Main(string[] args)
    {
        var fullPath = args[0];
        var outputPath = args[1];
        var outputDirectory = args[2];
        var asm = Assembly.LoadFile(fullPath);
        File.WriteAllText(outputPath, asm.GeneratePublicApi());
        var destinationFilePath = Path.Combine(outputDirectory, Path.GetFileName(outputPath));
        if (File.Exists(destinationFilePath))
        {
            File.Delete(destinationFilePath);
        }
        File.Move(outputPath, destinationFilePath);
    }
}
";
    }
}
