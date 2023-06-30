using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// Root myDeserializedClass = JsonConvert.DeserializeObject<QueryAutocompleteResponse>(myJsonResponse);
namespace Postman2CSharp
{
    public class MainTextMatchedSubstrings
    {
        public int Length { get; set; }
        public int Offset { get; set; }
    }

    public class MatchedSubstrings
    {
        public int Length { get; set; }
        public int Offset { get; set; }
    }

    public class Predictions
    {
        public string Description { get; set; }
        public List<MatchedSubstrings> MatchedSubstrings { get; set; }
        public StructuredFormatting StructuredFormatting { get; set; }
        public List<Terms> Terms { get; set; }
    }

    public class QueryAutocompleteResponse
    {
        public List<Predictions> Predictions { get; set; }
        public string Status { get; set; }
    }

    public class SecondaryTextMatchedSubstrings
    {
        public int Length { get; set; }
        public int Offset { get; set; }
    }

    public class StructuredFormatting
    {
        public string MainText { get; set; }
        public List<MainTextMatchedSubstrings> MainTextMatchedSubstrings { get; set; }
        public string SecondaryText { get; set; }
        public List<SecondaryTextMatchedSubstrings> SecondaryTextMatchedSubstrings { get; set; }
    }

    public class Terms
    {
        public int Offset { get; set; }
        public string Value { get; set; }
    }
}