using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<DInAnSMNetworkWithVariousSpecifiedFieldsAndFiltersResponse>>(myJsonResponse);
    public class DInAnSMNetworkWithVariousSpecifiedFieldsAndFiltersResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Tags { get; set; }
        public string Ssid { get; set; }
        public string WifiMac { get; set; }
        public string OsName { get; set; }
        public string SystemModel { get; set; }
        public string Uuid { get; set; }
        public string SerialNumber { get; set; }
        public string Serial { get; set; }
        public string Ip { get; set; }
        public string Notes { get; set; }
    }
}