using System.Collections.Generic;
using Model.Entities;

namespace Model
{
    public class FullGame
    {
        public Game Game { get; set; }
        public List<Round> Rounds { get; set; }

        public GameState State { get; set; }
    }

    public enum GameState
    {
        Unknown,
        Real,
        NoRounds,
        FewAddedWords,
        FewPlayedRounds,
        ZeroPlayedWords,
        LowAverageTime
    }
}