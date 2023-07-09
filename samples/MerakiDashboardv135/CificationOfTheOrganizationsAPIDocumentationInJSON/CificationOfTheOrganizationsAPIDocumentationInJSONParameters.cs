using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<CificationOfTheOrganizationsAPIDocumentationInJSONParameters>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class CificationOfTheOrganizationsAPIDocumentationInJSONParameters : IQueryParameters
    {
        /// <summary>
        /// OpenAPI Specification version to return. Default is 2 
        /// </summary>
        public string Version { get; set; }

        public Dictionary<string, string?> ToDictionary()
        {
            return new Dictionary<string, string?>
            {
                {
                    "version",
                    Version
                }
            };
        }
    }
}