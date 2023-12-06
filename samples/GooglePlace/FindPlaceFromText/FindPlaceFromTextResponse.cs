using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GooglePlace
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<FindPlaceFromTextResponse>(myJsonResponse);
    public class FindPlaceFromTextResponse
    {
        [JsonPropertyName("candidates")]
        public List<Candidates> Candidates { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

    public class Candidates
    {
        [JsonPropertyName("formatted_address")]
        public string FormattedAddress { get; set; }

        [JsonPropertyName("geometry")]
        public Geometry Geometry { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("opening_hours")]
        public OpeningHours OpeningHours { get; set; }

        [JsonPropertyName("rating")]
        public double Rating { get; set; }
    }
}