using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HatServer.DAL.Interfaces;
using Model;
using Model.Entities;

namespace HatServer.Services
{
    public interface IAnalyticsBusinessLogic
    {
        CommonAnalytics GetCommonAnalytics();
        Task<FullGame> GetFullGameByIdAsync(int id);
        List<FullGame> GetFullGames();
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

        public List<FullGame> GetFullGames()
        {
            var games = _gameRepository.GetFullGames().ToList();
            AnalyzeGames(games);
            return games.Where(g => g.State == GameState.Real).ToList();
        }
        
        public async Task<FullGame> GetFullGameByIdAsync(int id)
        {
            var game = await _gameRepository.GetFullGameAsync(id);
            if (game == null)
            {
                return null;
            }
            AnalyzeGame(game);
            return game;
        }

        private static CommonAnalytics AnalyzeGames(IReadOnlyCollection<FullGame> fullGames)
        {
            var analytics = new CommonAnalytics {TotalGames = fullGames.Count};
            foreach (var fullGame in fullGames)
            {
                AnalyzeGame(fullGame);
                switch (fullGame.State)
                {
                    case GameState.Real:
                        analytics.RealGames++;
                        break;
                    case GameState.NoRounds:
                        analytics.NoRounds++;
                        break;
                    case GameState.FewAddedWords:
                        analytics.FewAddedWords++;
                        break;
                    case GameState.FewPlayedRounds:
                        analytics.FewPlayedRounds++;
                        break;
                    case GameState.LowAverageTime:
                        analytics.LowAverageTime++;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return analytics;
        }

        private static void AnalyzeGame(FullGame fullGame)
        {
            if (fullGame.Rounds.Count == 0)
            {
                fullGame.State = GameState.NoRounds;
                return;
            }

            if (fullGame.Game.Words.Count <= 10)
            {
                fullGame.State = GameState.FewAddedWords;
                return;
            }

            if (fullGame.Game.Teams.SelectMany(t => t.Players).Count() > fullGame.Rounds.Count)
            {
                fullGame.State = GameState.FewPlayedRounds;
                return;
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
                return;
            }

            var averageTime = totalTime / numberOfPlayedWords;

            if (averageTime < 2000)
            {
                fullGame.State = GameState.LowAverageTime;
                return;
            }

            fullGame.State = GameState.Real;
        }
    }
}