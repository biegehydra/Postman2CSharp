using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateASwitchPortRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateASwitchPortRequest
    {
        public string Name { get; set; }
        public List<string> Tags { get; set; }
        public string Enabled { get; set; }
        public string PoeEnabled { get; set; }
        public string Type { get; set; }
        public string Vlan { get; set; }
        public string VoiceVlan { get; set; }
        public string AllowedVlans { get; set; }
        public string IsolationEnabled { get; set; }
        public string RstpEnabled { get; set; }
        public string StpGuard { get; set; }
        public string LinkNegotiation { get; set; }
        public string PortScheduleId { get; set; }
        public string Udld { get; set; }
        public string AccessPolicyType { get; set; }
        public string AccessPolicyNumber { get; set; }
        public List<string> MacAllowList { get; set; }
        public List<string> StickyMacAllowList { get; set; }
        public string StickyMacAllowListLimit { get; set; }
        public string StormControlEnabled { get; set; }
        public string AdaptivePolicyGroupId { get; set; }
        public string PeerSgtCapable { get; set; }
        public string FlexibleStackingEnabled { get; set; }
        public string DaiTrusted { get; set; }
        public Profile3 Profile { get; set; }
    }

    public class Profile3
    {
        public string Enabled { get; set; }
        public string Id { get; set; }
        public string Iname { get; set; }
    }
}