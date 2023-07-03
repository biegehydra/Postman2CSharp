using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<EndpointToSeePowerStatusForWirelessDevicesResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class EndpointToSeePowerStatusForWirelessDevicesResponse
    {
        public string Serial { get; set; }
        public string Name { get; set; }
        public Network Network { get; set; }
        public Power Power { get; set; }
        public List<Ports> Ports { get; set; }
        public Aggregation Aggregation { get; set; }
    }

    public class Ports
    {
        public string Name { get; set; }
        public Poe Poe { get; set; }
        public LinkNegotiation LinkNegotiation { get; set; }
    }

    public class Power
    {
        public string Mode { get; set; }
        public Ac Ac { get; set; }
        public Poe Poe { get; set; }
    }

    public class Aggregation
    {
        public bool Enabled { get; set; }
        public int Speed { get; set; }
    }

    public class LinkNegotiation
    {
        public string Duplex { get; set; }
        public int Speed { get; set; }
    }

    public class Poe
    {
        public bool IsConnected { get; set; }
        public string Standard { get; set; }
    }

    public class Ac
    {
        public bool IsConnected { get; set; }
    }
}