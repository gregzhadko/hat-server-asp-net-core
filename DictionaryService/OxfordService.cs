using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace DictionaryService
{
    public sealed class OxfordService : IOnlineDictionaryService
    {
        private readonly string _key;
        private readonly string _id;
        private const string Url = @"https://od-api.oxforddictionaries.com:443/api/v1/entries/en/";

        public OxfordService(string applicationId, string applicationKey)
        {
            _id = applicationId;
            _key = applicationKey;
        }

        [ItemCanBeNull]
        public async Task<IEnumerable<string>> GetDescriptionsAsync(string word)
        {
            var response = await LoadDescriptionAsync(word);
            if (response == null)
            {
                return null;
            }

            var rootObject = Deserialize(response);

            var list = rootObject.results.SelectMany(r => r.lexicalEntries).SelectMany(l => l.entries).SelectMany(e => e.senses)
                .Where(e => e.definitions != null).SelectMany(s => s.definitions).Where(d => d != null);

            return list;
        }

        public async Task<bool> DoesWordExist(string word)
        {
            var descriptions = await GetDescriptionsAsync(word);
            if (descriptions == null)
            {
                return false;
            }

            var list = descriptions.ToList();
            return list.Count != 0 && !string.IsNullOrWhiteSpace(list.First());
        }

        private async Task<string> LoadDescriptionAsync(string word)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("app_id", _id);
                client.DefaultRequestHeaders.Add("app_key", _key);
                try
                {
                    return await client.GetStringAsync(Url + word.ToLowerInvariant()).ConfigureAwait(false);
                }
                catch (HttpRequestException)
                {
                    return null;
                }
            }
        }

        private static Rootobject Deserialize(string response) => JsonConvert.DeserializeObject<Rootobject>(response);
    }
}

