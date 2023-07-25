using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ReturnTheSSIDStatusesOfAnAccessPointResponse>(myJsonResponse);
    public class ReturnTheSSIDStatusesOfAnAccessPointResponse
    {
        public List<BasicServiceSets> BasicServiceSets { get; set; }
    }

    public class BasicServiceSets
    {
        public string SsidName { get; set; }
        public int SsidNumber { get; set; }
        public bool Enabled { get; set; }
        public string Band { get; set; }
        public string Bssid { get; set; }
        public int Channel { get; set; }
        public string ChannelWidth { get; set; }
        public string Power { get; set; }
        public bool Visible { get; set; }
        public bool Broadcasting { get; set; }
    }
}