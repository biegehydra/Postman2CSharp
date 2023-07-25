using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ErgyBLEBluetoothSettingsAMustBeEnabledOnTheNetworkResponse>(myJsonResponse);
    public class ErgyBLEBluetoothSettingsAMustBeEnabledOnTheNetworkResponse
    {
        public bool ScanningEnabled { get; set; }
        public bool AdvertisingEnabled { get; set; }
        public string Uuid { get; set; }
        public string MajorMinorAssignmentMode { get; set; }
        public int Major { get; set; }
        public int Minor { get; set; }
        public bool EslEnabled { get; set; }
    }
}