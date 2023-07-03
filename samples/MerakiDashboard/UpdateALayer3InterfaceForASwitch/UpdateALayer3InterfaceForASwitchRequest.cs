using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateALayer3InterfaceForASwitchRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateALayer3InterfaceForASwitchRequest
    {
        public string Name { get; set; }
        public string Subnet { get; set; }
        public string InterfaceIp { get; set; }
        public string MulticastRouting { get; set; }
        public string VlanId { get; set; }
        public string DefaultGateway { get; set; }
        public OspfSettings OspfSettings { get; set; }
        public OspfV3 OspfV3 { get; set; }
        public Ipv6 Ipv6 { get; set; }
    }
}