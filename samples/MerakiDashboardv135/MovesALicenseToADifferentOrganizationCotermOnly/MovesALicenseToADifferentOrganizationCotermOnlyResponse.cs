using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<MovesALicenseToADifferentOrganizationCotermOnlyResponse>(myJsonResponse);
    public class MovesALicenseToADifferentOrganizationCotermOnlyResponse
    {
        public List<ListTheLicensesInACotermOrganizationResponse> RemainderLicenses { get; set; }
        public List<ListTheLicensesInACotermOrganizationResponse> MovedLicenses { get; set; }
    }
}