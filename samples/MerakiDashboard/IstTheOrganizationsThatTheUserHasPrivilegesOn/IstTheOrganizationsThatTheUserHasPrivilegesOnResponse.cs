using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<IstTheOrganizationsThatTheUserHasPrivilegesOnResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class IstTheOrganizationsThatTheUserHasPrivilegesOnResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Api2 Api { get; set; }
        public Licensing Licensing { get; set; }
        public Cloud Cloud { get; set; }
        public Management Management { get; set; }
    }

    public class Api2
    {
        public bool Enabled { get; set; }
    }

    public class Cloud
    {
        public Region Region { get; set; }
    }

    public class Licensing
    {
        public string Model { get; set; }
    }

    public class Region
    {
        public string Name { get; set; }
    }
}