using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ListRFProfilesForThisNetworkParameters>(myJsonResponse);
    public class ListRFProfilesForThisNetworkParameters : IQueryParameters
    {
        /// <summary>
        /// If the network is bound to a template, this parameter controls whether or not the non-basic RF 
        /// profiles defined on the template should be included in the response alongside the non-basic profiles 
        /// defined on the bound network. Defaults to false. 
        /// </summary>
        public string IncludeTemplateProfiles { get; set; }

        public Dictionary<string, string?> ToDictionary()
        {
            return new Dictionary<string, string?>
            {
                {
                    "includeTemplateProfiles",
                    IncludeTemplateProfiles
                }
            };
        }
    }
}