using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HatServer.Data;
using HatServer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Utilities;

namespace HatServer.Old
{
    public class OldService
    {
        public static Task AddPhraseAsync(int packId, PhraseItem phrase) => GetResponseAsync(
            $"addPackWordDescription?id={packId}&word={phrase.Phrase}&description={phrase.Description}&level={phrase.Complexity}&author={phrase.Author}", 8091);

        public static Task AddPhraseAsync(int packId, string phrase, string description, int complexity = 1, string author = "zhadko") => AddPhraseAsync(packId,
            new PhraseItem {Phrase = phrase, Description = description, Complexity = complexity, PhraseStates = GetDefaultPhraseState()});

        public static Task AddPhraseAsync(int packId, string phrase) => AddPhraseAsync(packId,
            new PhraseItem
            {
                Phrase = phrase,
                Complexity = 1,
                Description = "",
                Version = 0,
                PackId = packId,
                PhraseStates = GetDefaultPhraseState()
            });

        //чушь конечно иметь такой метод. Но кое-кто был слишком упрямым чтобы делать нормальные айдишники для слов. С новым сервачком такого говна не будет.
        public static async Task EditPhraseAsync(int packId, PhraseItem oldPhrase, PhraseItem newPhrase, string selectedAuthor = "zhadko")
        {
            if (oldPhrase.Phrase != newPhrase.Phrase)
            {
                await DeletePhraseAsync(packId, oldPhrase.Phrase, selectedAuthor);
            }

            if (!string.Equals(oldPhrase.Phrase, newPhrase.Phrase, StringComparison.Ordinal) ||
                Math.Abs(oldPhrase.Complexity - newPhrase.Complexity) > 0.01 ||
                !string.Equals(oldPhrase.Description, newPhrase.Description, StringComparison.Ordinal))
            {
                await AddPhraseDescriptionAsync(packId, newPhrase, newPhrase.Description, selectedAuthor);
            }
        }

        public static Task AddPhraseDescriptionAsync(int packId, PhraseItem phrase, string description, string selectedAuthor = "zhadko") => GetResponseAsync(
            $"addPackWordDescription?id={packId}&word={phrase.Phrase}&description={description.ReplaceSemicolons()}&level={phrase.Complexity}&author={selectedAuthor}",
            8091);

        public static Task DeletePhraseAsync(int packId, string phrase, string author) => GetResponseAsync($"removePackWord?id={packId}&word={phrase}&author={author}", 8091);

        private static List<PhraseState> GetDefaultPhraseState() => new List<PhraseState>
        {
            new PhraseState
            {
                ReviewState = ReviewState.Accept,
                ServerUser = new ServerUser {UserName = "zhadko"}
            }
        };

        public static async Task<List<Pack>> GetAllPacksInfoAsync()
        {
            var response = await GetResponseAsync("getPacks", 8081).ConfigureAwait(false);
            var jObjectPacks = JObject.Parse(response)["packs"].Children().ToList();
            var packs = new List<Pack>();
            foreach (var jToken in jObjectPacks)
            {
                var pack = jToken["pack"].ToObject<Pack>();
                packs.Add(pack);
            }

            return packs;
        }

        private static async Task<string> GetResponseAsync(string requestUriString, int port)
        {
            var url = File.ReadLines("Settings.txt").First();
            var finalUrl = $"{url}:{port}/{requestUriString}";
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(finalUrl).ConfigureAwait(false);
            }
        }

        public static async Task<List<Pack>> GetAllPacksAsync(List<ServerUser> users = null)
        {
            try
            {
                var packs = await GetAllPacksInfoAsync().ConfigureAwait(false);

                var result = new List<Pack>();

                //TODO: remove OrderBy (it was done for testing purposes)
                foreach (var packInfo in packs.OrderBy(p => p.Id))
                {
                    Pack pack = await GetPackAsync(packInfo.Id, users);
                    ConsoleUtilities.WriteInfo("Downloaded", pack.Id.ToString(), pack.Name, $"Words: {pack.Phrases.Count}", pack.Description);
                    result.Add(pack);
                }

                return result;
            }
            catch (Exception e)
            {
                ConsoleUtilities.WriteException(e);
                return null;
            }
        }

        public static async Task<Pack> GetPackAsync(int id, List<ServerUser> users = null)
        {
            var response = await GetResponseAsync($"getPack?id={id}", 8081).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Pack>(response, new JsonToPhraseItemConverter(users));
        }
    }
}