using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<DataWhetherADeviceIsRegularlyCheckingInToDashboardResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class DataWhetherADeviceIsRegularlyCheckingInToDashboardResponse
    {
        public DateTime FirstSeenAt { get; set; }
        public DateTime LastSeenAt { get; set; }
    }
}