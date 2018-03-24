using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alba.CsConsoleFormat.Fluent;
using HatServer.Models;
using HatServer.Old;
using Utilities;

namespace DictionaryService
{
    public class DescriptionUpdater
    {
        private OxfordService _oxfordService;

        public DescriptionUpdater()
        {
            var settings = File.ReadLines("Settings.txt").ToList();
            _oxfordService = new OxfordService(settings[2], settings[3]);
        }

        public async Task UpdateDescriptionsAsync(int packId)
        {
            var pack = await OldService.GetPackAsync(packId);

            foreach (var phrase in pack.Phrases/*.Where(p => String.IsNullOrWhiteSpace(p.Description))*/)
            {
                try
                {

                    ConsoleUtilities.WriteValid("Loading of description for " + phrase.Phrase);
                    var definitions = await LoadDescriptionsAsync(phrase);
                    definitions.ForEach(Console.WriteLine);

                    if (definitions.Count == 0)
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
                    ConsoleUtilities.WriteException(e);
                }
            }

        }

        private async Task<List<string>> LoadDescriptionsAsync(PhraseItem phrase)
        {
            try
            {
                return (await _oxfordService.GetDescriptionsAsync(phrase.Phrase)).Take(3).ToList();
            }
            catch (Exception e)
            {
                ConsoleUtilities.WriteError($"Can't load description for {phrase.Phrase}");
                //ConsoleUtilities.WriteException(e, $"Can't load description for {phrase.Phrase}");
                throw;
            }
        }

        private static async Task AddDescriptionAsync(Pack pack, PhraseItem phrase, string description)
        {
            try
            {
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
