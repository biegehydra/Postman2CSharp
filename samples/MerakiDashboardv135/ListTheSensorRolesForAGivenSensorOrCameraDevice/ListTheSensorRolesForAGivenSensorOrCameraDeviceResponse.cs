using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheSensorRolesForAGivenSensorOrCameraDeviceResponse>>(myJsonResponse);
    public class ListTheSensorRolesForAGivenSensorOrCameraDeviceResponse
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