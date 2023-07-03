using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<OuntsovertimeforanetworkdeviceornetworkclientResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class OuntsovertimeforanetworkdeviceornetworkclientResponse
    {
        public DateTime StartTs { get; set; }
        public DateTime EndTs { get; set; }
        public int ClientCount { get; set; }
    }
}