using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DictionaryService;
using HatServer.Old;
using Microsoft.EntityFrameworkCore;
using Utilities;
using YandexTranslateCSharpSdk;

namespace ConsoleMigration
{
    public class Program
    {
        private static YandexTranslateSdk _translatorWrapper;

        public static async Task Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            await RunSpellCheckerAsync();

            //await DeleteWordsInPackAsync(15);
            //await LoadPhrasesAsync(15, @"D:\sport.txt");
            //await LoadDescriptionsAsync(15);

            //await ManuallyDescriptionUpdatingAsync(15);

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        private static Task ManuallyDescriptionUpdatingAsync(int packId)
        {
            var definitionUpdate = new DescriptionUpdaterManager();
            return definitionUpdate.RunManualUpdatingAsync(packId);
        }

        private static async Task DeleteWordsInPackAsync(int packId)
        {
            var pack = await OldService.GetPackAsync(packId);
            foreach (var phrase in pack.Phrases)
            {
                Console.WriteLine($"Removing of phrase {phrase.Phrase}");
                await OldService.DeletePhraseAsync(packId, phrase.Phrase);
            }

            ConsoleUtilities.WriteGreenLine("Removing of phrases is completed");
        }

        public static async void RunMigration()
        {
        }

        private static async Task RunSpellCheckerAsync()
        {
            var packs = await OldService.GetAllPacksAsync();
            var spellChecker = new SpellChecker.SpellChecker(packs);
            spellChecker.Run();
        }

        private static Task LoadDescriptionsAsync(int packId)
        {
            var service = new DescriptionUpdaterManager();
            return service.UpdateDescriptionsAsync(packId, 1, p => String.IsNullOrWhiteSpace(p.Description));
        }

        /// <summary>
        /// Loads phrases to the pack from file
        /// </summary>
        /// <param name="packId">Pack id</param>
        /// <param name="path">Path to the file with a list of phrases</param>
        private static async Task LoadPhrasesAsync(int packId, string path)
        {
            var lines = File.ReadAllLines(path);
            var service = new DescriptionUpdaterManager();
            foreach (var line in lines)
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
                    var definitions = await service.LoadDescriptionsAsync(phrase);
                    if (definitions == null)
                    {
                        ConsoleUtilities.WriteRedLine($"No definitions for {phrase}");
                    }
                    await OldService.AddPhraseAsync(packId, phrase, definitions?.FirstOrDefault());
                    ConsoleUtilities.WriteGreenLine($"Phrase was added {phrase}");
                }
                catch
                {
                    ConsoleUtilities.WriteRedLine($"Phrase wasn't added {phrase}");
                }
            }
        }

        private static void SetupTranslator()
        {
            _translatorWrapper = new YandexTranslateSdk {ApiKey = File.ReadLines("Settings.txt").ToArray()[1]};
        }

        private static async Task TranslatePackAsync(int packId)
        {
            var pack = await OldService.GetPackAsync(packId);
            var finalList = new List<(string, string)>();

            foreach (var phrase in pack.Phrases)
            {
                var translated = await TranslateAsync(phrase.Phrase);
                finalList.Add((phrase.Phrase, translated));
                ConsoleUtilities.WriteGreenLine($"{phrase.Phrase}\t\t{translated}");
            }

            finalList.ToList().ForEach(i => Console.WriteLine("{0,-30}{1,30}", i.Item1, i.Item2));
        }

        private static Task<string> TranslateAsync(string text) => _translatorWrapper.TranslateText(text, "ru-en");
    }
}
