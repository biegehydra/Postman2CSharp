using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<CreateALayer3InterfaceForASwitchRequest>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class CreateALayer3InterfaceForASwitchRequest
    {
        public string Name { get; set; }
        public string Subnet { get; set; }
        public string InterfaceIp { get; set; }
        public string MulticastRouting { get; set; }
        public string VlanId { get; set; }
        public string DefaultGateway { get; set; }
        public OspfSettings2 OspfSettings { get; set; }
        public OspfV4 OspfV3 { get; set; }
        public Ipv9 Ipv6 { get; set; }
    }

    public class OspfSettings2
    {
        public string Area { get; set; }
        public string Cost { get; set; }
        public string IsPassiveEnabled { get; set; }
    }

    public class OspfV4
    {
        public string Area { get; set; }
        public string Cost { get; set; }
        public string IsPassiveEnabled { get; set; }
    }
}