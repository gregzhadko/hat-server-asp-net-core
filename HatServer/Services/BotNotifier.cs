using System;
using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Model.Entities;

namespace HatServer.Services
{
    public interface IBotNotifier
    {
        Task SendDownloadedNotificationAsync([NotNull] GamePack pack);
    }

    [UsedImplicitly]
    public class BotNotifier : IBotNotifier
    {
        private readonly HttpClient _client;
        private readonly ILogger<BotNotifier> _logger;
        private readonly IConfiguration _configuration;

        public BotNotifier(HttpClient client, ILogger<BotNotifier> logger, IConfiguration configuration)
        {
            _client = client;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task SendDownloadedNotificationAsync(GamePack pack)
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
    }
}