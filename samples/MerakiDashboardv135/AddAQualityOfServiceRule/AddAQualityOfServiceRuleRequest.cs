using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<AddAQualityOfServiceRuleRequest>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class AddAQualityOfServiceRuleRequest
    {
        public string Vlan { get; set; }
        public string Protocol { get; set; }
        public string SrcPort { get; set; }
        public string SrcPortRange { get; set; }
        public string DstPort { get; set; }
        public string DstPortRange { get; set; }
        public string Dscp { get; set; }
    }
}