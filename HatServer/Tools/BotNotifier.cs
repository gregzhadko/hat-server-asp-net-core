using System;
using System.Net.Http;
using System.Threading.Tasks;
using HatServer.DAL.Interfaces;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Model.Entities;

namespace HatServer.Tools
{
    public class BotNotifier
    {
        private readonly IConfiguration _configuration;
        private readonly IDownloadedPacksInfoRepository _downloadedPacksInfoRepository;

        public BotNotifier(IConfiguration configuration, IDownloadedPacksInfoRepository downloadedPacksInfoRepository)
        {
            _configuration = configuration;
            _downloadedPacksInfoRepository = downloadedPacksInfoRepository;
        }

        public async Task SendDownloadedNotificationAsync([NotNull] GamePack pack)
        {
            var client = new HttpClient();

            var downloadedToday = await _downloadedPacksInfoRepository.GetDailyDownloadsForPack(pack.Id);

            var uriString = pack.Paid
                ? String.Format(_configuration["botMessageUrl"] + "&parse_mode=Markdown", $"*{pack.Name}* ({downloadedToday.Count + 1})")
                : String.Format(_configuration["botMessageUrl"], $"{pack.Name} ({downloadedToday.Count + 1})");

            await client.GetAsync(uriString);
        }
    }
}