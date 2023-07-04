using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<AddASwitchPortScheduleRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class AddASwitchPortScheduleRequest
    {
        public string Name { get; set; }
        public PortSchedule PortSchedule { get; set; }
    }
}