namespace PPMGChronical.Models
{
    public class ChronicleEvent
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
