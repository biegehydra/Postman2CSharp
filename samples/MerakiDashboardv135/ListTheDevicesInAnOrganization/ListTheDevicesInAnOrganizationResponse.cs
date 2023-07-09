using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheDevicesInAnOrganizationResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ListTheDevicesInAnOrganizationResponse
    {
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public string Tags { get; set; }
        public string NetworkId { get; set; }
        public string Serial { get; set; }
        public string Model { get; set; }
        public string Mac { get; set; }
        public string LanIp { get; set; }
        public string Firmware { get; set; }
    }
}