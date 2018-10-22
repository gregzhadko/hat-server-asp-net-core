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
        Task<string> GetPackAsync(int packId);
        Task<string> GetResponseAsync(string requestUriString, int port);
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

        public async Task<string> GetPackAsync(int packId)
        {
            return await GetResponseAsync($"getPack?id={packId}", 8081);
        }

        public async Task<string> GetResponseAsync(string requestUriString, int port)
        {
            var url = _configuration["OldServerUrl"];
            var finalUrl = $"{url}:{port}/{requestUriString}";
            return await _client.GetStringAsync(finalUrl);
        }
    }
}