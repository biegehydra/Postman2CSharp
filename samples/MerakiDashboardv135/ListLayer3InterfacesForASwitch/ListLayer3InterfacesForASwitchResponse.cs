using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListLayer3InterfacesForASwitchResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ListLayer3InterfacesForASwitchResponse
    {
        public string InterfaceId { get; set; }
        public string Name { get; set; }
        public string Subnet { get; set; }
        public string InterfaceIp { get; set; }
        public string MulticastRouting { get; set; }
        public int VlanId { get; set; }
        public string DefaultGateway { get; set; }
        public OspfSettings OspfSettings { get; set; }
        public OspfV3 OspfV3 { get; set; }
        public Ipv9 Ipv6 { get; set; }
    }

    public class Ipv9
    {
        public string AssignmentMode { get; set; }
        public string Address { get; set; }
        public string Prefix { get; set; }
        public string Gateway { get; set; }
    }

    public class OspfSettings
    {
        public string Area { get; set; }
        public int Cost { get; set; }
        public bool IsPassiveEnabled { get; set; }
    }

    public class OspfV3
    {
        public string Area { get; set; }
        public int Cost { get; set; }
        public bool IsPassiveEnabled { get; set; }
    }
}