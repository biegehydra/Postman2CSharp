using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnTheRadioSettingsOfAnApplianceResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnTheRadioSettingsOfAnApplianceResponse
    {
        public string Serial { get; set; }
        public string RfProfileId { get; set; }
        public TwoFourGhzSettings TwoFourGhzSettings { get; set; }
        public FiveGhzSettings FiveGhzSettings { get; set; }
    }

    public class FiveGhzSettings
    {
        public int Channel { get; set; }
        public int ChannelWidth { get; set; }
        public int TargetPower { get; set; }
    }

    public class TwoFourGhzSettings
    {
        public int Channel { get; set; }
        public int TargetPower { get; set; }
    }
}