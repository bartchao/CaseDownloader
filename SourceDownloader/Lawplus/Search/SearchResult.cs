using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SourceDownloader.Lawplus.Search
{
    public class SearchResult
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }
        [JsonPropertyName("records")]
        public int Records { get; set; }
        [JsonPropertyName("rows")]
        public SearchResultRow[] SearchResultRows { get; set; }
    }
    public class SearchResultRow
    {
        [JsonPropertyName("caseNum")]
        public string CaseNum { get; set; }
        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }
        [JsonPropertyName("issue")]
        public string Issue { get; set; }
        [JsonPropertyName("judgeDate")]
        public string JudgeDate { get; set; }
    }
}
