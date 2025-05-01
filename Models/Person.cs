namespace PPMGChronical.Models
{
    public class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Years { get; set; }  // e.g., "1995-2010"
        public string Biography { get; set; }
        public List<string> Achievements { get; set; } = new List<string>();
        public string ImageUrl { get; set; }
    }
}
