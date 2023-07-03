using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateCommonDHCPSettingsOfMGsResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateCommonDHCPSettingsOfMGsResponse
    {
        public string DhcpLeaseTime { get; set; }
        public string DnsNameservers { get; set; }
        public List<string> DnsCustomNameservers { get; set; }
    }
}