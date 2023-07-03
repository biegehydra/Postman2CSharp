using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ListTheEventsForTheNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ListTheEventsForTheNetworkResponse
    {
        public string Message { get; set; }
        public DateTime PageStartAt { get; set; }
        public DateTime PageEndAt { get; set; }
        public List<Events> Events { get; set; }
    }

    public class Events
    {
        public DateTime OccurredAt { get; set; }
        public string NetworkId { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string ClientId { get; set; }
        public string ClientDescription { get; set; }
        public string ClientMac { get; set; }
        public string DeviceSerial { get; set; }
        public string DeviceName { get; set; }
        public int SsidNumber { get; set; }
        public EventData2 EventData { get; set; }
    }

    public class EventData2
    {
        public string Radio { get; set; }
        public string Vap { get; set; }
        public string ClientMac { get; set; }
        public string ClientIp { get; set; }
        public string Channel { get; set; }
        public string Rssi { get; set; }
        public string Aid { get; set; }
    }
}