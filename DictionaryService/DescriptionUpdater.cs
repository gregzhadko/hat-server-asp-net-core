using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HatServer.Models;
using HatServer.Old;
using Utilities;

namespace DictionaryService
{
    public class DescriptionUpdater
    {
        private readonly OxfordService _oxfordService;

        public DescriptionUpdater()
        {
            var settings = File.ReadLines("Settings.txt").ToList();
            _oxfordService = new OxfordService(settings[2], settings[3]);
        }

        public async Task UpdateDescriptionsAsync(int packId)
        {
            var pack = await OldService.GetPackAsync(packId);
            var errorList = new List<string>();

            foreach (var phrase in pack.Phrases.Where(p => String.IsNullOrWhiteSpace(p.Description)))
            {
                try
                {
                    ConsoleUtilities.WriteValid("Loading of description for " + phrase.Phrase);
                    var definitions = await LoadDescriptionsAsync(phrase.Phrase);
                    
                    if (definitions == null || definitions.Count == 0)
                    {
                        ConsoleUtilities.WriteError($"There is no definition for {phrase.Phrase}");
                        continue;
                    }

                    if (definitions.Count == 1)
                    {
                        await AddDescriptionAsync(pack, phrase, definitions[0].FormatDescription());
                        continue;
                    }

                    var description = new StringBuilder();
                    for (int i = 0; i < definitions.Count; i++)
                    {
                        description.Append($"{i + 1}. ");
                        description.Append(definitions[i].FormatDescription());
                        description.Append("\n");
                    }

                    description.Remove(description.Length - 1, 1);

                    await AddDescriptionAsync(pack, phrase, description.ToString());
                }
                catch (Exception e)
                {
                    errorList.Add(phrase.Phrase);
                }
            }

            Console.WriteLine("Description weren't added for the following words:");
            errorList.ForEach(Console.WriteLine);

        }

        public async Task<List<string>> LoadDescriptionsAsync(string phrase)
        {
            try
            {
                var listOfDescriptions = await _oxfordService.GetDescriptionsAsync(phrase);
                return listOfDescriptions?.Take(1).ToList();
            }
            catch (Exception)
            {
                ConsoleUtilities.WriteError($"Can't load description for {phrase}");
                throw;
            }
        }

        private static async Task AddDescriptionAsync(Pack pack, PhraseItem phrase, string description)
        {
            try
            {
                if (phrase.Complexity <= 0)
                {
                    phrase.Complexity = 1;
                }
                await OldService.AddPhraseDescriptionAsync(pack.Id, phrase, description);
                ConsoleUtilities.WriteValid($"Description for phrase {phrase.Phrase} was added");
            }
            catch (Exception e)
            {
                ConsoleUtilities.WriteException(e, $"Description for phrase {phrase.Phrase} wasn't added");
            }
        }
    }
}
