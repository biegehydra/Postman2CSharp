using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListtheNetworksinanOrganizationResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ListtheNetworksinanOrganizationResponse
    {
        public string Id { get; set; }
        public string OrganizationId { get; set; }
        public string Name { get; set; }
        public string TimeZone { get; set; }
        public List<string> Tags { get; set; }
        public List<string> ProductTypes { get; set; }
        public string EnrollmentString { get; set; }
    }
}