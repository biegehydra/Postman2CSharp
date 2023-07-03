using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheSensorRolesForDevicesInAGivenNetworkResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ListTheSensorRolesForDevicesInAGivenNetworkResponse
    {
        public Device Device { get; set; }
        public Relationships Relationships { get; set; }
    }

    public class Device
    {
        public string Name { get; set; }
        public string Serial { get; set; }
        public string ProductType { get; set; }
    }

    public class Relationships
    {
        public Livestream Livestream { get; set; }
    }
}