namespace HatServer.DTO.Response
{
    public class GamePackEmptyResponse
    {
        public int Id { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public int Version { get; set; }
        public bool Paid { get; set; }
    }
}