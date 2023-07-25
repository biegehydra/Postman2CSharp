using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<REachUplinkOfAllMXAndZNetworksWithinAnOrganizationResponse>>(myJsonResponse);
    public class REachUplinkOfAllMXAndZNetworksWithinAnOrganizationResponse
    {
        public string NetworkId { get; set; }
        public string Name { get; set; }
        public List<ByUplink> ByUplink { get; set; }
    }

    public class ByUplink
    {
        public string Serial { get; set; }
        public string Interface { get; set; }
        public int Sent { get; set; }
        public int Received { get; set; }
    }
}