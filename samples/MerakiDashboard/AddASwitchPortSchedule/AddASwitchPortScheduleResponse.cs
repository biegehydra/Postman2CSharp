using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<AddASwitchPortScheduleResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class AddASwitchPortScheduleResponse
    {
        public string Id { get; set; }
        public string NetworkId { get; set; }
        public string Name { get; set; }
        public PortSchedule PortSchedule { get; set; }
    }
}