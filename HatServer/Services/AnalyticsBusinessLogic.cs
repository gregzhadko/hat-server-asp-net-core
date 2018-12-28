using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using HatServer.DAL.Interfaces;
using Model;
using Model.Entities;

namespace HatServer.Services
{
    public interface IAnalyticsBusinessLogic
    {
        CommonAnalytics GetCommonAnalytics();
    }

    public class AnalyticsBusinessLogic : IAnalyticsBusinessLogic
    {
        private readonly IDeviceInfoRepository _deviceInfoRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IRoundRepository _roundRepository;

        public AnalyticsBusinessLogic(IDeviceInfoRepository deviceInfoRepository, IGameRepository gameRepository,
            IRoundRepository roundRepository)
        {
            _deviceInfoRepository = deviceInfoRepository;
            _gameRepository = gameRepository;
            _roundRepository = roundRepository;
        }

        public CommonAnalytics GetCommonAnalytics()
        {
            var fullGames = _gameRepository.GetFullGames()
                //.Where(g => g.Game.InGameId.Equals("FAEE87BF-599E-4BB5-8687-426F91EFE315_1544700985", StringComparison.CurrentCultureIgnoreCase))
                .ToList();
            
            return AnalyzeGames(fullGames);
        }

        private static CommonAnalytics AnalyzeGames(IReadOnlyCollection<FullGame> fullGames)
        {
            var analytics = new CommonAnalytics {TotalGames = fullGames.Count};
            foreach (var fullGame in fullGames)
            {
                if (fullGame.Rounds.Count == 0)
                {
                    fullGame.State = GameState.NoRounds;
                    analytics.NoRounds++;
                    continue;
                }
                
                if (fullGame.Game.Words.Count <= 10)
                {
                    fullGame.State = GameState.FewAddedWords;
                    analytics.FewAddedWords++;
                    continue;
                }

                if (fullGame.Game.Teams.SelectMany(t => t.Players).Count() > fullGame.Rounds.Count)
                {
                    fullGame.State = GameState.FewPlayedRounds;
                    analytics.FewPlayedRounds++;
                    continue;
                }

                var numberOfPlayedWords = 0;
                var totalTime = 0;
                foreach (var round in fullGame.Rounds)
                {
                    foreach (var word in round.Words)
                    {
                        if (word.Time <= 0)
                        {
                            continue;
                        }

                        numberOfPlayedWords++;
                        totalTime += word.Time;
                    }
                }

                if (numberOfPlayedWords == 0)
                {
                    //This case is almost impossible.
                    fullGame.State = GameState.ZeroPlayedWords;
                    continue;
                }

                var averageTime = totalTime / numberOfPlayedWords;

                if (averageTime < 2000)
                {
                    fullGame.State = GameState.LowAverageTime;
                    analytics.LowAverageTime++;
                    continue;
                }

                analytics.RealGames++;
                fullGame.State = GameState.Real;
            }

            return analytics;
        }
    }
}