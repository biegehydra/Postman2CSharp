using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListLayer3StaticRoutesForASwitchResponse>>(myJsonResponse);
namespace MerakiDashboard
{
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