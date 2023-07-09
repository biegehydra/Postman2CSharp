using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<ListTheEventsForTheNetworkParameters>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ListTheEventsForTheNetworkParameters : IQueryParameters
    {
        /// <summary>
        /// The product type to fetch events for. This parameter is required for networks with multiple device 
        /// types. Valid types are wireless, appliance, switch, systemsManager, camera, cellularGateway, and 
        /// cloudGateway 
        /// </summary>
        public string ProductType { get; set; }
        
        /// <summary>
        /// A list of event types. The returned events will be filtered to only include events with these types. 
        /// </summary>
        public string IncludedEventTypes { get; set; }
        
        /// <summary>
        /// A list of event types. The returned events will be filtered to exclude events with these types. 
        /// </summary>
        public string ExcludedEventTypes { get; set; }
        
        /// <summary>
        /// The MAC address of the Meraki device which the list of events will be filtered with 
        /// </summary>
        public string DeviceMac { get; set; }
        
        /// <summary>
        /// The serial of the Meraki device which the list of events will be filtered with 
        /// </summary>
        public string DeviceSerial { get; set; }
        
        /// <summary>
        /// The name of the Meraki device which the list of events will be filtered with 
        /// </summary>
        public string DeviceName { get; set; }
        
        /// <summary>
        /// The IP of the client which the list of events will be filtered with. Only supported for track-by-IP 
        /// networks. 
        /// </summary>
        public string ClientIp { get; set; }
        
        /// <summary>
        /// The MAC address of the client which the list of events will be filtered with. Only supported for 
        /// track-by-MAC networks. 
        /// </summary>
        public string ClientMac { get; set; }
        
        /// <summary>
        /// The name, or partial name, of the client which the list of events will be filtered with 
        /// </summary>
        public string ClientName { get; set; }
        
        /// <summary>
        /// The MAC address of the Systems Manager device which the list of events will be filtered with 
        /// </summary>
        public string SmDeviceMac { get; set; }
        
        /// <summary>
        /// The name of the Systems Manager device which the list of events will be filtered with 
        /// </summary>
        public string SmDeviceName { get; set; }
        
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

        public Dictionary<string, string?> ToDictionary()
        {
            return new Dictionary<string, string?>
            {
                {
                    "productType",
                    ProductType
                },
                {
                    "",
                    IncludedEventTypes
                },
                {
                    "",
                    ExcludedEventTypes
                },
                {
                    "deviceMac",
                    DeviceMac
                },
                {
                    "deviceSerial",
                    DeviceSerial
                },
                {
                    "deviceName",
                    DeviceName
                },
                {
                    "clientIp",
                    ClientIp
                },
                {
                    "clientMac",
                    ClientMac
                },
                {
                    "clientName",
                    ClientName
                },
                {
                    "smDeviceMac",
                    SmDeviceMac
                },
                {
                    "smDeviceName",
                    SmDeviceName
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