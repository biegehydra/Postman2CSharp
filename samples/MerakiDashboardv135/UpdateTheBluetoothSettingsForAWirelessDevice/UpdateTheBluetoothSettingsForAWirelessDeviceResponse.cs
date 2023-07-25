using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheBluetoothSettingsForAWirelessDeviceResponse>(myJsonResponse);
    public class UpdateTheBluetoothSettingsForAWirelessDeviceResponse
    {
        public string Uuid { get; set; }
        public int Major { get; set; }
        public int Minor { get; set; }
    }
}