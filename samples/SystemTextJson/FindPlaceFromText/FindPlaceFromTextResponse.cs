using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<FindPlaceFromTextResponse>(myJsonResponse);
namespace SystemTextJson
{
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
        public OpeningHours2 OpeningHours { get; set; }
        public double Rating { get; set; }
    }

    public class OpeningHours2
    {
        public bool OpenNow { get; set; }
    }
}