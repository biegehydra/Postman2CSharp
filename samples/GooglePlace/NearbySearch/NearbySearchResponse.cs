using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GooglePlace
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<NearbySearchResponse>(myJsonResponse);
    public class NearbySearchResponse
    {
        [JsonPropertyName("html_attributions")]
        public List<object> HtmlAttributions { get; set; }

        [JsonPropertyName("results")]
        public List<Results> Results { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

    public class Results
    {
        [JsonPropertyName("business_status")]
        public string BusinessStatus { get; set; }

        [JsonPropertyName("geometry")]
        public Geometry Geometry { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [JsonPropertyName("icon_background_color")]
        public string IconBackgroundColor { get; set; }

        [JsonPropertyName("icon_mask_base_uri")]
        public string IconMaskBaseUri { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("opening_hours")]
        public OpeningHours OpeningHours { get; set; }

        [JsonPropertyName("photos")]
        public List<Photos> Photos { get; set; }

        [JsonPropertyName("place_id")]
        public string PlaceId { get; set; }

        [JsonPropertyName("plus_code")]
        public PlusCode PlusCode { get; set; }

        [JsonPropertyName("price_level")]
        public int PriceLevel { get; set; }

        [JsonPropertyName("rating")]
        public double Rating { get; set; }

        [JsonPropertyName("reference")]
        public string Reference { get; set; }

        [JsonPropertyName("scope")]
        public string Scope { get; set; }

        [JsonPropertyName("types")]
        public List<string> Types { get; set; }

        [JsonPropertyName("user_ratings_total")]
        public int UserRatingsTotal { get; set; }

        [JsonPropertyName("vicinity")]
        public string Vicinity { get; set; }
    }
}