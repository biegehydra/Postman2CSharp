using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheBluetoothSettingsForAWirelessDeviceResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class UpdateTheBluetoothSettingsForAWirelessDeviceResponse
    {
        public string Uuid { get; set; }
        public int Major { get; set; }
        public int Minor { get; set; }
    }
}