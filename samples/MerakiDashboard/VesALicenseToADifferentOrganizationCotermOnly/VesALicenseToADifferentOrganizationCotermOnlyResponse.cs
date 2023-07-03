using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<VesALicenseToADifferentOrganizationCotermOnlyResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class VesALicenseToADifferentOrganizationCotermOnlyResponse
    {
        public List<RemainderLicenses> RemainderLicenses { get; set; }
        public List<MovedLicenses> MovedLicenses { get; set; }
    }

    public class MovedLicenses
    {
        public string Key { get; set; }
        public string OrganizationId { get; set; }
        public int Duration { get; set; }
        public string Mode { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime ClaimedAt { get; set; }
        public bool Invalidated { get; set; }
        public DateTime InvalidatedAt { get; set; }
        public bool Expired { get; set; }
        public List<Editions> Editions { get; set; }
        public List<Counts> Counts { get; set; }
    }

    public class RemainderLicenses
    {
        public string Key { get; set; }
        public string OrganizationId { get; set; }
        public int Duration { get; set; }
        public string Mode { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime ClaimedAt { get; set; }
        public bool Invalidated { get; set; }
        public DateTime InvalidatedAt { get; set; }
        public bool Expired { get; set; }
        public List<Editions> Editions { get; set; }
        public List<Counts> Counts { get; set; }
    }
}