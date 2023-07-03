using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheRadioSettingsOfAnApplianceRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheRadioSettingsOfAnApplianceRequest
    {
        public string RfProfileId { get; set; }
        public TwoFourGhzSettings2 TwoFourGhzSettings { get; set; }
        public FiveGhzSettings2 FiveGhzSettings { get; set; }
    }

    public class FiveGhzSettings2
    {
        public string Channel { get; set; }
        public string ChannelWidth { get; set; }
        public string TargetPower { get; set; }
    }

    public class TwoFourGhzSettings2
    {
        public string Channel { get; set; }
        public string TargetPower { get; set; }
    }
}