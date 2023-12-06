using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GooglePlace
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<PlaceDetailsResponse>(myJsonResponse);
    public class PlaceDetailsResponse
    {
        [JsonPropertyName("html_attributions")]
        public List<object> HtmlAttributions { get; set; }

        [JsonPropertyName("result")]
        public Result Result { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

    public class Result
    {
        [JsonPropertyName("address_components")]
        public List<AddressComponents> AddressComponents { get; set; }

        [JsonPropertyName("adr_address")]
        public string AdrAddress { get; set; }

        [JsonPropertyName("business_status")]
        public string BusinessStatus { get; set; }

        [JsonPropertyName("formatted_address")]
        public string FormattedAddress { get; set; }

        [JsonPropertyName("formatted_phone_number")]
        public string FormattedPhoneNumber { get; set; }

        [JsonPropertyName("geometry")]
        public Geometry Geometry { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [JsonPropertyName("icon_background_color")]
        public string IconBackgroundColor { get; set; }

        [JsonPropertyName("icon_mask_base_uri")]
        public string IconMaskBaseUri { get; set; }

        [JsonPropertyName("international_phone_number")]
        public string InternationalPhoneNumber { get; set; }

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

        [JsonPropertyName("rating")]
        public int Rating { get; set; }

        [JsonPropertyName("reference")]
        public string Reference { get; set; }

        [JsonPropertyName("reviews")]
        public List<Reviews> Reviews { get; set; }

        [JsonPropertyName("types")]
        public List<string> Types { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("user_ratings_total")]
        public int UserRatingsTotal { get; set; }

        [JsonPropertyName("utc_offset")]
        public int UtcOffset { get; set; }

        [JsonPropertyName("vicinity")]
        public string Vicinity { get; set; }

        [JsonPropertyName("website")]
        public string Website { get; set; }
    }

    public class Reviews
    {
        [JsonPropertyName("author_name")]
        public string AuthorName { get; set; }

        [JsonPropertyName("author_url")]
        public string AuthorUrl { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("profile_photo_url")]
        public string ProfilePhotoUrl { get; set; }

        [JsonPropertyName("rating")]
        public int Rating { get; set; }

        [JsonPropertyName("relative_time_description")]
        public string RelativeTimeDescription { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("time")]
        public int Time { get; set; }
    }

    public class Photos
    {
        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("html_attributions")]
        public List<string> HtmlAttributions { get; set; }

        [JsonPropertyName("photo_reference")]
        public string PhotoReference { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }
    }

    public class AddressComponents
    {
        [JsonPropertyName("long_name")]
        public string LongName { get; set; }

        [JsonPropertyName("short_name")]
        public string ShortName { get; set; }

        [JsonPropertyName("types")]
        public List<string> Types { get; set; }
    }

    public class OpeningHours
    {
        [JsonPropertyName("open_now")]
        public bool OpenNow { get; set; }

        [JsonPropertyName("periods")]
        public List<Periods> Periods { get; set; }

        [JsonPropertyName("weekday_text")]
        public List<string> WeekdayText { get; set; }
    }

    public class Close
    {
        [JsonPropertyName("day")]
        public int Day { get; set; }

        [JsonPropertyName("time")]
        public string Time { get; set; }
    }

    public class Geometry
    {
        [JsonPropertyName("location")]
        public Location Location { get; set; }

        [JsonPropertyName("viewport")]
        public Viewport Viewport { get; set; }
    }

    public class Location
    {
        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [JsonPropertyName("lng")]
        public double Lng { get; set; }
    }

    public class Periods
    {
        [JsonPropertyName("close")]
        public Close Close { get; set; }

        [JsonPropertyName("open")]
        public Close Open { get; set; }
    }

    public class PlusCode
    {
        [JsonPropertyName("compound_code")]
        public string CompoundCode { get; set; }

        [JsonPropertyName("global_code")]
        public string GlobalCode { get; set; }
    }

    public class Viewport
    {
        [JsonPropertyName("northeast")]
        public Location Northeast { get; set; }

        [JsonPropertyName("southwest")]
        public Location Southwest { get; set; }
    }
}