using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnThePortForwardingRulesForAnMXNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnThePortForwardingRulesForAnMXNetworkResponse
    {
        public List<Rules20> Rules { get; set; }
    }

    public class Rules20
    {
        public string LanIp { get; set; }
        public List<string> AllowedIps { get; set; }
        public string Name { get; set; }
        public string Protocol { get; set; }
        public string PublicPort { get; set; }
        public string LocalPort { get; set; }
        public string Uplink { get; set; }
    }
}