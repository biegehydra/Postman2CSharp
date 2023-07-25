using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheBluetoothSettingsForAWirelessDeviceRequest>(myJsonResponse);
    public class UpdateTheBluetoothSettingsForAWirelessDeviceRequest
    {
        public string Uuid { get; set; }
        public string Major { get; set; }
        public string Minor { get; set; }
    }
}