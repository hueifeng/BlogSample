using System;

namespace SourceGeneratorDemo
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Display();
            RunExportDumper();
            Console.ReadKey();
        }

        private static void RunExportDumper()
        {
            ExportDumper.Dump();
        }
    }
}
