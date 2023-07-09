using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<DInAnSMNetworkWithVariousSpecifiedFieldsAndFiltersParameters>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class DInAnSMNetworkWithVariousSpecifiedFieldsAndFiltersParameters : IQueryParameters
    {
        /// <summary>
        /// Additional fields that will be displayed for each device.
        ///     The default fields are: id, name, 
        /// tags, ssid, wifiMac, osName, systemModel, uuid, and serialNumber. The additional fields are: ip,
        ///     
        /// systemType, availableDeviceCapacity, kioskAppName, biosVersion, lastConnected, missingAppsCount, 
        /// userSuppliedAddress, location, lastUser,
        ///     ownerEmail, ownerUsername, osBuild, publicIp, 
        /// phoneNumber, diskInfoJson, deviceCapacity, isManaged, hadMdm, isSupervised, meid, imei, iccid,
        ///     
        /// simCarrierNetwork, cellularDataUsed, isHotspotEnabled, createdAt, batteryEstCharge, quarantined, 
        /// avName, avRunning, asName, fwName,
        ///     isRooted, loginRequired, screenLockEnabled, screenLockDelay, 
        /// autoLoginDisabled, autoTags, hasMdm, hasDesktopAgent, diskEncryptionEnabled,
        ///     
        /// hardwareEncryptionCaps, passCodeLock, usesHardwareKeystore, androidSecurityPatchVersion, and url. 
        /// </summary>
        public string Fields { get; set; }
        
        /// <summary>
        /// Filter devices by wifi mac(s). 
        /// </summary>
        public string WifiMacs { get; set; }
        
        /// <summary>
        /// Filter devices by serial(s). 
        /// </summary>
        public string Serials { get; set; }
        
        /// <summary>
        /// Filter devices by id(s). 
        /// </summary>
        public string Ids { get; set; }
        
        /// <summary>
        /// Specify a scope (one of all, none, withAny, withAll, withoutAny, or withoutAll) and a set of tags. 
        /// </summary>
        public string Scope { get; set; }
        
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
                    "fields",
                    Fields
                },
                {
                    "wifiMacs",
                    WifiMacs
                },
                {
                    "serials",
                    Serials
                },
                {
                    "ids",
                    Ids
                },
                {
                    "scope",
                    Scope
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