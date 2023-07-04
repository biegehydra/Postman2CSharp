using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<StatusesInformationForDevicesInAnOrganizationResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class StatusesInformationForDevicesInAnOrganizationResponse
    {
        public string Mac { get; set; }
        public string Name { get; set; }
        public ResultingNetworks Network { get; set; }
        public string ProductType { get; set; }
        public string Serial { get; set; }
        public string Status { get; set; }
        public List<string> Tags { get; set; }
    }
}