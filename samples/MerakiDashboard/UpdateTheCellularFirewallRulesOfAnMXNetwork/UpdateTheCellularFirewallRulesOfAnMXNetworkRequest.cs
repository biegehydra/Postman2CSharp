using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheCellularFirewallRulesOfAnMXNetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheCellularFirewallRulesOfAnMXNetworkRequest
    {
        public List<Rules2> Rules { get; set; }
    }

    public class Rules2
    {
        public string Policy { get; set; }
        public string Protocol { get; set; }
        public string SrcCidr { get; set; }
        public string DestCidr { get; set; }
        public string Comment { get; set; }
        public string SrcPort { get; set; }
        public string DestPort { get; set; }
        public string SyslogEnabled { get; set; }
    }
}