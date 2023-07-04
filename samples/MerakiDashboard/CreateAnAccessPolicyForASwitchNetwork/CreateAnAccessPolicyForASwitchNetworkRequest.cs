using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<CreateAnAccessPolicyForASwitchNetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class CreateAnAccessPolicyForASwitchNetworkRequest
    {
        public string Name { get; set; }
        public List<RadiusServers> RadiusServers { get; set; }
        public string RadiusTestingEnabled { get; set; }
        public string RadiusCoaSupportEnabled { get; set; }
        public string RadiusAccountingEnabled { get; set; }
        public string HostMode { get; set; }
        public string UrlRedirectWalledGardenEnabled { get; set; }
        public Radius2 Radius { get; set; }
        public string GuestPortBouncing { get; set; }
        public List<RadiusServers3> RadiusAccountingServers { get; set; }
        public string RadiusGroupAttribute { get; set; }
        public string AccessPolicyType { get; set; }
        public string IncreaseAccessSpeed { get; set; }
        public string GuestVlanId { get; set; }
        public Dot1x Dot1x { get; set; }
        public string VoiceVlanClients { get; set; }
        public List<string> UrlRedirectWalledGardenRanges { get; set; }
    }

    public class CriticalAuth2
    {
        public string DataVlanId { get; set; }
        public string VoiceVlanId { get; set; }
        public string SuspendPortBounce { get; set; }
    }

    public class Radius2
    {
        public CriticalAuth2 CriticalAuth { get; set; }
        public string FailedAuthVlanId { get; set; }
        public string ReAuthenticationInterval { get; set; }
    }
}