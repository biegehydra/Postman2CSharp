using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateAnAccessPolicyForASwitchNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateAnAccessPolicyForASwitchNetworkResponse
    {
        public string Name { get; set; }
        public List<RadiusServers> RadiusServers { get; set; }
        public Radius Radius { get; set; }
        public bool GuestPortBouncing { get; set; }
        public bool RadiusTestingEnabled { get; set; }
        public bool RadiusCoaSupportEnabled { get; set; }
        public bool RadiusAccountingEnabled { get; set; }
        public List<RadiusAccountingServers> RadiusAccountingServers { get; set; }
        public string RadiusGroupAttribute { get; set; }
        public string HostMode { get; set; }
        public string AccessPolicyType { get; set; }
        public bool IncreaseAccessSpeed { get; set; }
        public int GuestVlanId { get; set; }
        public Dot1x Dot1x { get; set; }
        public bool VoiceVlanClients { get; set; }
        public bool UrlRedirectWalledGardenEnabled { get; set; }
        public List<string> UrlRedirectWalledGardenRanges { get; set; }
    }
}