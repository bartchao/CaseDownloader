using FileDownloader.JSON;
using FileDownloader.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SourceDownloader.Sunshine.Search
{
    public class SearchResult
    {
        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; set; }
        [JsonPropertyName("stories")]
        public SearchResultStory[] Story { get; set; }
    }
    public class SearchResultStory : Story
    {
        [JsonPropertyName("detail_url")]
        public string DetailUrl { get; set; }
        [JsonPropertyName("court")]
        public Court Court { get; set; }
    }
    public class Pagination
    {
        [JsonPropertyName("current")]
        public int Current { get; set; }
        [JsonPropertyName("previous_url")]
        public string PreviousUrl { get; set; }
        [JsonPropertyName("next_url")]
        public string NextUrl { get; set;}
        [JsonPropertyName("pages")]
        public int Pages { get; set; }
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
