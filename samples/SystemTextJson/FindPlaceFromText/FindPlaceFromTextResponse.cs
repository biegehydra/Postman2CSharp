using System;
using System.Collections.Generic;
using System.Text.Json;

namespace SystemTextJson
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<FindPlaceFromTextResponse>(myJsonResponse);
    public class FindPlaceFromTextResponse
    {
        public List<Candidates> Candidates { get; set; }
        public string Status { get; set; }
    }

    public class Candidates
    {
        public string FormattedAddress { get; set; }
        public Geometry Geometry { get; set; }
        public string Name { get; set; }
        public OpeningHours OpeningHours { get; set; }
        public double Rating { get; set; }
    }
}