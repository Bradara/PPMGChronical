namespace PPMGChronical.Models
{
    public class MediaItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }  // e.g., "Photo", "Document", "Video"
        public int Year { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }
}
