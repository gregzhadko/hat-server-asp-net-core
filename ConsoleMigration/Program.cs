using System;
using System.Linq;
using Alba.CsConsoleFormat;
using Alba.CsConsoleFormat.Fluent;
using HatServer.Old;
using Utilities;

namespace ConsoleMigration
{
    public class Program
    {
        private static readonly LineThickness StrokeHeader = new LineThickness(LineWidth.None);
        private static readonly LineThickness StrokeRight = new LineThickness(LineWidth.None, LineWidth.None, LineWidth.Single, LineWidth.None);

        public static void Main(string[] args)
        {
            Run();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        private static void Run()
        {
            var oldService = new OldService();
            var packs = OldService.GetAllPacksAsync(null).Result;
          
        }
    }
}
