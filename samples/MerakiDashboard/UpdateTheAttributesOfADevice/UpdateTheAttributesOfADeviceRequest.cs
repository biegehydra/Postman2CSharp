using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheAttributesOfADeviceRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheAttributesOfADeviceRequest
    {
        public string Name { get; set; }
        public List<string> Tags { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public string MoveMapMarker { get; set; }
        public string SwitchProfileId { get; set; }
        public string FloorPlanId { get; set; }
    }
}