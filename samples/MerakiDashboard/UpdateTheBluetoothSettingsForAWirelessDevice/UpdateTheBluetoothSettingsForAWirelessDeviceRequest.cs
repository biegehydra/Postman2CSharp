using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheBluetoothSettingsForAWirelessDeviceRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheBluetoothSettingsForAWirelessDeviceRequest
    {
        public string Uuid { get; set; }
        public string Major { get; set; }
        public string Minor { get; set; }
    }
}