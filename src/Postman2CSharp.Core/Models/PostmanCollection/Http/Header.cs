using System;
using System.Collections.Generic;
using System.Text.Json;
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
            if (lowerKey is "content-length" or "user-agent" or  "accept-language" or "cache-control" or "sec-ch-ua"
                or "sec-fetch-mode" or "upgrade-insecure-requests" || string.IsNullOrWhiteSpace(lowerKey))
                return false;
            return true;
        }

        [JsonRequired] public required string Key { get; set; }
        [JsonRequired] public required string Value { get; set; }


        [JsonConverter(typeof(DescriptionConverter))]
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

        private class DescriptionConverter : JsonConverter<string?>
        {
            public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.StartObject)
                {
                    // Read the object and look for the 'content' property
                    using var jsonDoc = JsonDocument.ParseValue(ref reader);
                    if (jsonDoc.RootElement.TryGetProperty("content", out var contentProp))
                    {
                        return contentProp.GetString();
                    }

                    return null; // Or a default value if appropriate
                }
                if (reader.TokenType == JsonTokenType.String)
                {
                    // Read the string value directly
                    return reader.GetString();
                }

                throw new JsonException("Unexpected token type.");
            }

            public override void Write(Utf8JsonWriter writer, string? value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value);
            }
        }
    }
}