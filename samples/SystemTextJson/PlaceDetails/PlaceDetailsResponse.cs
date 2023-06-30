using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<PlaceDetailsResponse>(myJsonResponse);
namespace SystemTextJson
{
    public class AddressComponents
    {
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public List<string> Types { get; set; }
    }

    public class Close
    {
        public int Day { get; set; }
        public string Time { get; set; }
    }

    public class Geometry
    {
        public Location Location { get; set; }
        public Viewport Viewport { get; set; }
    }

    public class Location
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class Northeast
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class Open
    {
        public int Day { get; set; }
        public string Time { get; set; }
    }

    public class OpeningHours
    {
        public bool OpenNow { get; set; }
        public List<Periods> Periods { get; set; }
        public List<string> WeekdayText { get; set; }
    }

    public class Periods
    {
        public Close Close { get; set; }
        public Open Open { get; set; }
    }

    public class Photos
    {
        public int Height { get; set; }
        public List<string> HtmlAttributions { get; set; }
        public string PhotoReference { get; set; }
        public int Width { get; set; }
    }

    public class PlaceDetailsResponse
    {
        public List<object> HtmlAttributions { get; set; }
        public Result Result { get; set; }
        public string Status { get; set; }
    }

    public class PlusCode
    {
        public string CompoundCode { get; set; }
        public string GlobalCode { get; set; }
    }

    public class Result
    {
        public List<AddressComponents> AddressComponents { get; set; }
        public string AdrAddress { get; set; }
        public string BusinessStatus { get; set; }
        public string FormattedAddress { get; set; }
        public string FormattedPhoneNumber { get; set; }
        public Geometry Geometry { get; set; }
        public string Icon { get; set; }
        public string IconBackgroundColor { get; set; }
        public string IconMaskBaseUri { get; set; }
        public string InternationalPhoneNumber { get; set; }
        public string Name { get; set; }
        public OpeningHours OpeningHours { get; set; }
        public List<Photos> Photos { get; set; }
        public string PlaceId { get; set; }
        public PlusCode PlusCode { get; set; }
        public int Rating { get; set; }
        public string Reference { get; set; }
        public List<Reviews> Reviews { get; set; }
        public List<string> Types { get; set; }
        public string Url { get; set; }
        public int UserRatingsTotal { get; set; }
        public int UtcOffset { get; set; }
        public string Vicinity { get; set; }
        public string Website { get; set; }
    }

    public class Reviews
    {
        public string AuthorName { get; set; }
        public string AuthorUrl { get; set; }
        public string Language { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public int Rating { get; set; }
        public string RelativeTimeDescription { get; set; }
        public string Text { get; set; }
        public int Time { get; set; }
    }

    public class Southwest
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class Viewport
    {
        public Northeast Northeast { get; set; }
        public Southwest Southwest { get; set; }
    }
}