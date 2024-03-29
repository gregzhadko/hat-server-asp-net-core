﻿using System.Collections.Generic;

namespace HatServer.DTO.Response
{
    public class GamePackResponse
    {
        public int Id { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<GamePhraseResponse> Phrases { get; set; }
        public int Version { get; set; }
        public bool Paid { get; set; }
    }
}