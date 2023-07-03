using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<CreateALayer3StaticRouteForASwitchRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class CreateALayer3StaticRouteForASwitchRequest
    {
        public string Subnet { get; set; }
        public string NextHopIp { get; set; }
        public string Name { get; set; }
        public string AdvertiseViaOspfEnabled { get; set; }
        public string PreferOverOspfRoutesEnabled { get; set; }
    }
}