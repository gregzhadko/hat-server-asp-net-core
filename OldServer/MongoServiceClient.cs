using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Utilities;

namespace OldServer
{
    public class MongoServiceClient
    {
        [NotNull]
        public static Task AddPhraseAsync(int packId, [NotNull] PhraseItem phrase) => GetResponseAsync(
            $"addPackWordDescription?id={packId}&word={phrase.Phrase}&description={phrase.Description}&level={phrase.Complexity}&author=zhadko", 8091);

        [NotNull]
        public static Task AddPhraseAsync(int packId, string phrase, [CanBeNull] string description, int complexity = 1)
        {
            if (!String.IsNullOrWhiteSpace(description))
            {
                return AddPhraseAsync(packId,
                    new PhraseItem {Phrase = phrase, Description = description, Complexity = complexity, ReviewStates = GetDefaultReviewState()});
            }

            return AddPhraseAsync(packId, phrase);
        }

        [NotNull]
        public static Task AddPhraseAsync(int packId, string phrase)
        {
            return GetResponseAsync($"addPackWord?id={packId}&word={phrase}&author=zhadko", 8091);
        }

        //чушь конечно иметь такой метод. Но кое-кто был слишком упрямым чтобы делать нормальные айдишники для слов. С новым сервачком такого говна не будет.
        public static async Task EditPhraseAsync(int packId, [NotNull] PhraseItem oldPhrase, [NotNull] PhraseItem newPhrase, string selectedAuthor = "zhadko")
        {
            if (oldPhrase.Phrase != newPhrase.Phrase)
            {
                await DeletePhraseAsync(packId, oldPhrase.Phrase, selectedAuthor);
                await AddPhraseAsync(packId, newPhrase);
            }

            if (!string.Equals(oldPhrase.Phrase, newPhrase.Phrase, StringComparison.Ordinal) ||
                Math.Abs(oldPhrase.Complexity - newPhrase.Complexity) > 0.01 ||
                !string.Equals(oldPhrase.Description, newPhrase.Description, StringComparison.Ordinal))
            {
                await AddPhraseDescriptionAsync(packId, newPhrase, newPhrase.Description, selectedAuthor);
            }
        }

        [NotNull]
        public static Task AddPhraseDescriptionAsync(int packId, [NotNull] PhraseItem phrase, string description, string selectedAuthor = "zhadko") => GetResponseAsync(
            $"addPackWordDescription?id={packId}&word={phrase.Phrase}&description={description.ReplaceSemicolons()}&level={phrase.Complexity}&author={selectedAuthor}",
            8091);

        [NotNull]
        public static Task DeletePhraseAsync(int packId, string phrase, string author = "zhadko") => GetResponseAsync($"removePackWord?id={packId}&word={phrase}&author={author}", 8091);

        [NotNull]
        private static List<ReviewState> GetDefaultReviewState() => new List<ReviewState>
        {
            new ReviewState
            {
                State = State.Accept,
                UserName = Constants.DefaultUserName
            }
        };

        [ItemNotNull]
        private static async Task<List<Pack>> GetAllPacksInfoAsync()
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

        [ItemCanBeNull]
        public static async Task<List<Pack>> GetAllPacksAsync([CanBeNull] List<string> users = null)
        {
            try
            {
                var packs = await GetAllPacksInfoAsync().ConfigureAwait(false);

                var result = new List<Pack>();

                //TODO: remove OrderBy (it was done for testing purposes)
                foreach (var packInfo in packs
                    .Where(p => !String.IsNullOrEmpty(p.Name))
                    .OrderBy(p => p.Id))
                {
                    var pack = await GetPackAsync(packInfo.Id, users);
                    Console.WriteLine($"Downloaded {pack.Id.ToString()}, {pack.Name}, Words: {pack.Phrases.Count} {pack.Description}");
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

        public static async Task<Pack> GetPackAsync(int id, [CanBeNull] List<string> users = null)
        {
            var response = await GetResponseAsync($"getPack?id={id}", 8081).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Pack>(response, new JsonToPhraseItemConverter(users));
        }
    }
}