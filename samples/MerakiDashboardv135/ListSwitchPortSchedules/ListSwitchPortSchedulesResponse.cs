using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<ListSwitchPortSchedulesResponse>>(myJsonResponse);
    public class ListSwitchPortSchedulesResponse
    {
        public string Id { get; set; }
        public string NetworkId { get; set; }
        public string Name { get; set; }
        public PortSchedule PortSchedule { get; set; }
    }

    public class PortSchedule
    {
        public Friday Monday { get; set; }
        public Friday Tuesday { get; set; }
        public Friday Wednesday { get; set; }
        public Friday Thursday { get; set; }
        public Friday Friday { get; set; }
        public Friday Saturday { get; set; }
        public Friday Sunday { get; set; }
    }

    public class Friday
    {
        public bool Active { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}