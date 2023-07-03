using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<OrMoreSensorRolesToAGivenSensorOrCameraDeviceResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class OrMoreSensorRolesToAGivenSensorOrCameraDeviceResponse
    {
        public Livestream Livestream { get; set; }
    }
}