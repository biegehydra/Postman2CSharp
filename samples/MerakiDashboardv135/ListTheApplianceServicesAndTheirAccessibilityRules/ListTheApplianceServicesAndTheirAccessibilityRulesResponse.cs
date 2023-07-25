using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheApplianceServicesAndTheirAccessibilityRulesResponse>>(myJsonResponse);
    public class ListTheApplianceServicesAndTheirAccessibilityRulesResponse
    {
        public string Service { get; set; }
        public string Access { get; set; }
        public List<string> AllowedIps { get; set; }
    }
}