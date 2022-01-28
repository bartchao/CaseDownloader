using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SourceDownloader.Sunshine
{
    public class CourtsList
    {
        [JsonPropertyName("courts")]
        public List<Court> Court { get; set; } = new List<Court>();
    }
    public class Court
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("simple_name")]
        public string SimpleName { get; set; }
        [JsonPropertyName("code")]
        public string Code
        {
            get; set;
        }
    }
}
