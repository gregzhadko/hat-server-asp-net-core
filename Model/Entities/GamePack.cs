namespace Model.Entities
{
    public class GamePack
    {
        public long Id { get; set; }

        public string Language { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public GamePhrase[] Phrases { get; set; }

        public long Version { get; set; }

        public bool Paid { get; set; }
        
        public int Count { get; set; }
    }
}