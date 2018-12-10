using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Model.Entities;

namespace HatServer.Services
{
    public interface IBotNotifier
    {
        Task SendPackDownloadedNotificationAsync([NotNull] GamePack pack);
        Task<HttpResponseMessage> SendInfoAboutDownloadedPacksAsync(List<DownloadedPacksInfo> downloadedPacks);
    }

    [UsedImplicitly]
    public class BotNotifier : IBotNotifier
    {
        private const string NewLine = "%0A";
        private const string Space = "%20";
        
        private readonly HttpClient _client;
        private readonly ILogger<BotNotifier> _logger;
        private readonly IConfiguration _configuration;

        public BotNotifier(HttpClient client, ILogger<BotNotifier> logger, IConfiguration configuration)
        {
            _client = client;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task SendPackDownloadedNotificationAsync(GamePack pack)
        {
            try
            {
                var baseUrl = _configuration["botMessageUrl"];
                var uriString = pack.Paid
                    ? String.Format($"{baseUrl}&parse_mode=Markdown", $"*{pack.Name}*")
                    : String.Format(baseUrl, $"{pack.Name}");

                var response = await _client.GetAsync(uriString);
                response.EnsureSuccessStatusCode();
            }
            catch(Exception ex)
            {
                 _logger.LogError(ex, "Cannot sent notification to telegram");
            }
        }

        public async Task<HttpResponseMessage> SendInfoAboutDownloadedPacksAsync(
            List<DownloadedPacksInfo> downloadedPacks)
        {
            var groupedPacks = downloadedPacks.GroupBy(p => p.GamePackId);
            
            try
            {
                var baseUrl = _configuration["botMessageUrl"];
                var message = new StringBuilder();
                foreach (var pack in groupedPacks)
                {
                    message.Append($"{pack.First().GamePack.Name}{Space}{pack.Count()}{NewLine}");
                }

                var uriString = String.Format(baseUrl, $"{message}");
                var response = await _client.GetAsync(uriString);
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Cannot sent notification to telegram");
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }
    }
}