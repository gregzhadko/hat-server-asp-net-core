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

        public static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            SetupTranslator();

            await TranslatePackAsync(1);

//            LoadPhrases(23, @"C:\Users\Grigory_Zhadko\Downloads\Telegram Desktop\Simple Words.txt");
//            var count = OldService.GetPack(23).Result.Phrases.Count;
//            Console.WriteLine(count);
            
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        /// <summary>
        /// Loads phrases to the pack from file
        /// </summary>
        /// <param name="packId">Pack id</param>
        /// <param name="path">Path to the file with a list of phrases</param>
        private static void LoadPhrases(int packId, string path)
        {
            var lines = File.ReadAllLines(path);
            foreach(var line in lines)
            {
                //var spaceIndex = line.IndexOf(' ');
                //var phrase = line.Substring(spaceIndex, line.Length-spaceIndex).FormatPhrase();
                var phrase = line.FormatPhrase();
                if (String.IsNullOrWhiteSpace(phrase))
                {
                    continue;
                }
                try
                {
                    OldService.AddPhraseAsync(packId, phrase).GetAwaiter().GetResult();
                    ConsoleUtilities.WriteValid($"Phrase was added {phrase}");
                }
                catch
                {
                    ConsoleUtilities.WriteError($"Phrase wasn't added {phrase}");
                }
            }
        }

        private static void SetupTranslator()
        {
            _translatorWrapper = new YandexTranslateSdk();
            _translatorWrapper.ApiKey = File.ReadLines("Settings.txt").ToArray()[1];
        }

        private static async Task TranslatePackAsync(int packId)
        {
            var pack = await OldService.GetPackAsync(packId);
            var finalList = new List<(string, string)>();

            foreach (var phrase in pack.Phrases)
            {
                var translated = await Translate(phrase.Phrase);
                finalList.Add((phrase.Phrase, translated));
                ConsoleUtilities.WriteValid($"{phrase.Phrase}\t\t{translated}");
            }

            finalList.ToList().ForEach(i => Console.WriteLine("{0,-30}{1,30}", i.Item1, i.Item2));
        }

        private static async Task<string> Translate(string text)
        {
            return await _translatorWrapper.TranslateText(text, "ru-en");
        }
    }
}
