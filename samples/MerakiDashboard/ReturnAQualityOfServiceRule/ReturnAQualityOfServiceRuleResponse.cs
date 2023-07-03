using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnAQualityOfServiceRuleResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnAQualityOfServiceRuleResponse
    {
        public string Id { get; set; }
        public int Vlan { get; set; }
        public string Protocol { get; set; }
        public int SrcPort { get; set; }
        public string SrcPortRange { get; set; }
        public int DstPort { get; set; }
        public string DstPortRange { get; set; }
        public int Dscp { get; set; }
    }
}