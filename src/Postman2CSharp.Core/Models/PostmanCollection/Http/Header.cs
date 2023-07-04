using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Postman2CSharp.Core.Utilities;

namespace Postman2CSharp.Core.Models.PostmanCollection.Http
{
    public class Header : ICsProperty
    {
        private static readonly List<string> CommonAuthHeaders = new List<string>()
        {
            "ApiKey",
            "Bearer",
        };

        public static bool IsImportant(Header header)
        {
            var lowerKey = header.Key.ToLower();
            var lowerValue = header.Value.ToLower();
            if (lowerKey == "accept" && lowerValue == "application/json")
                return false;
            if(lowerKey == "content-type" && lowerValue == "application/json")
                return false;
            if (lowerKey == "content-type" && lowerValue == "application/x-www-form-urlencoded")
                return false;
            if (lowerKey == "content-length")
                return false;
            return true;
        }

        [JsonRequired] public required string Key { get; set; }
        [JsonRequired] public required string Value { get; set; }
        public string? Description { get; set; }
        public bool? Disabled { get; set; }
        public override bool Equals(object? obj)
        {
            return obj is Header header &&
                   Key == header.Key;
        }

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return HashCode.Combine(Key);
        }
    }
}