using System;
using System.Reflection;
using System.IO;
using PublicApiGenerator;

static class Program
{
    static void Main(string[] args)
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
