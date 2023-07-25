using System;
using System.Collections.Generic;
using System.Text.Json;

namespace SystemTextJson
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<TextSearchResponse>(myJsonResponse);
    public class TextSearchResponse
    {
        public List<object> HtmlAttributions { get; set; }
        public string NextPageToken { get; set; }
        public List<Results2> Results { get; set; }
        public string Status { get; set; }
    }

    public class Results2
    {
        public string BusinessStatus { get; set; }
        public string FormattedAddress { get; set; }
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
        public List<string> Types { get; set; }
        public int UserRatingsTotal { get; set; }
    }
}