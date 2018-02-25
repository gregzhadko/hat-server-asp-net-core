using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using HatServer.Models;
using Newtonsoft.Json.Linq;

namespace OldServerService
{
    public class OldService
    {
        public static IEnumerable<Pack> GetAllPacksInfo()
        {
            var packsInfo = GetPacksInfo(8081);
            return packsInfo.Select(p => new Pack { Id = Convert.ToInt32(p["id"]), Name = p["name"].ToString() });
        }

        private static IEnumerable<JToken> GetPacksInfo(int port)
        {
            var response = GetResponse("getPacks", port);
            var jObject = JObject.Parse(response)["packs"];
            var packs = jObject.Select(i => i["pack"]);
            return packs;
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
