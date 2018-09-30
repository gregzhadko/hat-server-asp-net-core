namespace HatServer.DTO.Request
{
    public class PostRoundRequest
    {
        public string GameId { get; set; }
        public int RoundNumber { get; set; }
        public int PlayerId { get; set; }
        public RoundPhraseDTO[] Words { get; set; }
        public SettingsDTO Settings { get; set; }
        public string DeviceId { get; set; }
        public int Timestamp { get; set; }
        public int Time { get; set; }
        public int Stage { get; set; }
    }

    public class SettingsDTO
    {
        public int RoundTime { get; set; }
        public bool CanChangeWord { get; set; }
        public bool BadItalicSimulated { get; set; }
    }

    public class RoundPhraseDTO
    {
        public string State { get; set; }
        public int WordId { get; set; }
        public int Time { get; set; }
    }
}
