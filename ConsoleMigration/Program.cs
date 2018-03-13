using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        static YandexTranslateSdk _translatorWrapper;

        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            //SetupTranslator();


            LoadPhrases(23);
            
            //Run();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        private static void LoadPhrases(int packId)
        {
            var lines = File.ReadAllLines(@"C:\Users\Grigory_Zhadko\Downloads\Telegram Desktop\Simple Words.txt");
            foreach(var line in lines)
            {
                var spaceIndex = line.IndexOf(' ');
                var phrase = line.Substring(spaceIndex, line.Length-spaceIndex).FormatPhrase();
                try
                {
                    OldService.AddPhraseAsync(packId, phrase).GetAwaiter().GetResult();
                    ConsoleUtilities.WriteValid(phrase);
                }
                catch
                {
                    ConsoleUtilities.WriteError(phrase);
                }
            }
        }

        private static void SetupTranslator()
        {
            _translatorWrapper = new YandexTranslateSdk();
            _translatorWrapper.ApiKey = File.ReadLines("Settings.txt").ToArray()[1];
        }

        private static void Run()
        {
            var pack = OldService.GetPack(13).Result;
            var finalList = new ConcurrentBag<(string, string)>();

            Parallel.ForEach(pack.Phrases, async phrase =>
            {
                var translated = await Translate(phrase.Phrase);
                finalList.Add((phrase.Phrase, translated));
                ConsoleUtilities.WriteInfo("Translated", phrase.Phrase, translated);
            });

            finalList.ToList().ForEach(i => Console.WriteLine("{0,-30}{1,15}", i.Item1, i.Item2));
        }

        private static Task<string> Translate(string text)
        {
            return _translatorWrapper.TranslateText(text, "ru-en");
        }
    }
}
