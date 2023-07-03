using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnTheAccessControlListsForAMSNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnTheAccessControlListsForAMSNetworkResponse
    {
        public List<Rules29> Rules { get; set; }
    }

    public class Rules29
    {
        public string Comment { get; set; }
        public string Policy { get; set; }
        public string IpVersion { get; set; }
        public string Protocol { get; set; }
        public string SrcCidr { get; set; }
        public string SrcPort { get; set; }
        public string DstCidr { get; set; }
        public string DstPort { get; set; }
        public string Vlan { get; set; }
    }
}