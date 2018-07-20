namespace Model.Entities
{
    public class GamePhrase
    {
        public string Phrase { get; set; }

        public long Complexity { get; set; }

        public string Description { get; set; }

        public string[] Tags { get; set; }
    }
}