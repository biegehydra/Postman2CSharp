using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<CreateALayer3InterfaceForASwitchRequest>(myJsonResponse);
    public class CreateALayer3InterfaceForASwitchRequest
    {
        public string Name { get; set; }
        public string Subnet { get; set; }
        public string InterfaceIp { get; set; }
        public string MulticastRouting { get; set; }
        public string VlanId { get; set; }
        public string DefaultGateway { get; set; }
        public OspfSettings2 OspfSettings { get; set; }
        public OspfSettings OspfV3 { get; set; }
        public Ipv9 Ipv6 { get; set; }
    }

    public class OspfSettings2
    {
        public string Area { get; set; }
        public string Cost { get; set; }
        public string IsPassiveEnabled { get; set; }
    }
}