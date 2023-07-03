using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<CreatesNewRFProfileForThisNetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class CreatesNewRFProfileForThisNetworkRequest
    {
        public string Name { get; set; }
        public TwoFourGhzSettings9 TwoFourGhzSettings { get; set; }
        public FiveGhzSettings9 FiveGhzSettings { get; set; }
        public PerSsidSettings PerSsidSettings { get; set; }
    }

    public class FiveGhzSettings9
    {
        public string MinBitrate { get; set; }
        public string AxEnabled { get; set; }
    }

    public class TwoFourGhzSettings9
    {
        public string MinBitrate { get; set; }
        public string AxEnabled { get; set; }
    }

    public class _15
    {
        public string BandOperationMode { get; set; }
        public string BandSteeringEnabled { get; set; }
    }

    public class _23
    {
        public string BandOperationMode { get; set; }
        public string BandSteeringEnabled { get; set; }
    }

    public class _33
    {
        public string BandOperationMode { get; set; }
        public string BandSteeringEnabled { get; set; }
    }

    public class _43
    {
        public string BandOperationMode { get; set; }
        public string BandSteeringEnabled { get; set; }
    }
}