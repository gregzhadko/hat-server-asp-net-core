using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DictionaryService;
using JetBrains.Annotations;
using Model;
using OldServer;
using Utilities;
using YandexTranslateCSharpSdk;

// ReSharper disable UnusedMember.Local

namespace ConsoleMigration
{
    internal static class Program
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

            //await FormatAllAsync(15);

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        [NotNull]
        private static Task ManuallyDescriptionUpdatingAsync(int packId)
        {
            var definitionUpdate = new DescriptionUpdaterManager();
            return definitionUpdate.RunManualUpdatingAsync(packId);
        }

        private static async Task DeleteWordsInPackAsync(int packId)
        {
            var pack = await MongoServiceClient.GetPackAsync(packId);
            foreach (var phrase in pack.Phrases)
            {
                Console.WriteLine($"Removing of phrase {phrase.Phrase}");
                await MongoServiceClient.DeletePhraseAsync(packId, phrase.Phrase);
            }

            ConsoleUtilities.WriteGreenLine("Removing of phrases is completed");
        }

        private static async Task FormatAllAsync(int packId)
        {
            var pack = await MongoServiceClient.GetPackAsync(packId);
            foreach (var phrase in pack.Phrases)
            {
                Console.WriteLine($"{phrase.Phrase}");
                var newPhrase = phrase.Phrase.FormatPhrase();
                var newDescription = phrase.Description.FormatDescription();
                await MongoServiceClient.EditPhraseAsync(packId, phrase,
                    new PhraseItem {Phrase = newPhrase, Description = newDescription, Complexity = phrase.Complexity});

                if (newPhrase != phrase.Phrase)
                {
                    ConsoleUtilities.WriteRedLine($"{phrase.Phrase}     ->      {newPhrase}");
                }

                if (newDescription != phrase.Description)
                {
                    ConsoleUtilities.WriteRedLine($"{phrase.Description}");
                    Console.WriteLine("|");
                    ConsoleUtilities.WriteRedLine($"{newDescription}");
                }
            }
        }

        private static async Task RunSpellCheckerAsync()
        {
            var settings = File.ReadLines("Settings.txt").ToList();
            var service = new OxfordService(settings[2], settings[3]);
            var packs = await MongoServiceClient.GetAllPacksAsync();
            var spellChecker = new SpellChecker.SpellChecker(packs, service);
            spellChecker.Run();
        }

        [NotNull]
        private static Task LoadDescriptionsAsync(int packId)
        {
            var service = new DescriptionUpdaterManager();
            return service.UpdateDescriptionsAsync(packId, 1, filter: p => string.IsNullOrWhiteSpace(p.Description));
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
            foreach (var phrase in lines.Select(selector: line => line.FormatPhrase()).Where(predicate: phrase => !string.IsNullOrWhiteSpace(phrase)))
            {
                try
                {
                    var definitions = await service.LoadDescriptionsAsync(phrase);
                    if (definitions == null)
                    {
                        ConsoleUtilities.WriteRedLine($"No definitions for {phrase}");
                    }

                    await MongoServiceClient.AddPhraseAsync(packId, phrase, definitions?.FirstOrDefault());
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
            var pack = await MongoServiceClient.GetPackAsync(packId);
            var finalList = new List<(string, string)>();

            foreach (var phrase in pack.Phrases)
            {
                var translated = await TranslateAsync(phrase.Phrase);
                finalList.Add((phrase.Phrase, translated));
                ConsoleUtilities.WriteGreenLine($"{phrase.Phrase}\t\t{translated}");
            }

            finalList.ToList().ForEach(action: i => Console.WriteLine("{0,-30}{1,30}", i.Item1, i.Item2));
        }

        private static Task<string> TranslateAsync(string text) => _translatorWrapper.TranslateText(text, "ru-en");
    }
}
