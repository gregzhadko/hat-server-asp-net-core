using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HatServer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HatServer.Old
{
    public class OldService
    {
        public async Task<List<Pack>> GetAllPacksInfoAsync()
        {
            var response = await GetResponse("getPacks", 8081);
            var jObjectPacks = JObject.Parse(response)["packs"].Children().ToList();
            var packs = new List<Pack>();
            foreach (var jToken in jObjectPacks)
            {
                var pack = jToken["pack"].ToObject<Pack>();
                packs.Add(pack);
            }

            return packs;
        }

        public async Task<Pack> GetPackByIdAsync(int id)
        {
            if (id == 0)
            {
                return null;
            }

            var response = await GetResponse($"getPack?id={id}", 8081);

            var pack = JsonConvert.DeserializeObject<Pack>(response);
            if (pack.Phrases == null)
            {
                pack.Phrases = new List<PhraseItem>();
            }

            pack.Phrases = pack.Phrases.OrderBy(p => p.Phrase).ToList();
            return pack;
        }

        public Task<string> GetResponse(string requestUriString, int port)
        {
            var request = WebRequest.Create($"http://pigowl.com:{port}/{requestUriString}");

            using (var response = (HttpWebResponse) request.GetResponse())
            using (var dataStream = response.GetResponseStream())
            {
                if (dataStream == null || dataStream == Stream.Null)
                {
                    throw new WebException("Stream is null");
                }

                var reader = new StreamReader(dataStream);
                return reader.ReadToEndAsync();
            }
        }
    }
}
