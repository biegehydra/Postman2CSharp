using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateASwitchPortScheduleResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateASwitchPortScheduleResponse
    {
        public string Id { get; set; }
        public string NetworkId { get; set; }
        public string Name { get; set; }
        public PortSchedule PortSchedule { get; set; }
    }
}