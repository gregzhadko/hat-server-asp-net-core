using HatServer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HatServer.Data;
using Utilities;

namespace HatServer.Old
{
    public class OldService
    {
        public static async Task<List<Pack>> GetAllPacksInfoAsync()
        {
            var response = await GetResponseAsync("getPacks", 8081);
            var jObjectPacks = JObject.Parse(response)["packs"].Children().ToList();
            var packs = new List<Pack>();
            foreach (var jToken in jObjectPacks)
            {
                var pack = jToken["pack"].ToObject<Pack>();
                packs.Add(pack);
            }

            return packs;
        }

        public static async Task<string> GetResponseAsync(string requestUriString, int port)
        {
            var url = $"http://pigowl.com:{port}/{requestUriString}";
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(url).ConfigureAwait(false);
            }
            
            
//            var request = WebRequest.Create(url);
//
//            using (var response = (HttpWebResponse)await request.GetResponseAsync())
//            using (var dataStream = response.GetResponseStream())
//            {
//                if (dataStream == null || dataStream == Stream.Null)
//                {
//                    throw new WebException("Stream is null");
//                }
//
//                var reader = new StreamReader(dataStream);
//                return await reader.ReadToEndAsync();
//            }
        }

        public static async Task<List<Pack>> GetAllPacksAsync(List<ApplicationUser> users)
        {
            try
            {
                var packs = await GetAllPacksInfoAsync().ConfigureAwait(false);

                var result = new List<Pack>();

                //TODO: remove OrderBy (it was done for testing purposes)
                foreach (var packInfo in packs.OrderBy(p => p.Id))
                {
                    var response = await GetResponseAsync($"getPack?id={packInfo.Id}", 8081);
                    var pack = JsonConvert.DeserializeObject<Pack>(response, new JsonToPhraseItemConverter(users));
                    ConsoleUtilities.WriteInfo("Downloaded", pack.Id.ToString(), pack.Name, pack.Description, $"Words: {pack.Phrases.Count}");
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
    }
}
