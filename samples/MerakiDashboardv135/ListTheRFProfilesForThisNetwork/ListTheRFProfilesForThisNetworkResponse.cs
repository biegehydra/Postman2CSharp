using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ListTheRFProfilesForThisNetworkResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ListTheRFProfilesForThisNetworkResponse
    {
        public List<Assigned> Assigned { get; set; }
    }

    public class Assigned
    {
        public string Id { get; set; }
        public string NetworkId { get; set; }
        public string Name { get; set; }
        public TwoFourGhzSettings2 TwoFourGhzSettings { get; set; }
        public FiveGhzSettings2 FiveGhzSettings { get; set; }
        public PerSsidSettings PerSsidSettings { get; set; }
    }

    public class PerSsidSettings
    {
        [JsonPropertyName("1")]
        public _1 _1 { get; set; }

        [JsonPropertyName("2")]
        public _2 _2 { get; set; }

        [JsonPropertyName("3")]
        public _3 _3 { get; set; }

        [JsonPropertyName("4")]
        public _4 _4 { get; set; }
    }

    public class FiveGhzSettings2
    {
        public int MinBitrate { get; set; }
        public bool AxEnabled { get; set; }
    }

    public class TwoFourGhzSettings2
    {
        public int MinBitrate { get; set; }
        public bool AxEnabled { get; set; }
    }

    public class _1
    {
        public string BandOperationMode { get; set; }
        public bool BandSteeringEnabled { get; set; }
    }

    public class _2
    {
        public string BandOperationMode { get; set; }
        public bool BandSteeringEnabled { get; set; }
    }

    public class _3
    {
        public string BandOperationMode { get; set; }
        public bool BandSteeringEnabled { get; set; }
    }

    public class _4
    {
        public string BandOperationMode { get; set; }
        public bool BandSteeringEnabled { get; set; }
    }
}