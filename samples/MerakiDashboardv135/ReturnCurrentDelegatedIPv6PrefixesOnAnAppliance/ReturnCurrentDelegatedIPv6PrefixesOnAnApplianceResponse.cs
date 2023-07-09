using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ReturnCurrentDelegatedIPv6PrefixesOnAnApplianceResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ReturnCurrentDelegatedIPv6PrefixesOnAnApplianceResponse
    {
        public Origin Origin { get; set; }
        public string Prefix { get; set; }
        public Counts Counts { get; set; }
        public string Method { get; set; }
        public string Description { get; set; }
        public bool IsPreferred { get; set; }
        public DateTime ExpiresAt { get; set; }
    }

    public class Counts
    {
        public int Assigned { get; set; }
        public int Available { get; set; }
    }

    public class Origin
    {
        public string Interface { get; set; }
    }
}