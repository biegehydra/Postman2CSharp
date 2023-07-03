using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<StatusesInformationForDevicesInAnOrganizationParameters>(myJsonResponse);
namespace MerakiDashboard
{
    public class StatusesInformationForDevicesInAnOrganizationParameters : IQueryParameters
    {
        /// <summary>
        /// The number of entries per page returned. Acceptable range is 3 - 1000. Default is 1000.
        /// </summary>
        public string PerPage { get; set; }
        
        /// <summary>
        /// A token used by the server to indicate the start of the page. Often this is a timestamp or an ID but it is not limited to those. This parameter should not be defined by client applications. The link for the first, last, prev, or next page in the HTTP Link header should define it.
        /// </summary>
        public string StartingAfter { get; set; }
        
        /// <summary>
        /// A token used by the server to indicate the end of the page. Often this is a timestamp or an ID but it is not limited to those. This parameter should not be defined by client applications. The link for the first, last, prev, or next page in the HTTP Link header should define it.
        /// </summary>
        public string EndingBefore { get; set; }
        
        /// <summary>
        /// Optional parameter to filter device by network ID. This filter uses multiple exact matches.
        /// </summary>
        public string NetworkIds { get; set; }
        
        /// <summary>
        /// Optional parameter to filter device by device product types. This filter uses multiple exact matches.
        /// </summary>
        public string ProductTypes { get; set; }
        
        /// <summary>
        /// Optional parameter to filter device by device serial numbers. This filter uses multiple exact matches.
        /// </summary>
        public string Serials { get; set; }
        
        /// <summary>
        /// An optional parameter to filter devices by the provisioning status. Accepted statuses: unprovisioned, incomplete, complete.
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// An optional parameter to filter devices by tags. The filtering is case-sensitive. If tags are included, 'tagsFilterType' should also be included (see below). This filter uses multiple exact matches.
        /// </summary>
        public string Tags { get; set; }
        
        /// <summary>
        /// An optional parameter of value 'withAnyTags' or 'withAllTags' to indicate whether to return devices which contain ANY or ALL of the included tags. If no type is included, 'withAnyTags' will be selected.
        /// </summary>
        public string TagsFilterType { get; set; }

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
                },
                {
                    "networkIds",
                    NetworkIds
                },
                {
                    "productTypes",
                    ProductTypes
                },
                {
                    "serials",
                    Serials
                },
                {
                    "status",
                    Status
                },
                {
                    "tags",
                    Tags
                },
                {
                    "tagsFilterType",
                    TagsFilterType
                }
            };
        }
    }
}