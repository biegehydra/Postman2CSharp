using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<AddDeleteOrUpdateTheTagsOfASetOfDevicesResponse>>(myJsonResponse);
    public class AddDeleteOrUpdateTheTagsOfASetOfDevicesResponse
    {
        public string Id { get; set; }
        public List<string> Tags { get; set; }
        public string WifiMac { get; set; }
        public string Serial { get; set; }
    }
}