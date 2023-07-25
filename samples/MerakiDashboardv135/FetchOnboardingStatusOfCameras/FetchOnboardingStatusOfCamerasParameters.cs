using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<FetchOnboardingStatusOfCamerasParameters>(myJsonResponse);
    public class FetchOnboardingStatusOfCamerasParameters : IQueryParameters
    {
        /// <summary>
        /// A list of serial numbers. The returned cameras will be filtered to only include these serials. 
        /// </summary>
        public string Serials { get; set; }
        
        /// <summary>
        /// A list of network IDs. The returned cameras will be filtered to only include these networks. 
        /// </summary>
        public string NetworkIds { get; set; }

        public Dictionary<string, string?> ToDictionary()
        {
            return new Dictionary<string, string?>
            {
                {
                    "serials",
                    Serials
                },
                {
                    "networkIds",
                    NetworkIds
                }
            };
        }
    }
}