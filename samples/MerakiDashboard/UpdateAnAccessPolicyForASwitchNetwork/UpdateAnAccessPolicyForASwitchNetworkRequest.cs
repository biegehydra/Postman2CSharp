using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateAnAccessPolicyForASwitchNetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateAnAccessPolicyForASwitchNetworkRequest
    {
        public string Name { get; set; }
        public List<RadiusServers> RadiusServers { get; set; }
        public Radius Radius { get; set; }
        public string GuestPortBouncing { get; set; }
        public string RadiusTestingEnabled { get; set; }
        public string RadiusCoaSupportEnabled { get; set; }
        public string RadiusAccountingEnabled { get; set; }
        public List<RadiusAccountingServers> RadiusAccountingServers { get; set; }
        public string RadiusGroupAttribute { get; set; }
        public string HostMode { get; set; }
        public string AccessPolicyType { get; set; }
        public string IncreaseAccessSpeed { get; set; }
        public string GuestVlanId { get; set; }
        public Dot1x Dot1x { get; set; }
        public string VoiceVlanClients { get; set; }
        public string UrlRedirectWalledGardenEnabled { get; set; }
        public List<string> UrlRedirectWalledGardenRanges { get; set; }
    }
}