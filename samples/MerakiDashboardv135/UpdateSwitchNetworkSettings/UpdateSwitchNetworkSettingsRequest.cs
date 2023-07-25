using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<UpdateSwitchNetworkSettingsRequest>(myJsonResponse);
    public class UpdateSwitchNetworkSettingsRequest
    {
        public string Vlan { get; set; }
        public string UseCombinedPower { get; set; }
        public List<PowerExceptions> PowerExceptions { get; set; }
        public Dot11w UplinkClientSampling { get; set; }
    }
}