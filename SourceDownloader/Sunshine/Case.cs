using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SourceDownloader.Sunshine
{
    public class VerdictWrapper
    {
        [JsonPropertyName("verdict")]
        public Verdict Verdict { get; set; }
    }
    public class Verdict
    {
        [JsonPropertyName("story")]
        public Story Story { get; set; }
        [JsonPropertyName("court")]
        public Court Court { get; set; }
        [JsonPropertyName("body")]
        public Body Body { get; set; }
    }
    public class Story
    {
        [JsonPropertyName("identity")]
        public Identity Identity { get; set; }
        [JsonPropertyName("reason")]
        public string Reason { get; set; }
        [JsonPropertyName("judges_name")]
        public string[] JudgeName { get; set; }
    }
    public class Identity
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("year")]
        public int Year { get; set; }
        [JsonPropertyName("word")]
        public string Word { get; set; }
        [JsonPropertyName("number")]
        public int Number { get; set; }
    }
    public class Body
    {
        [JsonPropertyName("raw_html_url")]
        public string RawHtmlUrl { get; set; }
        private string contentUrl;
        [JsonPropertyName("content_url")]
        public string ContentUrl
        {
            get
            {
                return contentUrl;
            }
            set
            {
                contentUrl = value;
                Content = Downloader.GetMainContent(contentUrl);
            }
        }
        public MainContent Content { get; private set; }
    }
    public class MainContent
    {
        [JsonPropertyName("main_content")]
        public string Content { get; set; }
        
    }
}


