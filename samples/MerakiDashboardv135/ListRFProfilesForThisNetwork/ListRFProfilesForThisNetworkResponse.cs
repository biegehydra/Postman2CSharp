using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ListRFProfilesForThisNetworkResponse>(myJsonResponse);
    public class ListRFProfilesForThisNetworkResponse
    {
        public string Id { get; set; }
        public string NetworkId { get; set; }
        public string Name { get; set; }
        public bool ClientBalancingEnabled { get; set; }
        public string MinBitrateType { get; set; }
        public string BandSelectionType { get; set; }
        public _0 ApBandSettings { get; set; }
        public TwoFourGhzSettings TwoFourGhzSettings { get; set; }
        public FiveGhzSettings3 FiveGhzSettings { get; set; }
        public FiveGhzSettings SixGhzSettings { get; set; }
        public Authentication2 Transmission { get; set; }
        public PerSsidSettings2 PerSsidSettings { get; set; }
    }

    public class PerSsidSettings2
    {
        public _0 _0 { get; set; }
        public _0 _1 { get; set; }
        public _0 _2 { get; set; }
        public _0 _3 { get; set; }
        public _0 _4 { get; set; }
        public _0 _5 { get; set; }
        public _0 _6 { get; set; }
        public _0 _7 { get; set; }
        public _0 _8 { get; set; }
        public _0 _9 { get; set; }
        public _0 _10 { get; set; }
        public _0 _11 { get; set; }
        public _0 _12 { get; set; }
        public _0 _13 { get; set; }
        public _0 _14 { get; set; }
    }

    public class FiveGhzSettings3
    {
        public int MaxPower { get; set; }
        public int MinPower { get; set; }
        public int MinBitrate { get; set; }
        public List<int> ValidAutoChannels { get; set; }
        public string ChannelWidth { get; set; }
        public int Rxsop { get; set; }
    }

    public class TwoFourGhzSettings
    {
        public int MaxPower { get; set; }
        public int MinPower { get; set; }
        public int MinBitrate { get; set; }
        public List<int> ValidAutoChannels { get; set; }
        public bool AxEnabled { get; set; }
        public int Rxsop { get; set; }
    }

    public class _0
    {
        public string Name { get; set; }
        public int MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public bool BandSteeringEnabled { get; set; }
    }
}