namespace PublicApiGenerator.Cli
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Linq;
    using Process = System.Diagnostics.Process;

    static class Program
    {
        /// <summary>
        /// Public API generator tool that is useful for semantic versioning
        /// </summary>
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

        static int Main(string targetFrameworks,
                        string? assembly = null,
                        string? projectPath = null,
                        string? package = null,
                        string? packageVersion = null,
                        string? generatorVersion = null,
                        string? workingDirectory = null,
                        string? outputDirectory = null,
                        bool verbose = false,
                        bool leaveArtifacts = false)
        {
            var log = verbose ? Console.Error : null;

            var frameworks = targetFrameworks.Split(";");

            if (string.IsNullOrEmpty(outputDirectory) && frameworks.Length > 1)
                outputDirectory = Environment.CurrentDirectory;

            if (string.IsNullOrEmpty(generatorVersion))
                generatorVersion = $"{Assembly.GetEntryAssembly()!.GetName().Version!.Major}.*";

            var workingArea = !string.IsNullOrEmpty(workingDirectory) ?
                Path.Combine(workingDirectory, Path.GetRandomFileName()) :
                Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            log?.WriteLine($"Working area: {workingArea}");

            try
            {
                AssertInputParameters(targetFrameworks, projectPath, package, packageVersion, workingArea, assembly);

                var template = CreateProject(targetFrameworks, projectPath, package, packageVersion, generatorVersion!);

                SaveProject(workingArea, template, log);

                foreach (var framework in frameworks)
                    Console.WriteLine(GeneratePublicApi(assembly, package, workingArea, framework, outputDirectory, log));

                return 0;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.GetBaseException().Message);
                log?.WriteLine(e);
                return 1;
            }
            finally
            {
                if (!leaveArtifacts)
                    Directory.Delete(workingArea, true);
            }
        }

        static string? GeneratePublicApi(string? assembly, string? package, string workingArea, string framework, string? outputDirectory, TextWriter? log)
        {
            var relativePath = Path.Combine(workingArea, "bin", "Release", framework);
            var name = !string.IsNullOrEmpty(assembly) ? $"{assembly}" : $"{package}.dll";
            relativePath = Path.Combine(relativePath, name);
            var fullPath = Path.GetFullPath(relativePath);

            var outputPath = outputDirectory != null
                ? Path.Combine(Path.GetDirectoryName(relativePath)!, $"{Path.GetFileNameWithoutExtension(name)}.{framework}.received.txt")
                : null;

            try
            {
                // Because we run in different appdomain we can always unload
                RunDotNet(workingArea, log,
                          outputPath != null ? TextWriter.Null : Console.Out,
                          "run",
                          "--configuration", "Release",
                          "--framework", framework,
                          "--",
                          fullPath, outputPath ?? "-");

                log?.WriteLine($"Public API file: {outputPath}");
                log?.WriteLine();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Unable to find {fullPath}. Consider specifying --assembly");
                throw;
            }

            if (outputDirectory == null || outputPath == null)
                return null;

            var destinationFilePath = Path.Combine(outputDirectory, Path.GetFileName(outputPath));
            if (File.Exists(destinationFilePath))
                File.Delete(destinationFilePath);
            File.Move(outputPath, destinationFilePath);
            return destinationFilePath;

        }

        static void RunDotNet(string workingArea, TextWriter? log, TextWriter stdout, params string[] arguments)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "dotnet",
                WorkingDirectory = workingArea,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            log?.WriteLine("DotNet CLI arguments:");
            foreach (var (i, arg) in arguments.Select((arg, i) => (i + 1, arg)))
            {
                log?.WriteLine($"{i,4}. {arg}");
                psi.ArgumentList.Add(arg);
            }

            using var process = Process.Start(psi);

            log?.WriteLine($"PID ({process.ProcessName}) = {process.Id}");

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            static DataReceivedEventHandler CreateDataReceivedEventHandler(TextWriter writer) =>
                (_, args) =>
                {
                    if (args.Data == null)
                        return;
                    writer.WriteLine(args.Data);
                };

            using var outputWriter = new StringWriter();

            process.OutputDataReceived += CreateDataReceivedEventHandler(outputWriter);
            process.OutputDataReceived += CreateDataReceivedEventHandler(stdout);

            using var errorWriter = new StringWriter();
            process.ErrorDataReceived += CreateDataReceivedEventHandler(errorWriter);

            process.WaitForExit((int) TimeSpan.FromSeconds(10).TotalMilliseconds);

            var output = outputWriter.ToString();

            log?.WriteLine($"Dotnet output: {output}");
            log?.WriteLine();

            if (process.ExitCode != 0)
            {
                var error = errorWriter.ToString();
                throw new Exception(
                    $"dotnet exit code {process.ExitCode}. Directory: {workingArea}. Args: {arguments}. Output: {output}. Error: {error}");
            }
        }

        static void SaveProject(string workingArea, XElement template, TextWriter? log)
        {
            Directory.CreateDirectory(workingArea);
            var projectFilePath = Path.Combine(workingArea, "project.csproj");
            {
                File.WriteAllText(projectFilePath, template.ToString());

                log?.WriteLine($"Project output path: {projectFilePath}");
                log?.WriteLines(template.ToString(), s => ">>> " + s);
            }

            var fullPath = Path.Combine(workingArea, "Program.cs");
            {
                using var stream = typeof(Program).Assembly.GetManifestResourceStream(typeof(Program), "GeneratorMain.cs");
                using var reader = new StreamReader(stream!);
                var program = reader.ReadToEnd();

                log?.WriteLine($"Program output path: {fullPath}");
                log?.WriteLines(program, s => ">>> " + s);

                File.WriteAllText(fullPath, program);
            }
        }

        static XElement CreateProject(string targetFrameworks, string? project, string? package, string? packageVersion, string generatorVersion)
        {
            return new XElement("Project", new XAttribute("Sdk", "Microsoft.NET.Sdk"),
                new XElement("PropertyGroup",
                    new XElement("OutputType", "Exe"),
                    new XElement("TargetFrameworks", targetFrameworks),
                    new XElement("CopyLocalLockFileAssemblies", "true")),
                new XElement("ItemGroup",
                    PackageReference(nameof(PublicApiGenerator), generatorVersion),
                    !string.IsNullOrEmpty(package) ? PackageReference(package, packageVersion!) : null,
                    !string.IsNullOrEmpty(project) ? new XElement("ProjectReference", new XAttribute("Include", project)) : null));

            static XElement PackageReference(string id, string version) =>
                new XElement("PackageReference", new XAttribute("Include", id),
                                                 new XAttribute("Version", version));
        }

        static void AssertInputParameters(string targetFrameworks, string? project, string? package,
            string? packageVersion, string workingArea, string? assembly)
        {
            static Exception Error(string message) =>
                new Exception("Argument error: " + message);

            if (string.IsNullOrEmpty(targetFrameworks))
                throw Error("specify the target frameworks like 'netcoreapp2.1;net461' or 'netcoreapp2.1'.");

            if (!string.IsNullOrEmpty(package) && string.IsNullOrEmpty(packageVersion))
                throw Error("when using the package switch the package-version switch needs to be specified.");

            if (!string.IsNullOrEmpty(package) && !string.IsNullOrEmpty(project))
                throw Error("when using the package name the project-path switch cannot be used or vice versa.");

            if (!string.IsNullOrEmpty(project) && string.IsNullOrEmpty(assembly))
                throw Error("when using the project-path switch the output assembly name has to be specified with --assembly.");

            if (File.Exists(workingArea) || Directory.Exists(workingArea))
                throw Error($"working area \"{workingArea}\" already exists.");
        }

        // Extensions

        static void WriteLines(this TextWriter target, string source) =>
            WriteLines(target, source, s => s);

        static void WriteLines(this TextWriter target, string source,
                               Func<string, string> selector)
        {
            using var reader = new StringReader(source);
            target.WriteLines(reader, selector);
        }

        static void WriteLines(this TextWriter target, TextReader source,
                               bool closeSource = false) =>
            WriteLines(target, source, s => s, closeSource);

        static void WriteLines(this TextWriter target, TextReader source,
                               Func<string, string> selector,
                               bool closeSource = false)
        {
            using var e = source.ReadLines(closeSource);
            while (e.MoveNext())
                target.WriteLine(selector(e.Current));
        }

        static IEnumerator<string> ReadLines(this TextReader reader, bool close = false)
        {
            return _(); IEnumerator<string> _()
            {
                try
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                        yield return line;
                }
                finally
                {
                    if (close)
                        reader.Close();
                }
            }
        }
    }
}
