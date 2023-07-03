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

    public class Friday2
    {
        public string Active { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }

    public class Monday2
    {
        public string Active { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }

    public class Saturday2
    {
        public string Active { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }

    public class Sunday2
    {
        public string Active { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }

    public class Thursday2
    {
        public string Active { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }

    public class Tuesday2
    {
        public string Active { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }

    public class Wednesday2
    {
        public string Active { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}