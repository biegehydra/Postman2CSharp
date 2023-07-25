using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<HeCurrentUplinkAddressesForDevicesInAnOrganizationResponse>>(myJsonResponse);
    public class HeCurrentUplinkAddressesForDevicesInAnOrganizationResponse
    {
        public string Mac { get; set; }
        public string Name { get; set; }
        public BypassActivationLockAttemptResponse Network { get; set; }
        public string ProductType { get; set; }
        public string Serial { get; set; }
        public List<string> Tags { get; set; }
        public List<Uplinks4> Uplinks { get; set; }
    }

    public class Addresses
    {
        public string Protocol { get; set; }
        public string AssignmentMode { get; set; }
        public string Address { get; set; }
        public string Gateway { get; set; }
        public AddAMediaServerToBeMonitoredForThisOrganizationRequest Public { get; set; }
    }

    public class Uplinks4
    {
        public string Interface { get; set; }
        public List<Addresses> Addresses { get; set; }
    }
}