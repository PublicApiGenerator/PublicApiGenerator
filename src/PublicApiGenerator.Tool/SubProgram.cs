using System;
using System.Reflection;
using System.IO;
using PublicApiGenerator;

static class Program
{
    static int Main(string[] args)
    {
        try
        {
            var assemblyPath = args[0];
            var asm = Assembly.LoadFile(assemblyPath);
            switch (args[1])
            {
                case "-": Console.WriteLine(asm.GeneratePublicApi()); break;
                case string apiFilePath: File.WriteAllText(apiFilePath, asm.GeneratePublicApi()); break;
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
