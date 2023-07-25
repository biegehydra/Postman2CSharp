using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ReturnAnOverviewOfTheLicenseStateForAnOrganizationResponse>(myJsonResponse);
    public class ReturnAnOverviewOfTheLicenseStateForAnOrganizationResponse
    {
        public string Status { get; set; }
        public string ExpirationDate { get; set; }
        public LicensedDeviceCounts LicensedDeviceCounts { get; set; }
    }

    public class LicensedDeviceCounts
    {
        public int MS { get; set; }
    }
}