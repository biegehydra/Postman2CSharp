using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GooglePlace
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<QueryAutocompleteResponse>(myJsonResponse);
    public class QueryAutocompleteResponse
    {
        [JsonPropertyName("predictions")]
        public List<Predictions> Predictions { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

    public class Predictions
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("matched_substrings")]
        public List<MainTextMatchedSubstrings> MatchedSubstrings { get; set; }

        [JsonPropertyName("structured_formatting")]
        public StructuredFormatting StructuredFormatting { get; set; }

        [JsonPropertyName("terms")]
        public List<Terms> Terms { get; set; }
    }

    public class StructuredFormatting
    {
        [JsonPropertyName("main_text")]
        public string MainText { get; set; }

        [JsonPropertyName("main_text_matched_substrings")]
        public List<MainTextMatchedSubstrings> MainTextMatchedSubstrings { get; set; }

        [JsonPropertyName("secondary_text")]
        public string SecondaryText { get; set; }

        [JsonPropertyName("secondary_text_matched_substrings")]
        public List<MainTextMatchedSubstrings> SecondaryTextMatchedSubstrings { get; set; }
    }

    public class MainTextMatchedSubstrings
    {
        [JsonPropertyName("length")]
        public int Length { get; set; }

        [JsonPropertyName("offset")]
        public int Offset { get; set; }
    }

    public class Terms
    {
        [JsonPropertyName("offset")]
        public int Offset { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}