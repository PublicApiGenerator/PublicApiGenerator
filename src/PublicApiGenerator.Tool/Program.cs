namespace PublicApiGenerator.Tool
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Xml.Linq;
    using Process = System.Diagnostics.Process;

    /// <summary>
    /// Program for dotnet tool
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Public API generator tool that is useful for semantic versioning
        /// </summary>
        /// <param name="targetFrameworks">Target frameworks to use to restore packages in. Must be a suitable target framework for executables like netcoreapp2.1. It is possible to specify multiple target frameworks like netcoreapp2.1;net461</param>
        /// <param name="assembly">The assembly name including the extension (i.ex. PublicApiGenerator.dll) to generate a public API from in case in differs from the package name.</param>
        /// <param name="projectPath">The path to the csproj that should be used to build the public API.</param>
        /// <param name="package">The package name from which a public API should be created. The tool assumes the package name equals the assembly name. If the assembly name is different specify <paramref name="assembly"/></param>
        /// <param name="packageVersion">The version of the package defined in <paramref name="package"/> to be used.</param>
        /// <param name="packageSource">Package source or feed to use (multiple allowed).</param>
        /// <param name="generatorVersion">The version of the PublicApiGenerator package to use.</param>
        /// <param name="workingDirectory">The working directory to be used for temporary work artifacts. A temporary directory will be created inside the working directory and deleted once the process is done. If no working directory is specified the users temp directory is used.</param>
        /// <param name="outputDirectory">The output directory where the generated public APIs should be moved.</param>
        /// <param name="verbose"></param>
        /// <param name="leaveArtifacts">Instructs to leave the temporary artifacts around for debugging and troubleshooting purposes.</param>
        /// <param name="waitTimeInSeconds">The number of seconds to wait for the API generation process to end. If multiple target frameworks are used the wait time is applied per target framework.</param>
        /// <returns></returns>
        static int Main(string targetFrameworks,
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
            int? waitTimeInSeconds = null)
        {
            var logError = Console.Error;
            var logVerbose = verbose ? Console.Error : TextWriter.Null;
            var processWaitTimeInSeconds = waitTimeInSeconds ?? 60;

            var workingArea = !string.IsNullOrEmpty(workingDirectory)
                ? Path.Combine(workingDirectory, Path.GetRandomFileName())
                : Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            logVerbose.WriteLine($"Working area: {workingArea}");

            try
            {
                AssertInputParameters(targetFrameworks, projectPath, package, packageVersion, workingArea, assembly);

                var frameworks = targetFrameworks.Split(";");

                if (string.IsNullOrEmpty(outputDirectory) && frameworks.Length > 1)
                {
                    outputDirectory = Environment.CurrentDirectory;
                }

                if (string.IsNullOrEmpty(generatorVersion))
                {
                    generatorVersion = $"{Assembly.GetEntryAssembly().GetName().Version.Major}.*";
                }

                var project = CreateProject(targetFrameworks, projectPath, package, packageVersion, packageSource, generatorVersion!);

                SaveProject(workingArea, project, logVerbose);

                foreach (var framework in frameworks)
                {
                    GeneratePublicApi(assembly, package, workingArea, framework, outputDirectory, processWaitTimeInSeconds, logVerbose, logError);
                }

                return 0;
            }
            catch (InvalidOperationException e)
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

        private static void GeneratePublicApi(string? assembly, string? package, string workingArea, string framework, string? outputDirectory, int waitTimeInSeconds, TextWriter logVerbose, TextWriter logError)
        {
            var relativePath = Path.Combine(workingArea, "bin", "Release", framework);
            var name = !string.IsNullOrEmpty(assembly) ? $"{assembly}" : $"{package}.dll";
            relativePath = Path.Combine(relativePath, name);
            var assemblyPath = Path.GetFullPath(relativePath);

            var apiFilePath = outputDirectory != null
                ? Path.Combine(workingArea, $"{Path.GetFileNameWithoutExtension(name)}.{framework}.received.txt")
                : null;

            try
            {
                // Because we run in different appdomain we can always unload
                RunDotnet(workingArea, waitTimeInSeconds, logVerbose,
                          apiFilePath != null ? null : Console.Out,
                          "run",
                          "--configuration", "Release",
                          "--framework", framework,
                          "--",
                          assemblyPath, apiFilePath ?? "-");
            }
            catch (FileNotFoundException)
            {
                logError.WriteLine($"Unable to find {assemblyPath}. Consider specifying --assembly");
                throw;
            }

            if (outputDirectory == null || apiFilePath == null)
            {
                return;
            }

            logVerbose.WriteLine($"Public API file: {apiFilePath}");
            logVerbose.WriteLine();

            var destinationFilePath = Path.Combine(outputDirectory, Path.GetFileName(apiFilePath));

            if (File.Exists(destinationFilePath))
                File.Delete(destinationFilePath);
            File.Move(apiFilePath, destinationFilePath);

            Console.WriteLine(Path.GetFullPath(destinationFilePath));
        }

        private static void RunDotnet(string workingArea, int waitTimeInSeconds, TextWriter logVerbose, TextWriter? stdout, params string[] arguments)
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

            using var process = Process.Start(psi);

            if (stdout == null)
            {
                logVerbose.WriteLine("Dotnet output:");
            }

            const string indent = "  ";
            process.OutputDataReceived += DataReceivedEventHandler(stdout ?? logVerbose, stdout == null ? indent : null);
            process.ErrorDataReceived  += DataReceivedEventHandler(logVerbose, indent);

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            if (stdout == null)
            {
                logVerbose.WriteLine();
            }

            if (!process.WaitForExit(waitTimeInSeconds * 1000))
            {
                throw new TimeoutException($"Process \"{psi.FileName}\" ({process.Id}) took too long to run.");
            }

            if (process.ExitCode != 0)
            {
                var pseudoCommandLine = string.Join(" ", from arg in arguments
                                                         select arg.IndexOf('"') >= 0
                                                              ? $"\"{arg.Replace("\"", "\"\"")}\""
                                                              : arg);
                throw new Exception(
                    $"dotnet exit code {process.ExitCode}. Directory: {workingArea}. Args: {pseudoCommandLine}.");
            }

            static DataReceivedEventHandler
                DataReceivedEventHandler(TextWriter writer, string? prefix = null) =>
                (_, args) =>
                {
                    if (args.Data == null)
                    {
                        return; // EOI
                    }

                    writer.WriteLine(prefix + args.Data);
                };
        }

        private static void SaveProject(string workingArea, XElement project, TextWriter logVerbose)
        {
            Directory.CreateDirectory(workingArea);
            var fullPath = Path.Combine(workingArea, "project.csproj");
            using (var output = File.CreateText(fullPath))
            {
                logVerbose.WriteLine($"Project output path: {fullPath}");
                logVerbose.WriteLine($"Project template: {project}");
                logVerbose.WriteLine();

                output.Write(project);
            }

            var programMain = typeof(Program).GetManifestResourceText("SubProgram.cs");

            fullPath = Path.Combine(workingArea, "Program.cs");
            using (var output = File.CreateText(fullPath))
            {
                logVerbose.WriteLine($"Program output path: {fullPath}");
                logVerbose.WriteLine($"Program template: {programMain}");
                logVerbose.WriteLine();

                output.Write(programMain);
            }
        }

        private static string GetManifestResourceText(this Type type, string name,
                                                      Encoding? encoding = null)
        {
            using var stream = type.Assembly.GetManifestResourceStream(type, name);
            if (stream == null)
            {
                throw new Exception($"Resource named \"{type.Namespace}.{name}\" not found.");
            }

            using var reader = encoding == null ? new StreamReader(stream)
                                                : new StreamReader(stream, encoding);
            return reader.ReadToEnd();
        }

        private static XElement CreateProject(string  targetFrameworks,
                                              string? project,
                                              string? package,
                                              string? packageVersion,
                                              ICollection<string>? packageSource,
                                              string  generatorVersion)
        {
            return
                new XElement("Project", new XAttribute("Sdk", "Microsoft.NET.Sdk"),
                    new XElement("PropertyGroup",
                        new XElement("OutputType", "Exe"),
                        new XElement("TargetFrameworks", targetFrameworks),
                        new XElement("CopyLocalLockFileAssemblies", "true"),
                    packageSource?.Count > 0
                        ? new XElement("RestoreAdditionalProjectSources", string.Join(";", packageSource))
                        : null),
                    new XElement("ItemGroup",
                        PackageReference(nameof(PublicApiGenerator), generatorVersion),
                        !string.IsNullOrEmpty(package) ? PackageReference(package!, packageVersion!) : null,
                        !string.IsNullOrEmpty(project) ? new XElement("ProjectReference", new XAttribute("Include", project)) : null));

            static XElement PackageReference(string id, string version) =>
                new XElement("PackageReference", new XAttribute("Include", id),
                                                 new XAttribute("Version", version));
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
    }
}
