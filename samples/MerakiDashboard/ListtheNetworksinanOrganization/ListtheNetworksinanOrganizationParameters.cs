using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<ListtheNetworksinanOrganizationParameters>(myJsonResponse);
namespace MerakiDashboard
{
    public class ListtheNetworksinanOrganizationParameters : IQueryParameters
    {
        /// <summary>
        /// An optional parameter that is the ID of a config template. Will return all networks bound to that template.
        /// </summary>
        public string ConfigTemplateId { get; set; }
        
        /// <summary>
        /// An optional parameter to filter networks by tags. The filtering is case-sensitive. If tags are included, 'tagsFilterType' should also be included (see below).
        /// </summary>
        public string Tags { get; set; }
        
        /// <summary>
        /// An optional parameter of value 'withAnyTags' or 'withAllTags' to indicate whether to return networks which contain ANY or ALL of the included tags. If no type is included, 'withAnyTags' will be selected.
        /// </summary>
        public string TagsFilterType { get; set; }
        
        /// <summary>
        /// The number of entries per page returned. Acceptable range is 3 - 100000. Default is 1000.
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

        public Dictionary<string, string?> ToDictionary()
        {
            return new Dictionary<string, string?>
            {
                {
                    "configTemplateId",
                    ConfigTemplateId
                },
                {
                    "tags",
                    Tags
                },
                {
                    "tagsFilterType",
                    TagsFilterType
                },
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