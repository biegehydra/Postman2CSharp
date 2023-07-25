using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<ViewTheChangeLogForYourOrganizationResponse>>(myJsonResponse);
    public class ViewTheChangeLogForYourOrganizationResponse
    {
        public DateTime Ts { get; set; }
        public string AdminName { get; set; }
        public string AdminEmail { get; set; }
        public string AdminId { get; set; }
        public string Page { get; set; }
        public string Label { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}