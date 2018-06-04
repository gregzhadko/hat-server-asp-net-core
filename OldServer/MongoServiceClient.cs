﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Model.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Utilities;

namespace OldServer
{
    public static class MongoServiceClient
    {
        [UsedImplicitly]
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

        [UsedImplicitly]
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

            var oldComplexity = oldPhrase.Complexity;
            var newComplexity = newPhrase.Complexity;
            if (!string.Equals(oldPhrase.Phrase, newPhrase.Phrase, StringComparison.Ordinal) ||
                !AreComplexitiesEqual(oldComplexity, newComplexity) ||
                !string.Equals(oldPhrase.Description, newPhrase.Description, StringComparison.Ordinal))
            {
                await AddPhraseDescriptionAsync(packId, newPhrase, newPhrase.Description, selectedAuthor);
            }
        }

        private static bool AreComplexitiesEqual(double? firstComplexity, double? secondComplexity)
        {
            if (!firstComplexity.HasValue)
            {
                return !secondComplexity.HasValue;
            }

            if (secondComplexity.HasValue)
            {
                return Math.Abs(firstComplexity.Value - secondComplexity.Value) > 0.01;
            }

            return false;
        }

        [NotNull]
        public static Task AddPhraseDescriptionAsync(int packId, [NotNull] PhraseItem phrase, string description, string selectedAuthor = "zhadko") => GetResponseAsync(
            $"addPackWordDescription?id={packId}&word={phrase.Phrase}&description={description.ReplaceSemicolons()}&level={phrase.Complexity}&author={selectedAuthor}",
            8091);

        [NotNull]
        public static Task DeletePhraseAsync(int packId, string phrase, string author = "zhadko") => GetResponseAsync($"removePackWord?id={packId}&word={phrase}&author={author}", 8091);

        [NotNull]
        private static List<ReviewState> GetDefaultReviewState()
        {
            return new List<ReviewState>
            {
                new ReviewState
                {
                    State = State.Accept,
                    User = new ServerUser {UserName = "zhadko"}
                }
            };
        }

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
        public static async Task<List<Pack>> GetAllPacksAsync([CanBeNull] List<ServerUser> users = null)
        {
            try
            {
                var packs = await GetAllPacksInfoAsync().ConfigureAwait(false);
                var trackId = 0;
                var result = new List<Pack>();
                //TODO: remove OrderBy (it was done for testing purposes)
                foreach (var packInfo in packs
                    .Where(p => !string.IsNullOrEmpty(p.Name))
                    .OrderBy(p => p.Id))
                {
                    var pack = await GetPackAsync(packInfo.Id, users, trackId);
                    Console.WriteLine($"Downloaded {pack.Id.ToString()}, {pack.Name}, Words: {pack.Phrases.Count} {pack.Description}");
                    result.Add(pack);
                    trackId = GetActualTrackId(pack, trackId);
                }

                return result;
            }
            catch (Exception e)
            {
                ConsoleUtilities.WriteException(e);
                return null;
            }
        }

        private static int GetActualTrackId([NotNull] Pack pack, int trackId)
        {
            var list = pack.Phrases.Select(p => p.TrackId).ToList();
            if (list.Count > 0)
            {
                return list.Max() + 1;
            }

            return trackId;
        }

        public static async Task<Pack> GetPackAsync(int id, [CanBeNull] List<ServerUser> users = null, int trackId = 1)
        {
            var response = await GetResponseAsync($"getPack?id={id}", 8081).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Pack>(response, new JsonToPhraseItemConverter(users, trackId));
        }
    }
}