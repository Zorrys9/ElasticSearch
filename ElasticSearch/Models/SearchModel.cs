using System;

namespace ElasticSearch.Models
{
    public class SearchModel
    {
        public string Category { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int FirstCountPages { get; set; } = 0;
        public int LastCountPages { get; set; } = Int32.MaxValue;
        public DateTime FirstDate { get; set; } = DateTime.MinValue;
        public DateTime LastDate { get; set; } = DateTime.Now;
    }
}
