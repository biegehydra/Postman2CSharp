using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<DateTheContentFilteringSettingsForAnMXNetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class DateTheContentFilteringSettingsForAnMXNetworkRequest
    {
        public List<string> AllowedUrlPatterns { get; set; }
        public List<string> BlockedUrlPatterns { get; set; }
        public List<string> BlockedUrlCategories { get; set; }
        public string UrlCategoryListSize { get; set; }
    }
}