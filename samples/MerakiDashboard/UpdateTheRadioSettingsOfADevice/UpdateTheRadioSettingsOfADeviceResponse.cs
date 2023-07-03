using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheRadioSettingsOfADeviceResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheRadioSettingsOfADeviceResponse
    {
        public string Serial { get; set; }
        public string RfProfileId { get; set; }
        public TwoFourGhzSettings TwoFourGhzSettings { get; set; }
        public FiveGhzSettings FiveGhzSettings { get; set; }
    }
}