using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnThe1ManyNATMappingRulesForAnMXNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnThe1ManyNATMappingRulesForAnMXNetworkResponse
    {
        public List<Rules14> Rules { get; set; }
    }

    public class PortRules
    {
        public string Name { get; set; }
        public string Protocol { get; set; }
        public string PublicPort { get; set; }
        public string LocalIp { get; set; }
        public string LocalPort { get; set; }
        public List<string> AllowedIps { get; set; }
    }

    public class Rules14
    {
        public string PublicIp { get; set; }
        public string Uplink { get; set; }
        public List<PortRules> PortRules { get; set; }
    }
}