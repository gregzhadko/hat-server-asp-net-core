using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DictionaryService
{
    public class OxfordService : IOnlineDictionaryService
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
            if (response == null)
            {
                return null;
            }
            var rootObject = Deserialize(response);

            var list = rootObject.results.SelectMany(r => r.lexicalEntries).SelectMany(l => l.entries).SelectMany(e => e.senses)
                .Where(e => e.definitions != null).SelectMany(s => s.definitions).Where(d => d != null);

            return list;
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
                catch (HttpRequestException e)
                {
                    return null;
                }
            }
        }

        private static Rootobject Deserialize(string response) => JsonConvert.DeserializeObject<Rootobject>(response);
    }
}

