using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnsTheSwitchNetworkSettingsResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnsTheSwitchNetworkSettingsResponse
    {
        public int Vlan { get; set; }
        public bool UseCombinedPower { get; set; }
        public List<PowerExceptions> PowerExceptions { get; set; }
        public Wan2 UplinkClientSampling { get; set; }
    }

    public class PowerExceptions
    {
        public string Serial { get; set; }
        public string PowerType { get; set; }
    }
}