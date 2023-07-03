using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<NearbySearchResponse>(myJsonResponse);
namespace SystemTextJson
{
    public class NearbySearchResponse
    {
        public List<object> HtmlAttributions { get; set; }
        public List<Results> Results { get; set; }
        public string Status { get; set; }
    }

    public class Results
    {
        public string BusinessStatus { get; set; }
        public Geometry Geometry { get; set; }
        public string Icon { get; set; }
        public string IconBackgroundColor { get; set; }
        public string IconMaskBaseUri { get; set; }
        public string Name { get; set; }
        public OpeningHours OpeningHours { get; set; }
        public List<Photos> Photos { get; set; }
        public string PlaceId { get; set; }
        public PlusCode PlusCode { get; set; }
        public int PriceLevel { get; set; }
        public double Rating { get; set; }
        public string Reference { get; set; }
        public string Scope { get; set; }
        public List<string> Types { get; set; }
        public int UserRatingsTotal { get; set; }
        public string Vicinity { get; set; }
    }
}