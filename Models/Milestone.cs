namespace PPMGChronical.Models
{
    public class Milestone
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public string Importance { get; set; }  // e.g., "High", "Medium", "Low"
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
