using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DictionaryService
{
    public class OxfordService
    {
        private string _key;
        private string _id;
        private const string Url = @"https://od-api.oxforddictionaries.com:443/api/v1/entries/en/";

        public OxfordService(string applicationId, string applicationKey)
        {
            _id = applicationId;
            _key = applicationKey;
        }

        public async Task<Rootobject> LoadDescriptionAsync(string word)
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("app_id", _id);
                client.DefaultRequestHeaders.Add("app_key", _key);

                var response = await client.GetStringAsync(Url + word.ToLowerInvariant());
                return JsonConvert.DeserializeObject<Rootobject>(response);
            }
        }
    }
}
