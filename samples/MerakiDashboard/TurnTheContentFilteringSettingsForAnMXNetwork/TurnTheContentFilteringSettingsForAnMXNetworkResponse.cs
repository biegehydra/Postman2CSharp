using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<TurnTheContentFilteringSettingsForAnMXNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class TurnTheContentFilteringSettingsForAnMXNetworkResponse
    {
        public List<string> AllowedUrlPatterns { get; set; }
        public List<string> BlockedUrlPatterns { get; set; }
        public List<BlockedUrlCategories> BlockedUrlCategories { get; set; }
        public string UrlCategoryListSize { get; set; }
    }

    public class BlockedUrlCategories
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}