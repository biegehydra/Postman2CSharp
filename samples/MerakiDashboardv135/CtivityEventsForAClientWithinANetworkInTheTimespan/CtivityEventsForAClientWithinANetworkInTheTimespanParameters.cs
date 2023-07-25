using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<CtivityEventsForAClientWithinANetworkInTheTimespanParameters>(myJsonResponse);
    public class CtivityEventsForAClientWithinANetworkInTheTimespanParameters : IQueryParameters
    {
        /// <summary>
        /// The number of entries per page returned. Acceptable range is 3 - 1000. 
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
        /// is 1 day. 
        /// </summary>
        public string Timespan { get; set; }
        
        /// <summary>
        /// A list of event types to include. If not specified, events of all types will be returned. Valid 
        /// types are 'assoc', 'disassoc', 'auth', 'deauth', 'dns', 'dhcp', 'roam', 'connection' and/or 
        /// 'sticky'. 
        /// </summary>
        public string Types { get; set; }
        
        /// <summary>
        /// A list of severities to include. If not specified, events of all severities will be returned. Valid 
        /// severities are 'good', 'info', 'warn' and/or 'bad'. 
        /// </summary>
        public string IncludedSeverities { get; set; }
        
        /// <summary>
        /// Filter results by band (either '2.4', '5', '6'). 
        /// </summary>
        public string Band { get; set; }
        
        /// <summary>
        /// An SSID number to include. If not specified, events for all SSIDs will be returned. 
        /// </summary>
        public string SsidNumber { get; set; }
        
        /// <summary>
        /// Filter results by an AP's serial number. 
        /// </summary>
        public string DeviceSerial { get; set; }

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
                    "types",
                    Types
                },
                {
                    "includedSeverities",
                    IncludedSeverities
                },
                {
                    "band",
                    Band
                },
                {
                    "ssidNumber",
                    SsidNumber
                },
                {
                    "deviceSerial",
                    DeviceSerial
                }
            };
        }
    }
}