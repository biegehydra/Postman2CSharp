using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<FDevicesLicensesAndOrOrdersIntoAnOrganizationRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class FDevicesLicensesAndOrOrdersIntoAnOrganizationRequest
    {
        public List<string> Orders { get; set; }
        public List<string> Serials { get; set; }
        public List<RemainderLicenses> Licenses { get; set; }
    }
}