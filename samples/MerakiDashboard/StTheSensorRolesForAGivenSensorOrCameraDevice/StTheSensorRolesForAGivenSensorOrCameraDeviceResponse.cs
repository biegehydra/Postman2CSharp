using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<StTheSensorRolesForAGivenSensorOrCameraDeviceResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class StTheSensorRolesForAGivenSensorOrCameraDeviceResponse
    {
        public Livestream Livestream { get; set; }
    }

    public class RelatedDevices
    {
        public string Serial { get; set; }
        public string ProductType { get; set; }
    }

    public class Livestream
    {
        public List<RelatedDevices> RelatedDevices { get; set; }
    }
}