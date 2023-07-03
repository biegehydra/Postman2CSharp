using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheRadioSettingsOfADeviceRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheRadioSettingsOfADeviceRequest
    {
        public string RfProfileId { get; set; }
        public TwoFourGhzSettings TwoFourGhzSettings { get; set; }
        public FiveGhzSettings FiveGhzSettings { get; set; }
    }
}