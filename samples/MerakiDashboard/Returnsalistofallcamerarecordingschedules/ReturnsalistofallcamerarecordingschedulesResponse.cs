using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ReturnsAListOfAllCameraRecordingSchedulesResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnsAListOfAllCameraRecordingSchedulesResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}