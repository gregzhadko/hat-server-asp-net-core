﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Model.Entities;
using OldServer;
using Utilities;

namespace DictionaryService
{
    public sealed class DescriptionUpdaterManager
    {
        private readonly IOnlineDictionaryService _service;

        public DescriptionUpdaterManager()
        {
            var settings = File.ReadLines("Settings.txt").ToList();
            _service = new OxfordService(settings[2], settings[3]);
        }

//        public async Task UpdateDescriptionsAsync(int packId, int maxDescription, [NotNull] Func<PhraseItem, bool> filter)
//        {
//            var pack = await MongoServiceClient.GetPackAsync(packId);
//            var errorList = new List<string>();
//
//            foreach (var phrase in pack.Phrases.Where(filter))
//            {
//                try
//                {
//                    ConsoleUtilities.WriteGreenLine("Loading of description for " + phrase.Phrase);
//                    var definitions = (await LoadDescriptionsAsync(phrase.Phrase))?.Take(maxDescription).ToList();
//
//                    if (definitions == null || definitions.Count == 0)
//                    {
//                        ConsoleUtilities.WriteRedLine($"There is no definitions for {phrase.Phrase}");
//                        continue;
//                    }
//
//                    if (definitions.Count == 1)
//                    {
//                        await AddDescriptionAsync(pack, phrase, definitions[0].FormatDescription());
//                        continue;
//                    }
//
//                    var description = new StringBuilder();
//                    for (var i = 0; i < definitions.Count; i++)
//                    {
//                        description.Append($"{i + 1}. ");
//                        description.Append(definitions[i].FormatDescription());
//                        description.Append("\n");
//                    }
//
//                    description.Remove(description.Length - 1, 1);
//
//                    await AddDescriptionAsync(pack, phrase, description.ToString());
//                }
//                catch (Exception)
//                {
//                    errorList.Add(phrase.Phrase);
//                }
//            }
//
//            Console.WriteLine("Description weren't added for the following words:");
//            errorList.ForEach(Console.WriteLine);
//        }

        public async Task<IEnumerable<string>> LoadDescriptionsAsync(string phrase)
        {
            try
            {
                return await _service.GetDescriptionsAsync(phrase);
            }
            catch (Exception)
            {
                ConsoleUtilities.WriteRedLine($"Can't load description for {phrase}");
                throw;
            }
        }

        // ReSharper disable once UnusedMember.Local
        private static async Task AddDescriptionAsync(Pack pack, [NotNull] PhraseItem phrase, string description)
        {
            try
            {
                if (phrase.Complexity <= 0)
                {
                    phrase.Complexity = 1;
                }

                await MongoServiceClient.AddPhraseDescriptionAsync(pack.Id, phrase, description);
                ConsoleUtilities.WriteGreenLine($"Description for phrase {phrase.Phrase} was added");
            }
            catch (Exception e)
            {
                ConsoleUtilities.WriteException(e, $"Description for phrase {phrase.Phrase} wasn't added");
            }
        }

//        public async Task RunManualUpdatingAsync(int packId)
//        {
//            var pack = await MongoServiceClient.GetPackAsync(packId);
//            foreach (var phrase in pack.Phrases)
//            {
//                Console.WriteLine("Current phrase state");
//                var arr = new List<string> {phrase.Phrase, phrase.Description};
//                phrase.ReviewStates.ForEach(s => arr.Add(s.ToString()));
//                ConsoleUtilities.WriteInfo("", arr.ToArray());
//                Console.Write("Do you want to change it?\ny/n ");
//                var answer = Console.ReadKey();
//                Console.WriteLine();
//                if (answer.Key == ConsoleKey.Y)
//                {
//                    var descriptions = (await LoadDescriptionsAsync(phrase.Phrase))?.ToList();
//                    if (descriptions?.Any() != true)
//                    {
//                        ConsoleUtilities.WriteRed("There are no descriptions for phrase");
//                        Console.WriteLine(phrase.Phrase);
//                        return;
//                    }
//
//                    Console.WriteLine("Available definitions:");
//                    for (var i = 0; i < descriptions.Count; i++)
//                    {
//                        ConsoleUtilities.WriteGreenLine($"{i + 1}. {descriptions[i]}");
//                    }
//
//                    Console.WriteLine("Enter the description number");
//                    var line = Console.ReadLine();
//                    Console.WriteLine();
//                    if (int.TryParse(line, out var number) && number - 1 >= 0 && number - 1 < descriptions.Count)
//                    {
//                        await AddDescriptionAsync(pack, phrase, descriptions[number - 1]);
//                    }
//                    else
//                    {
//                        ConsoleUtilities.WriteRedLine("Wrong number, description wasn't added");
//                    }
//                }
//                else
//                {
//                    ConsoleUtilities.WriteRedLine("Definition wasn't added");
//                }
//            }
//        }
    }
}
