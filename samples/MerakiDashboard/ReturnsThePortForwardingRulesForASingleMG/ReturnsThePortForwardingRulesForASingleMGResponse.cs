using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnsThePortForwardingRulesForASingleMGResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnsThePortForwardingRulesForASingleMGResponse
    {
        public List<Rules26> Rules { get; set; }
    }

    public class Rules26
    {
        public string LanIp { get; set; }
        public string Name { get; set; }
        public string Access { get; set; }
        public string PublicPort { get; set; }
        public string LocalPort { get; set; }
        public string Uplink { get; set; }
        public string Protocol { get; set; }
        public List<string> AllowedIps { get; set; }
    }
}