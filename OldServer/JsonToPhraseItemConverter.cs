using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Model;
using Model.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OldServer
{
    internal sealed class JsonToPhraseItemConverter : JsonConverter
    {
        [CanBeNull]
        private readonly List<ServerUser> _users;

        private int _trackId;

        public JsonToPhraseItemConverter([CanBeNull] List<ServerUser> users, int trackId)
        {
            _users = users;
            _trackId = trackId;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        [CanBeNull]
        public override object ReadJson([NotNull] JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.TokenType != JsonToken.StartObject ? null : ReadPack(reader);
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

        private void ReadPhrases([NotNull] JObject packItem, [NotNull] Pack pack)
        {
            var defaultUser = _users?.First(u => u.UserName == Constants.DefaultUserName);
            foreach (var phraseItem in packItem["phrases"].Children().ToList())
            {
                var phrase = new PhraseItem
                {
                    Phrase = phraseItem["phrase"].Value<string>(),
                    Complexity = phraseItem["complexity"].Value<int>(),
                    Description = phraseItem["description"].Value<string>(),
                    CreatedDate = DateTime.Now,
                    CreatedBy = defaultUser,
                    TrackId = ++_trackId,
                    Track = null
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
                var state = phraseItem["reviews"][user.UserName];
                if (state == null)
                {
                    continue;
                }

                var reviewState = new ReviewState
                {
                    User = user,
                    PhraseItem = phrase,
                    State = (State)state.Value<int>()
                };
                phrase.ReviewStates.Add(reviewState);
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Pack).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }
    }
}