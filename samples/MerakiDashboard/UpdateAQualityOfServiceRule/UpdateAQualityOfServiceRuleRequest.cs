using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateAQualityOfServiceRuleRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateAQualityOfServiceRuleRequest
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