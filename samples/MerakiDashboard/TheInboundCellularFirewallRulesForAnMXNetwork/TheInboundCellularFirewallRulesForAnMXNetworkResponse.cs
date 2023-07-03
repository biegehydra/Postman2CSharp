using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<TheInboundCellularFirewallRulesForAnMXNetworkResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class TheInboundCellularFirewallRulesForAnMXNetworkResponse
    {
        public string Comment { get; set; }
        public string Policy { get; set; }
        public string Protocol { get; set; }
        public string DestPort { get; set; }
        public string DestCidr { get; set; }
        public string SrcPort { get; set; }
        public string SrcCidr { get; set; }
        public bool SyslogEnabled { get; set; }
    }
}