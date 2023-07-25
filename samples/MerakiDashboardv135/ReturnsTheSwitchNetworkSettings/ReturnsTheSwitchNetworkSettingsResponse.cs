using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ReturnsTheSwitchNetworkSettingsResponse>(myJsonResponse);
    public class ReturnsTheSwitchNetworkSettingsResponse
    {
        public int Vlan { get; set; }
        public bool UseCombinedPower { get; set; }
        public List<PowerExceptions> PowerExceptions { get; set; }
        public Authentication2 UplinkClientSampling { get; set; }
    }

    public class PowerExceptions
    {
        public string Serial { get; set; }
        public string PowerType { get; set; }
    }
}