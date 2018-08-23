using System;
using System.Collections.Generic;

namespace HatServer.DTO.Request
{
    public class PostGameRequest
    {
        public int Round { get; set; }
        public string Id { get; set; }
        public PhraseDTO[] Words { get; set; }
        public string DeviceId { get; set; }
        public TeamDTO[] TeamsDTO { get; set; }
        public int Timestamp { get; set; }
        public int Stage { get; set; }
    }

    public class PhraseDTO
    {
        public string Word { get; set; }
        public int Id { get; set; }
        public bool BadItalic { get; set; }
        public int PackId { get; set; }
    }

    public class TeamDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PlayerDTO[] PlayersDTO { get; set; }
    }

    public class PlayerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Penalty { get; set; }
    }
}