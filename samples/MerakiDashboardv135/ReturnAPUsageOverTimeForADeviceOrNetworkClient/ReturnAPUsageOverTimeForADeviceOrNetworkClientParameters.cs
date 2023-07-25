using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ReturnAPUsageOverTimeForADeviceOrNetworkClientParameters>(myJsonResponse);
    public class ReturnAPUsageOverTimeForADeviceOrNetworkClientParameters : IQueryParameters
    {
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
        /// The time resolution in seconds for returned data. The valid resolutions are: 300, 600, 1200, 3600, 
        /// 14400, 86400. The default is 86400. 
        /// </summary>
        public string Resolution { get; set; }
        
        /// <summary>
        /// Automatically select a data resolution based on the given timespan; this overrides the value 
        /// specified by the 'resolution' parameter. The default setting is false. 
        /// </summary>
        public string AutoResolution { get; set; }
        
        /// <summary>
        /// Filter results by network client to return per-device AP usage over time inner joined by the queried 
        /// client's connection history. 
        /// </summary>
        public string ClientId { get; set; }
        
        /// <summary>
        /// Filter results by device. Requires :band. 
        /// </summary>
        public string DeviceSerial { get; set; }
        
        /// <summary>
        /// Filter results by AP tag; either :clientId or :deviceSerial must be jointly specified. 
        /// </summary>
        public string ApTag { get; set; }
        
        /// <summary>
        /// Filter results by band (either '2.4', '5' or '6'). 
        /// </summary>
        public string Band { get; set; }
        
        /// <summary>
        /// Filter results by SSID number. 
        /// </summary>
        public string Ssid { get; set; }

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
                    "resolution",
                    Resolution
                },
                {
                    "autoResolution",
                    AutoResolution
                },
                {
                    "clientId",
                    ClientId
                },
                {
                    "deviceSerial",
                    DeviceSerial
                },
                {
                    "apTag",
                    ApTag
                },
                {
                    "band",
                    Band
                },
                {
                    "ssid",
                    Ssid
                }
            };
        }
    }
}