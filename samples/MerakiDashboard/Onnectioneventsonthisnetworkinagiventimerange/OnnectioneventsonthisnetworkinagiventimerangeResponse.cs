using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<OnnectioneventsonthisnetworkinagiventimerangeResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class OnnectioneventsonthisnetworkinagiventimerangeResponse
    {
        public int SsidNumber { get; set; }
        public int Vlan { get; set; }
        public string ClientMac { get; set; }
        public string Serial { get; set; }
        public string FailureStep { get; set; }
        public string Type { get; set; }
        public DateTime Ts { get; set; }
    }
}