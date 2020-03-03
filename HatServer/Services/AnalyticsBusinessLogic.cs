using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HatServer.DAL.Interfaces;
using Model;

namespace HatServer.Services
{
    public interface IAnalyticsBusinessLogic
    {
        Task<CommonAnalytics> GetCommonAnalyticsAsync();
        Task<FullGame> GetFullGameByIdAsync(int id);
        Task<List<FullGame>> GetFullGamesAsync();
    }

    public class AnalyticsBusinessLogic : IAnalyticsBusinessLogic
    {
        private readonly IGameRepository _gameRepository;

        public AnalyticsBusinessLogic(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<CommonAnalytics> GetCommonAnalyticsAsync()
        {
            var fullGames = await _gameRepository.GetFullGamesAsync();
            //.Where(g => g.Game.InGameId.Equals("5C140301-7381-4E65-B46F-817A5359DBD4_1535379624", StringComparison.CurrentCultureIgnoreCase))
            //.ToList();

            return AnalyzeGames(fullGames);
        }

        public async Task<List<FullGame>> GetFullGamesAsync()
        {
            var games = await _gameRepository.GetFullGamesAsync();
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
            var analytics = new CommonAnalytics { TotalGames = fullGames.Count };
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