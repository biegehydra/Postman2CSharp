using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListSwitchPortSchedulesResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ListSwitchPortSchedulesResponse
    {
        public string Id { get; set; }
        public string NetworkId { get; set; }
        public string Name { get; set; }
        public PortSchedule PortSchedule { get; set; }
    }

    public class PortSchedule
    {
        public Monday Monday { get; set; }
        public Tuesday Tuesday { get; set; }
        public Wednesday Wednesday { get; set; }
        public Thursday Thursday { get; set; }
        public Friday Friday { get; set; }
        public Saturday Saturday { get; set; }
        public Sunday Sunday { get; set; }
    }

    public class Friday
    {
        public bool Active { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }

    public class Monday
    {
        public bool Active { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }

    public class Saturday
    {
        public bool Active { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }

    public class Sunday
    {
        public bool Active { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }

    public class Thursday
    {
        public bool Active { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }

    public class Tuesday
    {
        public bool Active { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }

    public class Wednesday
    {
        public bool Active { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}