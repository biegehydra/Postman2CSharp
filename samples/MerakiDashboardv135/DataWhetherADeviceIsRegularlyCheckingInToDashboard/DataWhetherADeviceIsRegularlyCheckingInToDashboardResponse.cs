using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<DataWhetherADeviceIsRegularlyCheckingInToDashboardResponse>>(myJsonResponse);
    public class DataWhetherADeviceIsRegularlyCheckingInToDashboardResponse
    {
        public DateTime FirstSeenAt { get; set; }
        public DateTime LastSeenAt { get; set; }
    }
}