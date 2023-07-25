using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<CtivityEventsForAClientWithinANetworkInTheTimespanResponse>>(myJsonResponse);
    public class CtivityEventsForAClientWithinANetworkInTheTimespanResponse
    {
        public int OccurredAt { get; set; }
        public string DeviceSerial { get; set; }
        public int Band { get; set; }
        public int SsidNumber { get; set; }
        public string Type { get; set; }
        public string Subtype { get; set; }
        public string Severity { get; set; }
        public double DurationMs { get; set; }
        public int Channel { get; set; }
        public int Rssi { get; set; }
        public EventData EventData { get; set; }
    }

    public class EventData
    {
        public string ClientIp { get; set; }
    }
}