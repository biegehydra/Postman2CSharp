using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<LRecordsOfCommandsSentToSystemsManagerDevicesResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class LRecordsOfCommandsSentToSystemsManagerDevicesResponse
    {
        public string Action { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string DashboardUser { get; set; }
        public DateTime Ts { get; set; }
    }
}