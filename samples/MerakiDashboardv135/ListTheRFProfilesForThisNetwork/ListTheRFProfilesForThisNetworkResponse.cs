using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ListTheRFProfilesForThisNetworkResponse>(myJsonResponse);
    public class ListTheRFProfilesForThisNetworkResponse
    {
        public List<Assigned> Assigned { get; set; }
    }

    public class Assigned
    {
        public string Id { get; set; }
        public string NetworkId { get; set; }
        public string Name { get; set; }
        public FiveGhzSettings TwoFourGhzSettings { get; set; }
        public FiveGhzSettings2 FiveGhzSettings { get; set; }
        public PerSsidSettings PerSsidSettings { get; set; }
    }

    public class PerSsidSettings
    {
        public _1 _1 { get; set; }
        public _1 _2 { get; set; }
        public _1 _3 { get; set; }
        public _1 _4 { get; set; }
    }

    public class FiveGhzSettings2
    {
        public int MinBitrate { get; set; }
        public bool AxEnabled { get; set; }
    }

    public class _1
    {
        public string BandOperationMode { get; set; }
        public bool BandSteeringEnabled { get; set; }
    }
}