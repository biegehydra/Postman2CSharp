using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateALayer3StaticRouteForASwitchResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateALayer3StaticRouteForASwitchResponse
    {
        public string StaticRouteId { get; set; }
        public string Name { get; set; }
        public string Subnet { get; set; }
        public string NextHopIp { get; set; }
        public bool AdvertiseViaOspfEnabled { get; set; }
        public bool PreferOverOspfRoutesEnabled { get; set; }
    }
}