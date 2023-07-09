using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<OricalRecordsOfCommandsSentToSystemsManagerDevicesParameters>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class OricalRecordsOfCommandsSentToSystemsManagerDevicesParameters : IQueryParameters
    {
        /// <summary>
        /// The number of entries per page returned. Acceptable range is 3 - 1000. Default is 1000. 
        /// </summary>
        public string PerPage { get; set; }
        
        /// <summary>
        /// A token used by the server to indicate the start of the page. Often this is a timestamp or an ID but 
        /// it is not limited to those. This parameter should not be defined by client applications. The link 
        /// for the first, last, prev, or next page in the HTTP Link header should define it. 
        /// </summary>
        public string StartingAfter { get; set; }
        
        /// <summary>
        /// A token used by the server to indicate the end of the page. Often this is a timestamp or an ID but 
        /// it is not limited to those. This parameter should not be defined by client applications. The link 
        /// for the first, last, prev, or next page in the HTTP Link header should define it. 
        /// </summary>
        public string EndingBefore { get; set; }

        public Dictionary<string, string?> ToDictionary()
        {
            return new Dictionary<string, string?>
            {
                {
                    "perPage",
                    PerPage
                },
                {
                    "startingAfter",
                    StartingAfter
                },
                {
                    "endingBefore",
                    EndingBefore
                }
            };
        }
    }
}