using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<AddAQualityOfServiceRuleRequest>(myJsonResponse);
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