using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ListTheClientsThatHaveUsedThisNetworkInTheTimespanParameters>(myJsonResponse);
    public class ListTheClientsThatHaveUsedThisNetworkInTheTimespanParameters : IQueryParameters
    {
        /// <summary>
        /// The beginning of the timespan for the data. The maximum lookback period is 31 days from today. 
        /// </summary>
        public string T0 { get; set; }
        
        /// <summary>
        /// The timespan for which the information will be fetched. If specifying timespan, do not specify 
        /// parameter t0. The value must be in seconds and be less than or equal to 31 days. The default is 1 
        /// day. 
        /// </summary>
        public string Timespan { get; set; }
        
        /// <summary>
        /// The number of entries per page returned. Acceptable range is 3 - 1000. Default is 10. 
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
        
        /// <summary>
        /// Filters clients based on status. Can be one of 'Online' or 'Offline'. 
        /// </summary>
        public string Statuses { get; set; }
        
        /// <summary>
        /// Filters clients based on a partial or full match for the ip address field. 
        /// </summary>
        public string Ip { get; set; }
        
        /// <summary>
        /// Filters clients based on a partial or full match for the ip6 address field. 
        /// </summary>
        public string Ip6 { get; set; }
        
        /// <summary>
        /// Filters clients based on a partial or full match for the ip6Local address field. 
        /// </summary>
        public string Ip6Local { get; set; }
        
        /// <summary>
        /// Filters clients based on a partial or full match for the mac address field. 
        /// </summary>
        public string Mac { get; set; }
        
        /// <summary>
        /// Filters clients based on a partial or full match for the os (operating system) field. 
        /// </summary>
        public string Os { get; set; }
        
        /// <summary>
        /// Filters clients based on partial or full match for the iPSK name field. 
        /// </summary>
        public string PskGroup { get; set; }
        
        /// <summary>
        /// Filters clients based on a partial or full match for the description field. 
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Filters clients based on the full match for the VLAN field. 
        /// </summary>
        public string Vlan { get; set; }
        
        /// <summary>
        /// Filters clients based on recent connection type. Can be one of 'Wired' or 'Wireless'. 
        /// </summary>
        public string RecentDeviceConnections { get; set; }

        public Dictionary<string, string?> ToDictionary()
        {
            return new Dictionary<string, string?>
            {
                {
                    "t0",
                    T0
                },
                {
                    "timespan",
                    Timespan
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
                },
                {
                    "statuses",
                    Statuses
                },
                {
                    "ip",
                    Ip
                },
                {
                    "ip6",
                    Ip6
                },
                {
                    "ip6Local",
                    Ip6Local
                },
                {
                    "mac",
                    Mac
                },
                {
                    "os",
                    Os
                },
                {
                    "pskGroup",
                    PskGroup
                },
                {
                    "description",
                    Description
                },
                {
                    "vlan",
                    Vlan
                },
                {
                    "recentDeviceConnections",
                    RecentDeviceConnections
                }
            };
        }
    }
}