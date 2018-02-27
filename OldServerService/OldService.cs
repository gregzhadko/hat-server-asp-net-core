using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using HatServer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OldServerService
{
    public class OldService
    {
        public IEnumerable<Pack> GetAllPacksInfo()
        {
            var response = GetResponse("getPacks", 8081);
            var jObjectPacks = JObject.Parse(response)["packs"].Children().ToList();
            foreach (var jToken in jObjectPacks)
            {
                yield return jToken["pack"].ToObject<Pack>();
            }
        }

        public Pack GetPackById(int id)
        {
            if (id == 0)
            {
                return null;
            }

            var response = GetResponse($"getPack?id={id}", 8081);

            var pack = JsonConvert.DeserializeObject<Pack>(response);
            if (pack.Phrases == null)
            {
                pack.Phrases = new List<PhraseItem>();
            }
            pack.Phrases = pack.Phrases.OrderBy(p => p.Phrase).ToList();
            return pack;
        }

        private static string GetResponse(string requestUriString, int port)
        {
            var request = WebRequest.Create($"http://pigowl.com:{port}/{requestUriString}");

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var dataStream = response.GetResponseStream())
            {
                if (dataStream == null || dataStream == Stream.Null)
                {
                    throw new WebException("Stream is null");
                }

                var reader = new StreamReader(dataStream);
                return reader.ReadToEnd();
            }
        }
    }
}
