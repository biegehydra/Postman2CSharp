using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<UpdateANetworkRequest>(myJsonResponse);
    public class UpdateANetworkRequest
    {
        public string Name { get; set; }
        public string TimeZone { get; set; }
        public List<string> Tags { get; set; }
        public string EnrollmentString { get; set; }
        public string Notes { get; set; }
    }
}