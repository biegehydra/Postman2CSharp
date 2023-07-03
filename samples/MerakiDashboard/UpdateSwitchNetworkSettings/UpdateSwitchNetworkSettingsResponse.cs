using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateSwitchNetworkSettingsResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateSwitchNetworkSettingsResponse
    {
        public int Vlan { get; set; }
        public bool UseCombinedPower { get; set; }
        public List<PowerExceptions> PowerExceptions { get; set; }
        public UplinkClientSampling UplinkClientSampling { get; set; }
    }
}