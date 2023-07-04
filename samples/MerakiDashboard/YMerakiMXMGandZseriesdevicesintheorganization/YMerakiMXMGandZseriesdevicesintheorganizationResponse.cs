using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<YMerakiMXMGAndZSeriesDevicesInTheOrganizationResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class YMerakiMXMGAndZSeriesDevicesInTheOrganizationResponse
    {
        public string NetworkId { get; set; }
        public string Serial { get; set; }
        public string Model { get; set; }
        public DateTime LastReportedAt { get; set; }
        public List<Uplinks> Uplinks { get; set; }
    }
}