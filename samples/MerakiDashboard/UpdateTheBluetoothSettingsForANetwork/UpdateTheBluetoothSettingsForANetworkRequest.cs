using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheBluetoothSettingsForANetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheBluetoothSettingsForANetworkRequest
    {
        public string ScanningEnabled { get; set; }
        public string AdvertisingEnabled { get; set; }
        public string Uuid { get; set; }
        public string MajorMinorAssignmentMode { get; set; }
        public string Major { get; set; }
        public string Minor { get; set; }
    }
}