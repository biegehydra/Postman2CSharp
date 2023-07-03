using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnTheBluetoothSettingsForAWirelessDeviceResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnTheBluetoothSettingsForAWirelessDeviceResponse
    {
        public string Uuid { get; set; }
        public int Major { get; set; }
        public int Minor { get; set; }
    }
}