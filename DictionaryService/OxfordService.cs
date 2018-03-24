using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DictionaryService
{
    public class OxfordService
    {
        private readonly string _key;
        private readonly string _id;
        private const string Url = @"https://od-api.oxforddictionaries.com:443/api/v1/entries/en/";

        public OxfordService(string applicationId, string applicationKey)
        {
            _id = applicationId;
            _key = applicationKey;
        }

        public async Task<IEnumerable<string>> GetDescriptionsAsync(string word)
        {
            var response = await LoadDescriptionAsync(word);
            var rootObject = Deserialize(response);

            var list = rootObject.results.SelectMany(r => r.lexicalEntries).SelectMany(l => l.entries).SelectMany(e => e.senses)
                .Where(e => e.definitions != null).SelectMany(s => s.definitions).Where(d => d != null);

            return list;
        }

        public async Task<string> LoadDescriptionAsync(string word)
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("app_id", _id);
                client.DefaultRequestHeaders.Add("app_key", _key);

                return await client.GetStringAsync(Url + word.ToLowerInvariant()).ConfigureAwait(false);
            }
        }

        private Rootobject Deserialize(string response) => JsonConvert.DeserializeObject<Rootobject>(response);
    }
}
