using HatServer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace HatServer.Data
{
    public class JsonToPhraseItemConverter : JsonConverter
    {
        private readonly List<string> _users;

        public JsonToPhraseItemConverter(List<string> users) => _users = users;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        [CanBeNull]
        public override object ReadJson([NotNull] JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartObject)
            {
                return null;
            }

            return ReadPack(reader);
        }

        [NotNull]
        private Pack ReadPack(JsonReader reader)
        {
            var packItem = JObject.Load(reader);
            var pack = new Pack
            {
                Id = packItem["id"].Value<int>(),
                Language = packItem["language"].HasValues ? packItem["language"].Value<string>() : "ru",
                Name = packItem["name"].Value<string>(),
                Description = packItem["description"].Value<string>(),
                Phrases = new List<PhraseItem>()
            };
            ReadPhrases(packItem, pack);
            return pack;
        }

        private void ReadPhrases([NotNull] JObject packItem, Pack pack)
        {
            foreach (var phraseItem in packItem["phrases"].Children().ToList())
            {
                var phrase = new PhraseItem
                {
                    Phrase = phraseItem["phrase"].Value<string>(),
                    Complexity = phraseItem["complexity"].Value<int>(),
                    Description = phraseItem["description"].Value<string>()
                };

                ReadReviewers(phraseItem, phrase);

                pack.Phrases.Add(phrase);
            }
        }

        private void ReadReviewers([NotNull] JToken phraseItem, PhraseItem phrase)
        {
            if (phraseItem["reviews"] == null || _users == null)
            {
                return;
            }

            foreach (var user in _users)
            {
                var reviewState = phraseItem["reviews"][user];
                if (reviewState == null)
                {
                    continue;
                }

                var phraseState = new PhraseState
                {
                    UserName = user,
                    PhraseItem = phrase,
                    ReviewState = (ReviewState)reviewState.Value<int>()
                };
                phrase.PhraseStates.Add(phraseState);
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Pack).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }
    }
}