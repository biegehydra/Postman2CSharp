using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ListOfDevicesLicensesAndOrOrdersIntoAnOrganizationRequest>(myJsonResponse);
    public class ListOfDevicesLicensesAndOrOrdersIntoAnOrganizationRequest
    {
        public List<string> Orders { get; set; }
        public List<string> Serials { get; set; }
        public List<Licenses2> Licenses { get; set; }
    }

    public class Licenses2
    {
        public string Key { get; set; }
        public string Mode { get; set; }
    }
}