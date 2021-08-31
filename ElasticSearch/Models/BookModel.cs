using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearch.Models
{
    public class BookModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("date")]
        public DateTime Date { get; set; }
        [JsonProperty("countPages")]
        public int CountPages { get; set; }
        [JsonProperty("countBooks")]
        public int CountBooks { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
