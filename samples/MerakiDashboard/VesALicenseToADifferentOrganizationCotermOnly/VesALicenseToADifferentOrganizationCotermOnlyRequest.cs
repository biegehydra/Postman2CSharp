using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<VesALicenseToADifferentOrganizationCotermOnlyRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class VesALicenseToADifferentOrganizationCotermOnlyRequest
    {
        public Destination2 Destination { get; set; }
        public List<Licenses> Licenses { get; set; }
    }

    public class Counts5
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
        public List<Counts5> Counts { get; set; }
    }
}