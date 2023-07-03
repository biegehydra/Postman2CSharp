using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<HetheradeviceisregularlycheckingintoDashboardResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class HetheradeviceisregularlycheckingintoDashboardResponse
    {
        public DateTime FirstSeenAt { get; set; }
        public DateTime LastSeenAt { get; set; }
    }
}