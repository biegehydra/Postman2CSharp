using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<CreatesNewRFProfileForThisNetwork2Request>(myJsonResponse);
namespace MerakiDashboard
{
    public class CreatesNewRFProfileForThisNetwork2Request
    {
        public string Name { get; set; }
        public string BandSelectionType { get; set; }
        public string ClientBalancingEnabled { get; set; }
        public string MinBitrateType { get; set; }
        public ApBandSettings2 ApBandSettings { get; set; }
        public TwoFourGhzSettings13 TwoFourGhzSettings { get; set; }
        public FiveGhzSettings13 FiveGhzSettings { get; set; }
        public SixGhzSettings2 SixGhzSettings { get; set; }
        public Transmission2 Transmission { get; set; }
        public PerSsidSettings PerSsidSettings { get; set; }
    }

    public class FiveGhzSettings13
    {
        public string MaxPower { get; set; }
        public string MinPower { get; set; }
        public string MinBitrate { get; set; }
        public List<string> ValidAutoChannels { get; set; }
        public string ChannelWidth { get; set; }
        public string Rxsop { get; set; }
    }

    public class SixGhzSettings2
    {
        public string MaxPower { get; set; }
        public string MinPower { get; set; }
        public string MinBitrate { get; set; }
        public List<string> ValidAutoChannels { get; set; }
        public string ChannelWidth { get; set; }
        public string Rxsop { get; set; }
    }

    public class TwoFourGhzSettings13
    {
        public string MaxPower { get; set; }
        public string MinPower { get; set; }
        public string MinBitrate { get; set; }
        public List<string> ValidAutoChannels { get; set; }
        public string AxEnabled { get; set; }
        public string Rxsop { get; set; }
    }

    public class _02
    {
        public string MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public string BandSteeringEnabled { get; set; }
    }

    public class _102
    {
        public string MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public string BandSteeringEnabled { get; set; }
    }

    public class _112
    {
        public string MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public string BandSteeringEnabled { get; set; }
    }

    public class _123
    {
        public string MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public string BandSteeringEnabled { get; set; }
    }

    public class _132
    {
        public string MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public string BandSteeringEnabled { get; set; }
    }

    public class _142
    {
        public string MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public string BandSteeringEnabled { get; set; }
    }

    public class _19
    {
        public string MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public string BandSteeringEnabled { get; set; }
    }

    public class _27
    {
        public string MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public string BandSteeringEnabled { get; set; }
    }

    public class _37
    {
        public string MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public string BandSteeringEnabled { get; set; }
    }

    public class _47
    {
        public string MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public string BandSteeringEnabled { get; set; }
    }

    public class _52
    {
        public string MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public string BandSteeringEnabled { get; set; }
    }

    public class _62
    {
        public string MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public string BandSteeringEnabled { get; set; }
    }

    public class _72
    {
        public string MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public string BandSteeringEnabled { get; set; }
    }

    public class _82
    {
        public string MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public string BandSteeringEnabled { get; set; }
    }

    public class _92
    {
        public string MinBitrate { get; set; }
        public string BandOperationMode { get; set; }
        public string BandSteeringEnabled { get; set; }
    }

    public class ApBandSettings2
    {
        public string BandOperationMode { get; set; }
        public string BandSteeringEnabled { get; set; }
    }

    public class Transmission2
    {
        public string Enabled { get; set; }
    }
}