using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<OricalRecordsOfCommandsSentToSystemsManagerDevicesResponse>>(myJsonResponse);
    public class OricalRecordsOfCommandsSentToSystemsManagerDevicesResponse
    {
        public string Action { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string DashboardUser { get; set; }
        public DateTime Ts { get; set; }
    }
}