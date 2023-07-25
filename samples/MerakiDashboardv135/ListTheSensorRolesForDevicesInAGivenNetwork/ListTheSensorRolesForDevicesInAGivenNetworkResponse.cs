using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheSensorRolesForDevicesInAGivenNetworkResponse>>(myJsonResponse);
    public class ListTheSensorRolesForDevicesInAGivenNetworkResponse
    {
        public Device Device { get; set; }
        public ListTheSensorRolesForAGivenSensorOrCameraDeviceResponse Relationships { get; set; }
    }

    public class Device
    {
        public string Name { get; set; }
        public string Serial { get; set; }
        public string ProductType { get; set; }
    }
}