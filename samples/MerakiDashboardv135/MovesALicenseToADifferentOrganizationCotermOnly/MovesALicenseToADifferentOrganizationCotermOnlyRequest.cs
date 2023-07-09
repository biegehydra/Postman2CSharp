using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<MovesALicenseToADifferentOrganizationCotermOnlyRequest>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class MovesALicenseToADifferentOrganizationCotermOnlyRequest
    {
        public Destination2 Destination { get; set; }
        public List<Licenses> Licenses { get; set; }
    }

    public class Counts4
    {
        public string Model { get; set; }
        public string Count { get; set; }
    }

    public class Destination2
    {
        public string OrganizationId { get; set; }
        public string Mode { get; set; }
    }

    public class Licenses
    {
        public string Key { get; set; }
        public List<Counts4> Counts { get; set; }
    }
}