using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<ListLayer3StaticRoutesForASwitchResponse>>(myJsonResponse);
    public class ListLayer3StaticRoutesForASwitchResponse
    {
        public string StaticRouteId { get; set; }
        public string Name { get; set; }
        public string Subnet { get; set; }
        public string NextHopIp { get; set; }
        public bool AdvertiseViaOspfEnabled { get; set; }
        public bool PreferOverOspfRoutesEnabled { get; set; }
    }
}