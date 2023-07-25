using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ListCommonDHCPSettingsOfMGsResponse>(myJsonResponse);
    public class ListCommonDHCPSettingsOfMGsResponse
    {
        public string DhcpLeaseTime { get; set; }
        public string DnsNameservers { get; set; }
        public List<string> DnsCustomNameservers { get; set; }
    }
}