using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ElasticSearch.Models
{
    public class ResponseBookModel
    {
        [JsonProperty("took")]
        public int Took { get; set; }
        [JsonProperty("timed_out")]
        public bool TimedOut { get; set; }
        [JsonProperty("_shards")]
        public Shards Shards { get; set; }
        [JsonProperty("hits")]
        public Hits Hits { get; set; }
        
    }

    public class Hits
    {
        [JsonProperty("total")]
        public Total Totals { get; set; }
        [JsonProperty("max_score")]
        public double MaxScore { get; set; }
        [JsonProperty("hits")]
        public IEnumerable<InfoItems> Info { get; set; }
    }

    public class InfoItems
    {
        [JsonProperty("_index")]
        public string Index { get; set; }
        [JsonProperty("_type")]
        public string Type { get; set; }
        [JsonProperty("_id")]
        public string Id { get; set; }
        [JsonProperty("_score")]
        public double Score { get; set; }
        [JsonProperty("_source")]
        public BookModel Book { get; set; }
    }
    public class Shards
    {
        [JsonProperty("total")]
        public int Total { get; set; }
        [JsonProperty("successful")]
        public int Success { get; set; }
        [JsonProperty("skipped")]
        public int Skipped { get; set; }
        [JsonProperty("failed")]
        public int Failed { get; set; }
    }
    public class Total
    {
        [JsonProperty("value")]
        public int Value { get; set; }
        [JsonProperty("relation")]
        public string Relation { get; set; }
    }
}
