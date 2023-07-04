using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheAccessPoliciesForASwitchNetworkResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ListTheAccessPoliciesForASwitchNetworkResponse
    {
        public string Name { get; set; }
        public List<RadiusServers> RadiusServers { get; set; }
        public Radius Radius { get; set; }
        public bool GuestPortBouncing { get; set; }
        public bool RadiusTestingEnabled { get; set; }
        public bool RadiusCoaSupportEnabled { get; set; }
        public bool RadiusAccountingEnabled { get; set; }
        public List<RadiusServers> RadiusAccountingServers { get; set; }
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

    public class CriticalAuth
    {
        public int DataVlanId { get; set; }
        public int VoiceVlanId { get; set; }
        public bool SuspendPortBounce { get; set; }
    }

    public class Radius
    {
        public CriticalAuth CriticalAuth { get; set; }
        public int FailedAuthVlanId { get; set; }
        public int ReAuthenticationInterval { get; set; }
    }

    public class Dot1x
    {
        public string ControlDirection { get; set; }
    }
}