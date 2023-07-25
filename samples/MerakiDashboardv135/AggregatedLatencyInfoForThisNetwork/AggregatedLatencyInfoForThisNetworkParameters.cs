using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<AggregatedLatencyInfoForThisNetworkParameters>(myJsonResponse);
    public class AggregatedLatencyInfoForThisNetworkParameters : IQueryParameters
    {
        /// <summary>
        /// The beginning of the timespan for the data. The maximum lookback period is 180 days from today. 
        /// </summary>
        public string T0 { get; set; }
        
        /// <summary>
        /// The end of the timespan for the data. t1 can be a maximum of 7 days after t0. 
        /// </summary>
        public string T1 { get; set; }
        
        /// <summary>
        /// The timespan for which the information will be fetched. If specifying timespan, do not specify 
        /// parameters t0 and t1. The value must be in seconds and be less than or equal to 7 days. 
        /// </summary>
        public string Timespan { get; set; }
        
        /// <summary>
        /// Filter results by band (either '2.4', '5' or '6'). Note that data prior to February 2020 will not 
        /// have band information. 
        /// </summary>
        public string Band { get; set; }
        
        /// <summary>
        /// Filter results by SSID 
        /// </summary>
        public string Ssid { get; set; }
        
        /// <summary>
        /// Filter results by VLAN 
        /// </summary>
        public string Vlan { get; set; }
        
        /// <summary>
        /// Filter results by AP Tag 
        /// </summary>
        public string ApTag { get; set; }
        
        /// <summary>
        /// Partial selection: If present, this call will return only the selected fields of ["rawDistribution", 
        /// "avg"]. All fields will be returned by default. Selected fields must be entered as a comma separated 
        /// string. 
        /// </summary>
        public string Fields { get; set; }

        public Dictionary<string, string?> ToDictionary()
        {
            return new Dictionary<string, string?>
            {
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
                    "band",
                    Band
                },
                {
                    "ssid",
                    Ssid
                },
                {
                    "vlan",
                    Vlan
                },
                {
                    "apTag",
                    ApTag
                },
                {
                    "fields",
                    Fields
                }
            };
        }
    }
}