using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheSwitchPortsForASwitchResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ListTheSwitchPortsForASwitchResponse
    {
        public string PortId { get; set; }
        public string Name { get; set; }
        public List<string> Tags { get; set; }
        public bool Enabled { get; set; }
        public bool PoeEnabled { get; set; }
        public string Type { get; set; }
        public int Vlan { get; set; }
        public int VoiceVlan { get; set; }
        public string AllowedVlans { get; set; }
        public bool IsolationEnabled { get; set; }
        public bool RstpEnabled { get; set; }
        public string StpGuard { get; set; }
        public string LinkNegotiation { get; set; }
        public List<string> LinkNegotiationCapabilities { get; set; }
        public string PortScheduleId { get; set; }
        public string Udld { get; set; }
        public string AccessPolicyType { get; set; }
        public int AccessPolicyNumber { get; set; }
        public List<string> MacAllowList { get; set; }
        public List<string> StickyMacAllowList { get; set; }
        public int StickyMacAllowListLimit { get; set; }
        public bool StormControlEnabled { get; set; }
        public string AdaptivePolicyGroupId { get; set; }
        public bool PeerSgtCapable { get; set; }
        public bool FlexibleStackingEnabled { get; set; }
        public bool DaiTrusted { get; set; }
        public Profile Profile { get; set; }
    }

    public class Profile
    {
        public bool Enabled { get; set; }
        public string Id { get; set; }
        public string Iname { get; set; }
    }
}