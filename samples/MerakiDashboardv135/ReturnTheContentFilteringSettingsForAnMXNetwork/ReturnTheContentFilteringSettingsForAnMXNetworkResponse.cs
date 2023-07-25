using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ReturnTheContentFilteringSettingsForAnMXNetworkResponse>(myJsonResponse);
    public class ReturnTheContentFilteringSettingsForAnMXNetworkResponse
    {
        public List<string> AllowedUrlPatterns { get; set; }
        public List<string> BlockedUrlPatterns { get; set; }
        public List<ListTheOrganizationsResponse> BlockedUrlCategories { get; set; }
        public string UrlCategoryListSize { get; set; }
    }
}