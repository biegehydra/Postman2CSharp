using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<EturnAPusageovertimeforadeviceornetworkclientResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class EturnAPusageovertimeforadeviceornetworkclientResponse
    {
        public DateTime StartTs { get; set; }
        public DateTime EndTs { get; set; }
        public int TotalKbps { get; set; }
        public int SentKbps { get; set; }
        public int ReceivedKbps { get; set; }
    }
}