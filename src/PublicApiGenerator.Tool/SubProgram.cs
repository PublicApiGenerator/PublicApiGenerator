using System;
using System.Reflection;
using System.IO;
using System.Text.Json;
using PublicApiGenerator;

static class Program
{
    // args[0] - assemblyPath to generate public API from
    // args[1] - apiFilePath to generate public API to, '-' for stdout
    // args[2] - settingsFile with options in json form
    static int Main(string[] args)
    {
        try
        {
            var assemblyPath = args[0];
            var asm = Assembly.LoadFile(assemblyPath);
            var options = args[2] == "-" ? new ApiGeneratorOptions() : JsonSerializer.Deserialize<ApiGeneratorOptions>(File.ReadAllText(args[2]));

            switch (args[1])
            {
                case "-":
                    Console.WriteLine(asm.GeneratePublicApi(options));
                    break;
                case string apiFilePath:
                    File.WriteAllText(apiFilePath, asm.GeneratePublicApi(options));
                    break;
            }
            return 0;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e);
            return 1;
        }
    }
}
