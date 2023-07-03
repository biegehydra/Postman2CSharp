using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateCommonDHCPSettingsOfMGsRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateCommonDHCPSettingsOfMGsRequest
    {
        public string DhcpLeaseTime { get; set; }
        public string DnsNameservers { get; set; }
        public List<string> DnsCustomNameservers { get; set; }
    }
}