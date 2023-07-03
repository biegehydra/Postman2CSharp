using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<HesentandreceivedbytesforeachuplinkofanetworkResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class HesentandreceivedbytesforeachuplinkofanetworkResponse
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<ByInterface> ByInterface { get; set; }
    }

    public class ByInterface
    {
        public string Interface { get; set; }
        public int Sent { get; set; }
        public int Received { get; set; }
    }
}