using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<IentConnectionEventsOnThisNetworkInAGivenTimeRangeResponse>>(myJsonResponse);
    public class IentConnectionEventsOnThisNetworkInAGivenTimeRangeResponse
    {
        public int SsidNumber { get; set; }
        public int Vlan { get; set; }
        public string ClientMac { get; set; }
        public string Serial { get; set; }
        public string FailureStep { get; set; }
        public string Type { get; set; }
        public DateTime Ts { get; set; }
    }
}