using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<CificationOfTheOrganizationsAPIDocumentationInJSONResponse>(myJsonResponse);
    public class CificationOfTheOrganizationsAPIDocumentationInJSONResponse
    {
        public string Openapi { get; set; }
        public Info Info { get; set; }
        public Paths Paths { get; set; }
    }

    public class Get
    {
        public string Description { get; set; }
        public string OperationId { get; set; }
        public Responses Responses { get; set; }
    }

    public class Info
    {
        public string Version { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class _200
    {
        public string Description { get; set; }
        public Examples Examples { get; set; }
    }

    public class Examples
    {
        public List<ListSwitchPortSchedulesResponse> ApplicationJson { get; set; }
    }

    public class Organizations
    {
        public Get Get { get; set; }
    }

    public class Paths
    {
        public Organizations Organizations { get; set; }
    }

    public class Responses
    {
        public _200 _200 { get; set; }
    }
}