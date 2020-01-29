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
            var fullPath = args[0];
            var asm = Assembly.LoadFile(fullPath);
            switch (args[1])
            {
                case "-": Console.WriteLine(asm.GeneratePublicApi()); break;
                case string outputPath: File.WriteAllText(outputPath, asm.GeneratePublicApi()); break;
            }
            return 0;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e);
            return 0xbad;
        }
    }
}
