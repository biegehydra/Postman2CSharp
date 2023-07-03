using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<OrMoreSensorRolesToAGivenSensorOrCameraDeviceRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class OrMoreSensorRolesToAGivenSensorOrCameraDeviceRequest
    {
        public Livestream Livestream { get; set; }
    }

    public class RelatedDevices2
    {
        public string Serial { get; set; }
    }
}