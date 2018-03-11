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

        public static async Task<string> GetResponseAsync(string requestUriString, int port)
        {
            var url = File.ReadLines("Settings.txt").First();
            var finalUrl = $"{url}:{port}/{requestUriString}";
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(finalUrl).ConfigureAwait(false);
            }
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
                    var response = await GetResponseAsync($"getPack?id={packInfo.Id}", 8081).ConfigureAwait(false);
                    var pack = JsonConvert.DeserializeObject<Pack>(response, new JsonToPhraseItemConverter(users));
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
    }
}