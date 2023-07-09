using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<RageChannelUtilizationForAllBandsSegmentedByDeviceParameters>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class RageChannelUtilizationForAllBandsSegmentedByDeviceParameters : IQueryParameters
    {
        /// <summary>
        /// Filter results by network. 
        /// </summary>
        public string NetworkIds { get; set; }
        
        /// <summary>
        /// Filter results by device. 
        /// </summary>
        public string Serials { get; set; }
        
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
        
        /// <summary>
        /// The beginning of the timespan for the data. The maximum lookback period is 31 days from today. 
        /// </summary>
        public string T0 { get; set; }
        
        /// <summary>
        /// The end of the timespan for the data. t1 can be a maximum of 31 days after t0. 
        /// </summary>
        public string T1 { get; set; }
        
        /// <summary>
        /// The timespan for which the information will be fetched. If specifying timespan, do not specify 
        /// parameters t0 and t1. The value must be in seconds and be less than or equal to 31 days. The default 
        /// is 7 days. 
        /// </summary>
        public string Timespan { get; set; }
        
        /// <summary>
        /// The time interval in seconds for returned data. The valid intervals are: 300, 600, 3600, 7200, 
        /// 14400, 21600. The default is 3600. 
        /// </summary>
        public string Interval { get; set; }

        public Dictionary<string, string?> ToDictionary()
        {
            return new Dictionary<string, string?>
            {
                {
                    "networkIds",
                    NetworkIds
                },
                {
                    "serials",
                    Serials
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
                    "t0",
                    T0
                },
                {
                    "t1",
                    T1
                },
                {
                    "timespan",
                    Timespan
                },
                {
                    "interval",
                    Interval
                }
            };
        }
    }
}