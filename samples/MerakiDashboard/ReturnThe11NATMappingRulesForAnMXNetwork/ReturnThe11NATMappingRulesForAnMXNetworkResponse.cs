using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnThe11NATMappingRulesForAnMXNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnThe11NATMappingRulesForAnMXNetworkResponse
    {
        public List<Rules17> Rules { get; set; }
    }

    public class Rules17
    {
        public string Name { get; set; }
        public string LanIp { get; set; }
        public string PublicIp { get; set; }
        public string Uplink { get; set; }
        public List<AllowedInbound> AllowedInbound { get; set; }
    }

    public class AllowedInbound
    {
        public string Protocol { get; set; }
        public List<string> DestinationPorts { get; set; }
        public List<string> AllowedIps { get; set; }
    }
}