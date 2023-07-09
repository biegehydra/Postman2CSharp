using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnASingleDeviceResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ReturnASingleDeviceResponse
    {
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Serial { get; set; }
        public string Mac { get; set; }
        public string Model { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public string LanIp { get; set; }
        public List<string> Tags { get; set; }
        public string NetworkId { get; set; }
        public BeaconIdParams BeaconIdParams { get; set; }
        public string Firmware { get; set; }
        public string FloorPlanId { get; set; }
    }

    public class BeaconIdParams
    {
        public string Uuid { get; set; }
        public int Major { get; set; }
        public int Minor { get; set; }
    }
}