using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheSensorRolesForAGivenSensorOrCameraDeviceResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
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