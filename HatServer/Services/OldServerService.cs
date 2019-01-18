using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;

namespace HatServer.Services
{
    public interface IOldServerService
    {
        Task<string> GetResponseAsync(string requestUriString, int port);
    }

    [UsedImplicitly]
    public class OldServerService : IOldServerService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public OldServerService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<string> GetResponseAsync(string requestUriString, int port)
        {
            var url = _configuration["OldServerUrl"];
            var finalUrl = $"{url}:{port}/{requestUriString}";
            return await _client.GetStringAsync(finalUrl);
        }
    }
}