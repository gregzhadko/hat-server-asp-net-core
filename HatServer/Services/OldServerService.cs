using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HatServer.Services
{
    public interface IOldServerService
    {
        Task<string> GetPacksAsync();
    }

    [UsedImplicitly]
    public class OldServerService : IOldServerService
    {
        private readonly HttpClient _client;
        private readonly ILogger<BotNotifier> _logger;
        private readonly IConfiguration _configuration;

        public OldServerService(HttpClient client, ILogger<BotNotifier> logger, IConfiguration configuration)
        {
            _client = client;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<string> GetPacksAsync()
        {
            return await GetResponseAsync("getPacks", 8081);
        }

        private async Task<string> GetResponseAsync(string requestUriString, int port)
        {
            var url = _configuration["OldServerUrl"];
            var finalUrl = $"{url}:{port}/{requestUriString}";
            return await _client.GetStringAsync(finalUrl);
        }
    }
}