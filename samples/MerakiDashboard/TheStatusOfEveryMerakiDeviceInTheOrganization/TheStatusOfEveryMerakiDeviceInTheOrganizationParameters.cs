using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<TheStatusOfEveryMerakiDeviceInTheOrganizationParameters>(myJsonResponse);
namespace MerakiDashboard
{
    public class TheStatusOfEveryMerakiDeviceInTheOrganizationParameters : IQueryParameters
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
        /// Optional parameter to filter devices by network ids.
        /// </summary>
        public string NetworkIds { get; set; }
        
        /// <summary>
        /// Optional parameter to filter devices by serials.
        /// </summary>
        public string Serials { get; set; }
        
        /// <summary>
        /// Optional parameter to filter devices by statuses. Valid statuses are ["online", "alerting", "offline", "dormant"].
        /// </summary>
        public string Statuses { get; set; }
        
        /// <summary>
        /// An optional parameter to filter device statuses by product type. Valid types are wireless, appliance, switch, systemsManager, camera, cellularGateway, sensor, and cloudGateway.
        /// </summary>
        public string ProductTypes { get; set; }
        
        /// <summary>
        /// Optional parameter to filter devices by models.
        /// </summary>
        public string Models { get; set; }
        
        /// <summary>
        /// An optional parameter to filter devices by tags. The filtering is case-sensitive. If tags are included, 'tagsFilterType' should also be included (see below).
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
                    "serials",
                    Serials
                },
                {
                    "statuses",
                    Statuses
                },
                {
                    "productTypes",
                    ProductTypes
                },
                {
                    "models",
                    Models
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