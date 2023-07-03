using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ListRFProfilesForThisNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ListRFProfilesForThisNetworkResponse
    {
        public string Id { get; set; }
        public string NetworkId { get; set; }
        public string Name { get; set; }
        public bool ClientBalancingEnabled { get; set; }
        public string MinBitrateType { get; set; }
        public string BandSelectionType { get; set; }
        public ApBandSettings ApBandSettings { get; set; }
        public TwoFourGhzSettings8 TwoFourGhzSettings { get; set; }
        public FiveGhzSettings8 FiveGhzSettings { get; set; }
        public SixGhzSettings SixGhzSettings { get; set; }
        public Transmission Transmission { get; set; }
        public PerSsidSettings2 PerSsidSettings { get; set; }
    }

    public class PerSsidSettings2
    {
        [JsonPropertyName("0")]
        public _0 _0 { get; set; }

        [JsonPropertyName("1")]
        public _1 _1 { get; set; }

        [JsonPropertyName("2")]
        public _2 _2 { get; set; }

        [JsonPropertyName("3")]
        public _3 _3 { get; set; }

        [JsonPropertyName("4")]
        public _4 _4 { get; set; }

        [JsonPropertyName("5")]
        public _5 _5 { get; set; }

        [JsonPropertyName("6")]
        public _6 _6 { get; set; }

        [JsonPropertyName("7")]
        public _7 _7 { get; set; }

        [JsonPropertyName("8")]
        public _8 _8 { get; set; }

        [JsonPropertyName("9")]
        public _9 _9 { get; set; }

        [JsonPropertyName("10")]
        public _10 _10 { get; set; }

        [JsonPropertyName("11")]
        public _11 _11 { get; set; }

        [JsonPropertyName("12")]
        public _12 _12 { get; set; }

        [JsonPropertyName("13")]
        public _13 _13 { get; set; }

        [JsonPropertyName("14")]
        public _14 _14 { get; set; }
    }

    public class FiveGhzSettings8
    {
        public int MaxPower { get; set; }
        public int MinPower { get; set; }
        public int MinBitrate { get; set; }
        public List<int> ValidAutoChannels { get; set; }
        public string ChannelWidth { get; set; }
        public int Rxsop { get; set; }
    }

    public class SixGhzSettings
    {
        public int MaxPower { get; set; }
        public int MinPower { get; set; }
        public int MinBitrate { get; set; }
        public List<int> ValidAutoChannels { get; set; }
        public string ChannelWidth { get; set; }
        public int Rxsop { get; set; }
    }

    public class TwoFourGhzSettings8
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

    public class _10
    {
        public string Name { get; set; }
        public int MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public bool BandSteeringEnabled { get; set; }
    }

    public class _11
    {
        public string Name { get; set; }
        public int MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public bool BandSteeringEnabled { get; set; }
    }

    public class _12
    {
        public string Name { get; set; }
        public int MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public bool BandSteeringEnabled { get; set; }
    }

    public class _122
    {
        public string Name { get; set; }
        public int MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public bool BandSteeringEnabled { get; set; }
    }

    public class _13
    {
        public string Name { get; set; }
        public int MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public bool BandSteeringEnabled { get; set; }
    }

    public class _14
    {
        public string Name { get; set; }
        public int MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public bool BandSteeringEnabled { get; set; }
    }

    public class _22
    {
        public string Name { get; set; }
        public int MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public bool BandSteeringEnabled { get; set; }
    }

    public class _32
    {
        public string Name { get; set; }
        public int MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public bool BandSteeringEnabled { get; set; }
    }

    public class _42
    {
        public string Name { get; set; }
        public int MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public bool BandSteeringEnabled { get; set; }
    }

    public class _5
    {
        public string Name { get; set; }
        public int MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public bool BandSteeringEnabled { get; set; }
    }

    public class _6
    {
        public string Name { get; set; }
        public int MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public bool BandSteeringEnabled { get; set; }
    }

    public class _7
    {
        public string Name { get; set; }
        public int MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public bool BandSteeringEnabled { get; set; }
    }

    public class _8
    {
        public string Name { get; set; }
        public int MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public bool BandSteeringEnabled { get; set; }
    }

    public class _9
    {
        public string Name { get; set; }
        public int MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public bool BandSteeringEnabled { get; set; }
    }

    public class ApBandSettings
    {
        public string BandOperationMode { get; set; }
        public bool BandSteeringEnabled { get; set; }
    }

    public class Transmission
    {
        public bool Enabled { get; set; }
    }
}