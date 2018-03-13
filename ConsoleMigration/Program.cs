using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Alba.CsConsoleFormat;
using HatServer.Old;
using Utilities;
using YandexTranslateCSharpSdk;

namespace ConsoleMigration
{
    public class Program
    {
        private static readonly LineThickness StrokeHeader = new LineThickness(LineWidth.None);
        private static readonly LineThickness StrokeRight = new LineThickness(LineWidth.None, LineWidth.None, LineWidth.Single, LineWidth.None);
        static YandexTranslateSdk _translatorWrapper;

        public static void Main(string[] args)
        {
            _translatorWrapper = new YandexTranslateSdk();
            _translatorWrapper.ApiKey = File.ReadLines("Settings.txt").ToArray()[1];

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Run();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }


        private static void Run()
        {
            var oldService = new OldService();
            //var packs = OldService.GetAllPacksAsync(null).Result;

            //packs.ForEach(p => Console.WriteLine($"{p.Id} {p.Name}"));

            var pack = OldService.GetPack(13).Result;
            var finalList = new ConcurrentBag<(string, string)>();

            Parallel.ForEach(pack.Phrases, phrase =>
            {
                var translated = Translate(phrase.Phrase);
                finalList.Add((phrase.Phrase, translated));
                ConsoleUtilities.WriteInfo("Translated", phrase.Phrase, translated);
            });

            //const string fileName = @"C:\SimpleWords.txt";
            //File.Create(fileName);
            //File.WriteAllLines(fileName, finalList.Select(i => String.Format("{0,-30}{1,10}", i.Item1, i.Item2)));

            finalList.ToList().ForEach(i => Console.WriteLine(String.Format("{0,-30}{1,10}", i.Item1, i.Item2)));
        }

        private static string Translate(string text)
        {
            return _translatorWrapper.TranslateText(text, "ru-en").Result;
        }
    }
}
