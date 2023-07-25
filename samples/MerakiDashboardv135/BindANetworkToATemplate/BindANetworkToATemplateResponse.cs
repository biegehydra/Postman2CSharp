using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<BindANetworkToATemplateResponse>(myJsonResponse);
    public class BindANetworkToATemplateResponse
    {
        public string Id { get; set; }
        public string OrganizationId { get; set; }
        public string Name { get; set; }
        public string TimeZone { get; set; }
        public List<string> Tags { get; set; }
        public List<string> ProductTypes { get; set; }
        public string EnrollmentString { get; set; }
        public string Notes { get; set; }
        public bool IsBoundToConfigTemplate { get; set; }
        public string ConfigTemplateId { get; set; }
    }
}