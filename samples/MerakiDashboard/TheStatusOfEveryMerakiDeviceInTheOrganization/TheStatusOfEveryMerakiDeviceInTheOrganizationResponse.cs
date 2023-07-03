using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<TheStatusOfEveryMerakiDeviceInTheOrganizationResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class TheStatusOfEveryMerakiDeviceInTheOrganizationResponse
    {
        public string Name { get; set; }
        public string Serial { get; set; }
        public string Mac { get; set; }
        public string PublicIp { get; set; }
        public string NetworkId { get; set; }
        public string Status { get; set; }
        public DateTime LastReportedAt { get; set; }
        public string LanIp { get; set; }
        public string Gateway { get; set; }
        public string IpType { get; set; }
        public string PrimaryDns { get; set; }
        public string SecondaryDns { get; set; }
        public string ProductType { get; set; }
        public Components Components { get; set; }
        public string Model { get; set; }
        public List<string> Tags { get; set; }
    }

    public class Components
    {
        public List<object> PowerSupplies { get; set; }
    }
}