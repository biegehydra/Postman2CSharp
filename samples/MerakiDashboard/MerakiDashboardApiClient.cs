using System.Net.Http.Headers;
using System.Text;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
namespace MerakiDashboard
{
    public class MerakiDashboardApiClient : IMerakiDashboardApiClient
    {
        private readonly HttpClient _httpClient;
        private string _baseUrl = "https://api.meraki.com/api/v1";
        private readonly string _apiKey;
        public MerakiDashboardApiClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri($"https://{_baseUrl}/");
    
            _apiKey = config["Path:ToApiKey"];
    
            _httpClient.DefaultRequestHeaders.Add($"X-Cisco-Meraki-API-Key", $"");
        }
    
        /// <summary>
        /// List the organizations that the user has privileges on
        /// </summary>
        public async Task<ListtheOrganizationsResponse> ListtheOrganizations(CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ListtheOrganizationsResponse>($"organizations", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the networks that the user has privileges on in an organization
        /// </summary>
        /// <param name="organizationId">(Required) </param>
        public async Task<ListtheNetworksinanOrganizationResponse> ListtheNetworksinanOrganization(ListtheNetworksinanOrganizationParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/networks", parametersDict);
            return await _httpClient.GetFromJsonAsync<ListtheNetworksinanOrganizationResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the devices in an organization
        /// </summary>
        /// <param name="organizationId">(Required) </param>
        public async Task<ListtheDevicesinanOrganizationResponse> ListtheDevicesinanOrganization(ListtheDevicesinanOrganizationParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/devices", parametersDict);
            return await _httpClient.GetFromJsonAsync<ListtheDevicesinanOrganizationResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the clients that have used this network in the timespan
        /// </summary>
        /// <param name="networkId">(Required) </param>
        public async Task<ListtheClientsinaNetworkResponse> ListtheClientsinaNetwork(ListtheClientsinaNetworkParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/clients", parametersDict);
            return await _httpClient.GetFromJsonAsync<ListtheClientsinaNetworkResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Returns the identity of the current user.
        /// </summary>
        public async Task<ReturnstheidentityofthecurrentuserResponse> Returnstheidentityofthecurrentuser(CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnstheidentityofthecurrentuserResponse>($"administered/identities/me", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the DHCP subnet information for an appliance
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<ReturntheDHCPsubnetinformationforanapplianceResponse> ReturntheDHCPsubnetinformationforanappliance(string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturntheDHCPsubnetinformationforanapplianceResponse>($"devices/{serial}/appliance/dhcp/subnets", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the performance score for a single MX. Only primary MX devices supported. If no data is available, a 204 error code is returned.
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<ReturntheperformancescoreforasingleMXResponse> ReturntheperformancescoreforasingleMX(string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturntheperformancescoreforasingleMXResponse>($"devices/{serial}/appliance/performance", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return current delegated IPv6 prefixes on an appliance.
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<TurnCurrentDelegatedIPv6PrefixesOnAnApplianceResponse> TurnCurrentDelegatedIPv6PrefixesOnAnAppliance(string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<TurnCurrentDelegatedIPv6PrefixesOnAnApplianceResponse>($"devices/{serial}/appliance/prefixes/delegated", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the security events for a client. Clients can be identified by a client key or either the MAC or IP depending on whether the network uses Track-by-IP.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="clientId">(Required) Client ID</param>
        public async Task<ListthesecurityeventsforaclientResponse> Listthesecurityeventsforaclient(ListthesecurityeventsforaclientParameters queryParameters, string networkId, string clientId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/appliance/clients/{clientId}/security/events", parametersDict);
            return await _httpClient.GetFromJsonAsync<ListthesecurityeventsforaclientResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the security events for a network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ListTheSecurityEventsForANetworkResponse> ListTheSecurityEventsForANetwork(ListTheSecurityEventsForANetworkParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/appliance/security/events", parametersDict);
            return await _httpClient.GetFromJsonAsync<ListTheSecurityEventsForANetworkResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the security events for an organization
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<ListTheSecurityEventsForAnOrganizationResponse> ListTheSecurityEventsForAnOrganization(ListTheSecurityEventsForAnOrganizationParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/appliance/security/events", parametersDict);
            return await _httpClient.GetFromJsonAsync<ListTheSecurityEventsForAnOrganizationResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Get the sent and received bytes for each uplink of a network.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<HesentandreceivedbytesforeachuplinkofanetworkResponse> Hesentandreceivedbytesforeachuplinkofanetwork(HesentandreceivedbytesforeachuplinkofanetworkParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/appliance/uplinks/usageHistory", parametersDict);
            return await _httpClient.GetFromJsonAsync<HesentandreceivedbytesforeachuplinkofanetworkResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the uplink status of every Meraki MX and Z series appliances in the organization
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<MerakiMXandZseriesappliancesintheorganizationResponse> MerakiMXandZseriesappliancesintheorganization(MerakiMXandZseriesappliancesintheorganizationParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/appliance/uplink/statuses", parametersDict);
            return await _httpClient.GetFromJsonAsync<MerakiMXandZseriesappliancesintheorganizationResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Show VPN history stat for networks in an organization
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<ShowVPNhistorystatfornetworksinanorganizationResponse> ShowVPNhistorystatfornetworksinanorganization(ShowVPNhistorystatfornetworksinanorganizationParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/appliance/vpn/stats", parametersDict);
            return await _httpClient.GetFromJsonAsync<ShowVPNhistorystatfornetworksinanorganizationResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Show VPN status for networks in an organization
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<ShowVPNstatusfornetworksinanorganizationResponse> ShowVPNstatusfornetworksinanorganization(ShowVPNstatusfornetworksinanorganizationParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/appliance/vpn/statuses", parametersDict);
            return await _httpClient.GetFromJsonAsync<ShowVPNstatusfornetworksinanorganizationResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the radio settings of an appliance
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<ReturnTheRadioSettingsOfAnApplianceResponse> ReturnTheRadioSettingsOfAnAppliance(string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnTheRadioSettingsOfAnApplianceResponse>($"devices/{serial}/appliance/radio/settings", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the radio settings of an appliance
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// rfProfileId| string| The ID of an RF profile to assign to the device. If the value of this parameter is null, the appropriate basic RF profile (indoor or outdoor) will be assigned to the device. Assigning an RF profile will clear ALL manually configured overrides on the device (channel width, channel, power).
        /// twoFourGhzSettings| object| Manual radio settings for 2.4 GHz.
        /// fiveGhzSettings| object| Manual radio settings for 5 GHz.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/devices/{serial}/appliance/radio/settings` | update
        /// 
        /// 
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<UpdateTheRadioSettingsOfAnApplianceResponse> UpdateTheRadioSettingsOfAnAppliance(UpdateTheRadioSettingsOfAnApplianceRequest request, string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateTheRadioSettingsOfAnApplianceResponse>($"devices/{serial}/appliance/radio/settings", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the uplink settings for an MX appliance
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<ReturnTheUplinkSettingsForAnMXApplianceResponse> ReturnTheUplinkSettingsForAnMXAppliance(string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnTheUplinkSettingsForAnMXApplianceResponse>($"devices/{serial}/appliance/uplinks/settings", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the uplink settings for an MX appliance
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// interfaces| object| Interface settings.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/devices/{serial}/appliance/uplinks/settings` | update
        /// 
        /// 
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<UpdateTheUplinkSettingsForAnMXApplianceResponse> UpdateTheUplinkSettingsForAnMXAppliance(UpdateTheUplinkSettingsForAnMXApplianceRequest request, string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateTheUplinkSettingsForAnMXApplianceResponse>($"devices/{serial}/appliance/uplinks/settings", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Generate a new vMX authentication token
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<Stream> GenerateanewvMXauthenticationtoken(string serial, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsync($"devices/{serial}/appliance/vmx/authenticationToken", cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Return the connectivity testing destinations for an MX network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ConnectivityTestingDestinationsForAnMXNetworkResponse> ConnectivityTestingDestinationsForAnMXNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ConnectivityTestingDestinationsForAnMXNetworkResponse>($"networks/{networkId}/appliance/connectivityMonitoringDestinations", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the connectivity testing destinations for an MX network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// destinations| array| The list of connectivity monitoring destinations
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/appliance/connectivityMonitoringDestinations` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ConnectivityTestingDestinationsForAnMXNetwork2Response> ConnectivityTestingDestinationsForAnMXNetwork2(ConnectivityTestingDestinationsForAnMXNetwork2Request request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<ConnectivityTestingDestinationsForAnMXNetwork2Response>($"networks/{networkId}/appliance/connectivityMonitoringDestinations", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the content filtering settings for an MX network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<TurnTheContentFilteringSettingsForAnMXNetworkResponse> TurnTheContentFilteringSettingsForAnMXNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<TurnTheContentFilteringSettingsForAnMXNetworkResponse>($"networks/{networkId}/appliance/contentFiltering", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the content filtering settings for an MX network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// allowedUrlPatterns| array| A list of URL patterns that are allowed
        /// blockedUrlPatterns| array| A list of URL patterns that are blocked
        /// blockedUrlCategories| array| A list of URL categories to block
        /// urlCategoryListSize| string| URL category list size which is either 'topSites' or 'fullList'
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<DateTheContentFilteringSettingsForAnMXNetworkResponse> DateTheContentFilteringSettingsForAnMXNetwork(DateTheContentFilteringSettingsForAnMXNetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<DateTheContentFilteringSettingsForAnMXNetworkResponse>($"networks/{networkId}/appliance/contentFiltering", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the cellular firewall rules for an MX network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ReturnTheCellularFirewallRulesForAnMXNetworkResponse> ReturnTheCellularFirewallRulesForAnMXNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnTheCellularFirewallRulesForAnMXNetworkResponse>($"networks/{networkId}/appliance/firewall/cellularFirewallRules", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the cellular firewall rules of an MX network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// rules| array| An ordered array of the firewall rules (not including the default rule)
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<UpdateTheCellularFirewallRulesOfAnMXNetworkResponse> UpdateTheCellularFirewallRulesOfAnMXNetwork(UpdateTheCellularFirewallRulesOfAnMXNetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateTheCellularFirewallRulesOfAnMXNetworkResponse>($"networks/{networkId}/appliance/firewall/cellularFirewallRules", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the appliance services and their accessibility rules
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<HeApplianceServicesAndTheirAccessibilityRulesResponse> HeApplianceServicesAndTheirAccessibilityRules(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<HeApplianceServicesAndTheirAccessibilityRulesResponse>($"networks/{networkId}/appliance/firewall/firewalledServices", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the accessibility settings of the given service ('ICMP', 'web', or 'SNMP')
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="service">(Required) Service</param>
        public async Task<IbilitySettingsOfTheGivenServiceICMPWebOrSNMPResponse> IbilitySettingsOfTheGivenServiceICMPWebOrSNMP(string networkId, string service, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<IbilitySettingsOfTheGivenServiceICMPWebOrSNMPResponse>($"networks/{networkId}/appliance/firewall/firewalledServices/{service}", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Updates the accessibility settings for the given service ('ICMP', 'web', or 'SNMP')
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// access| string| A string indicating the rule for which IPs are allowed to use the specified service. Acceptable values are "blocked" (no remote IPs can access the service), "restricted" (only allowed IPs can access the service), and "unrestriced" (any remote IP can access the service). This field is required
        /// allowedIps| array| An array of allowed IPs that can access the service. This field is required if "access" is set to "restricted". Otherwise this field is ignored
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="service">(Required) Service</param>
        public async Task<BilitySettingsForTheGivenServiceICMPWebOrSNMPResponse> BilitySettingsForTheGivenServiceICMPWebOrSNMP(BilitySettingsForTheGivenServiceICMPWebOrSNMPRequest request, string networkId, string service, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<BilitySettingsForTheGivenServiceICMPWebOrSNMPResponse>($"networks/{networkId}/appliance/firewall/firewalledServices/{service}", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the inbound cellular firewall rules for an MX network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<TheInboundCellularFirewallRulesForAnMXNetworkResponse> TheInboundCellularFirewallRulesForAnMXNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<TheInboundCellularFirewallRulesForAnMXNetworkResponse>($"networks/{networkId}/appliance/firewall/inboundCellularFirewallRules", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the inbound cellular firewall rules of an MX network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// rules| array| An ordered array of the firewall rules (not including the default rule)
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ETheInboundCellularFirewallRulesOfAnMXNetworkResponse> ETheInboundCellularFirewallRulesOfAnMXNetwork(ETheInboundCellularFirewallRulesOfAnMXNetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<ETheInboundCellularFirewallRulesOfAnMXNetworkResponse>($"networks/{networkId}/appliance/firewall/inboundCellularFirewallRules", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the inbound firewall rules for an MX network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ReturnTheInboundFirewallRulesForAnMXNetworkResponse> ReturnTheInboundFirewallRulesForAnMXNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnTheInboundFirewallRulesForAnMXNetworkResponse>($"networks/{networkId}/appliance/firewall/inboundFirewallRules", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the inbound firewall rules of an MX network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// rules| array| An ordered array of the firewall rules (not including the default rule)
        /// syslogDefaultRule| boolean| Log the special default rule (boolean value - enable only if you've configured a syslog server) (optional)
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<UpdateTheInboundFirewallRulesOfAnMXNetworkResponse> UpdateTheInboundFirewallRulesOfAnMXNetwork(UpdateTheInboundFirewallRulesOfAnMXNetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateTheInboundFirewallRulesOfAnMXNetworkResponse>($"networks/{networkId}/appliance/firewall/inboundFirewallRules", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the L3 firewall rules for an MX network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ReturnTheL3FirewallRulesForAnMXNetworkResponse> ReturnTheL3FirewallRulesForAnMXNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnTheL3FirewallRulesForAnMXNetworkResponse>($"networks/{networkId}/appliance/firewall/l3FirewallRules", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the L3 firewall rules of an MX network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// rules| array| An ordered array of the firewall rules (not including the default rule)
        /// syslogDefaultRule| boolean| Log the special default rule (boolean value - enable only if you've configured a syslog server) (optional)
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<UpdateTheL3FirewallRulesOfAnMXNetworkResponse> UpdateTheL3FirewallRulesOfAnMXNetwork(UpdateTheL3FirewallRulesOfAnMXNetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateTheL3FirewallRulesOfAnMXNetworkResponse>($"networks/{networkId}/appliance/firewall/l3FirewallRules", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the MX L7 firewall rules for an MX network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ListTheMXL7FirewallRulesForAnMXNetworkResponse> ListTheMXL7FirewallRulesForAnMXNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ListTheMXL7FirewallRulesForAnMXNetworkResponse>($"networks/{networkId}/appliance/firewall/l7FirewallRules", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the MX L7 firewall rules for an MX network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// rules| array| An ordered array of the MX L7 firewall rules
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/appliance/firewall/l7FirewallRules` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<UpdateTheMXL7FirewallRulesForAnMXNetworkResponse> UpdateTheMXL7FirewallRulesForAnMXNetwork(UpdateTheMXL7FirewallRulesForAnMXNetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateTheMXL7FirewallRulesForAnMXNetworkResponse>($"networks/{networkId}/appliance/firewall/l7FirewallRules", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the 1:Many NAT mapping rules for an MX network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ReturnThe1ManyNATMappingRulesForAnMXNetworkResponse> ReturnThe1ManyNATMappingRulesForAnMXNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnThe1ManyNATMappingRulesForAnMXNetworkResponse>($"networks/{networkId}/appliance/firewall/oneToManyNatRules", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Set the 1:Many NAT mapping rules for an MX network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// rules| array| An array of 1:Many nat rules
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<SetThe1ManyNATMappingRulesForAnMXNetworkResponse> SetThe1ManyNATMappingRulesForAnMXNetwork(SetThe1ManyNATMappingRulesForAnMXNetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<SetThe1ManyNATMappingRulesForAnMXNetworkResponse>($"networks/{networkId}/appliance/firewall/oneToManyNatRules", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the 1:1 NAT mapping rules for an MX network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ReturnThe11NATMappingRulesForAnMXNetworkResponse> ReturnThe11NATMappingRulesForAnMXNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnThe11NATMappingRulesForAnMXNetworkResponse>($"networks/{networkId}/appliance/firewall/oneToOneNatRules", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Set the 1:1 NAT mapping rules for an MX network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// rules| array| An array of 1:1 nat rules
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<SetThe11NATMappingRulesForAnMXNetworkResponse> SetThe11NATMappingRulesForAnMXNetwork(SetThe11NATMappingRulesForAnMXNetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<SetThe11NATMappingRulesForAnMXNetworkResponse>($"networks/{networkId}/appliance/firewall/oneToOneNatRules", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the port forwarding rules for an MX network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ReturnThePortForwardingRulesForAnMXNetworkResponse> ReturnThePortForwardingRulesForAnMXNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnThePortForwardingRulesForAnMXNetworkResponse>($"networks/{networkId}/appliance/firewall/portForwardingRules", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the port forwarding rules for an MX network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// rules| array| An array of port forwarding params
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<UpdateThePortForwardingRulesForAnMXNetworkResponse> UpdateThePortForwardingRulesForAnMXNetwork(UpdateThePortForwardingRulesForAnMXNetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateThePortForwardingRulesForAnMXNetworkResponse>($"networks/{networkId}/appliance/firewall/portForwardingRules", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the firewall settings for this network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ReturnTheFirewallSettingsForThisNetworkResponse> ReturnTheFirewallSettingsForThisNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnTheFirewallSettingsForThisNetworkResponse>($"networks/{networkId}/appliance/firewall/settings", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the firewall settings for this network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// spoofingProtection| object| Spoofing protection settings
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<UpdateTheFirewallSettingsForThisNetworkResponse> UpdateTheFirewallSettingsForThisNetwork(UpdateTheFirewallSettingsForThisNetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateTheFirewallSettingsForThisNetworkResponse>($"networks/{networkId}/appliance/firewall/settings", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List per-port VLAN settings for all ports of a MX.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ListPerPortVLANSettingsForAllPortsOfAMXResponse> ListPerPortVLANSettingsForAllPortsOfAMX(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ListPerPortVLANSettingsForAllPortsOfAMXResponse>($"networks/{networkId}/appliance/ports", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return per-port VLAN settings for a single MX port.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="portId">(Required) Port ID</param>
        public async Task<ReturnPerPortVLANSettingsForASingleMXPortResponse> ReturnPerPortVLANSettingsForASingleMXPort(string networkId, string portId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnPerPortVLANSettingsForASingleMXPortResponse>($"networks/{networkId}/appliance/ports/{portId}", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the per-port VLAN settings for a single MX port.
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// enabled| boolean| The status of the port
        /// dropUntaggedTraffic| boolean| Trunk port can Drop all Untagged traffic. When true, no VLAN is required. Access ports cannot have dropUntaggedTraffic set to true.
        /// type| string| The type of the port: 'access' or 'trunk'.
        /// vlan| integer| Native VLAN when the port is in Trunk mode. Access VLAN when the port is in Access mode.
        /// allowedVlans| string| Comma-delimited list of the VLAN ID's allowed on the port, or 'all' to permit all VLAN's on the port.
        /// accessPolicy| string| The name of the policy. Only applicable to Access ports. Valid values are: 'open', '8021x-radius', 'mac-radius', 'hybris-radius' for MX64 or Z3 or any MX supporting the per port authentication feature. Otherwise, 'open' is the only valid value and 'open' is the default value if the field is missing.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/appliance/ports/{portId}` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="portId">(Required) Port ID</param>
        public async Task<UpdateThePerPortVLANSettingsForASingleMXPortResponse> UpdateThePerPortVLANSettingsForASingleMXPort(UpdateThePerPortVLANSettingsForASingleMXPortRequest request, string networkId, string portId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateThePerPortVLANSettingsForASingleMXPortResponse>($"networks/{networkId}/appliance/ports/{portId}", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List static delegated prefixes for a network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ListStaticDelegatedPrefixesForANetworkResponse> ListStaticDelegatedPrefixesForANetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ListStaticDelegatedPrefixesForANetworkResponse>($"networks/{networkId}/appliance/prefixes/delegated/statics", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Add a static delegated prefix from a network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// prefix| string| A static IPv6 prefix
        /// origin| object| The origin of the prefix
        /// description| string| A name or description for the prefix
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/appliance/prefixes/delegated/statics` | create
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<Stream> AddAStaticDelegatedPrefixFromANetwork(AddAStaticDelegatedPrefixFromANetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"networks/{networkId}/appliance/prefixes/delegated/statics", request, cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Return a static delegated prefix from a network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="staticDelegatedPrefixId">(Required) Static delegated prefix ID</param>
        public async Task<ReturnAStaticDelegatedPrefixFromANetworkResponse> ReturnAStaticDelegatedPrefixFromANetwork(string networkId, string staticDelegatedPrefixId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnAStaticDelegatedPrefixFromANetworkResponse>($"networks/{networkId}/appliance/prefixes/delegated/statics/{staticDelegatedPrefixId}", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update a static delegated prefix from a network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// prefix| string| A static IPv6 prefix
        /// origin| object| The origin of the prefix
        /// description| string| A name or description for the prefix
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/appliance/prefixes/delegated/statics/{staticDelegatedPrefixId}` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="staticDelegatedPrefixId">(Required) Static delegated prefix ID</param>
        public async Task<UpdateAStaticDelegatedPrefixFromANetworkResponse> UpdateAStaticDelegatedPrefixFromANetwork(UpdateAStaticDelegatedPrefixFromANetworkRequest request, string networkId, string staticDelegatedPrefixId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateAStaticDelegatedPrefixFromANetworkResponse>($"networks/{networkId}/appliance/prefixes/delegated/statics/{staticDelegatedPrefixId}", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Delete a static delegated prefix from a network
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/appliance/prefixes/delegated/statics/{staticDelegatedPrefixId}` | destroy
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="staticDelegatedPrefixId">(Required) Static delegated prefix ID</param>
        public async Task<Stream> DeleteAStaticDelegatedPrefixFromANetwork(string networkId, string staticDelegatedPrefixId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"networks/{networkId}/appliance/prefixes/delegated/statics/{staticDelegatedPrefixId}", cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// List the RF profiles for this network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ListtheRFprofilesforthisnetworkResponse> ListtheRFprofilesforthisnetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ListtheRFprofilesforthisnetworkResponse>($"networks/{networkId}/appliance/rfProfiles", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Returns all supported intrusion settings for an MX network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<NsAllSupportedIntrusionSettingsForAnMXNetworkResponse> NsAllSupportedIntrusionSettingsForAnMXNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<NsAllSupportedIntrusionSettingsForAnMXNetworkResponse>($"networks/{networkId}/appliance/security/intrusion", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Set the supported intrusion settings for an MX network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// mode| string| Set mode to 'disabled'/'detection'/'prevention' (optional - omitting will leave current config unchanged)
        /// idsRulesets| string| Set the detection ruleset 'connectivity'/'balanced'/'security' (optional - omitting will leave current config unchanged). Default value is 'balanced' if none currently saved
        /// protectedNetworks| object| Set the included/excluded networks from the intrusion engine (optional - omitting will leave current config unchanged). This is available only in 'passthrough' mode
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<EtTheSupportedIntrusionSettingsForAnMXNetworkResponse> EtTheSupportedIntrusionSettingsForAnMXNetwork(EtTheSupportedIntrusionSettingsForAnMXNetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<EtTheSupportedIntrusionSettingsForAnMXNetworkResponse>($"networks/{networkId}/appliance/security/intrusion", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Returns all supported intrusion settings for an organization
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<LlSupportedIntrusionSettingsForAnOrganizationResponse> LlSupportedIntrusionSettingsForAnOrganization(string organizationId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<LlSupportedIntrusionSettingsForAnOrganizationResponse>($"organizations/{organizationId}/appliance/security/intrusion", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Sets supported intrusion settings for an organization
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// allowedRules| array| Sets a list of specific SNORT signatures to allow
        /// 
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<TsSupportedIntrusionSettingsForAnOrganizationResponse> TsSupportedIntrusionSettingsForAnOrganization(TsSupportedIntrusionSettingsForAnOrganizationRequest request, string organizationId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<TsSupportedIntrusionSettingsForAnOrganizationResponse>($"organizations/{organizationId}/appliance/security/intrusion", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Returns all supported malware settings for an MX network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<UrnsAllSupportedMalwareSettingsForAnMXNetworkResponse> UrnsAllSupportedMalwareSettingsForAnMXNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<UrnsAllSupportedMalwareSettingsForAnMXNetworkResponse>($"networks/{networkId}/appliance/security/malware", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Set the supported malware settings for an MX network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// mode| string| Set mode to 'enabled' to enable malware prevention, otherwise 'disabled'
        /// allowedUrls| array| The urls that should be permitted by the malware detection engine. If omitted, the current config will remain unchanged. This is available only if your network supports AMP allow listing
        /// allowedFiles| array| The sha256 digests of files that should be permitted by the malware detection engine. If omitted, the current config will remain unchanged. This is available only if your network supports AMP allow listing
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<SetTheSupportedMalwareSettingsForAnMXNetworkResponse> SetTheSupportedMalwareSettingsForAnMXNetwork(SetTheSupportedMalwareSettingsForAnMXNetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<SetTheSupportedMalwareSettingsForAnMXNetworkResponse>($"networks/{networkId}/appliance/security/malware", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the appliance settings for a network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ReturnTheApplianceSettingsForANetworkResponse> ReturnTheApplianceSettingsForANetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnTheApplianceSettingsForANetworkResponse>($"networks/{networkId}/appliance/settings", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the appliance settings for a network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// clientTrackingMethod| string| Client tracking method of a network
        /// deploymentMode| string| Deployment mode of a network
        /// dynamicDns| object| Dynamic DNS settings for a network
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/appliance/settings` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<UpdateTheApplianceSettingsForANetworkResponse> UpdateTheApplianceSettingsForANetwork(UpdateTheApplianceSettingsForANetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateTheApplianceSettingsForANetworkResponse>($"networks/{networkId}/appliance/settings", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return single LAN configuration
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ReturnSingleLANConfigurationResponse> ReturnSingleLANConfiguration(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnSingleLANConfigurationResponse>($"networks/{networkId}/appliance/singleLan", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update single LAN configuration
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// subnet| string| The subnet of the single LAN configuration
        /// applianceIp| string| The appliance IP address of the single LAN
        /// ipv6| object| IPv6 configuration on the VLAN
        /// mandatoryDhcp| object| Mandatory DHCP will enforce that clients connecting to this LAN must use the IP address assigned by the DHCP server. Clients who use a static IP address won't be able to associate. Only available on firmware versions 17.0 and above
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/appliance/singleLan` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<UpdateSingleLANConfigurationResponse> UpdateSingleLANConfiguration(UpdateSingleLANConfigurationRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateSingleLANConfigurationResponse>($"networks/{networkId}/appliance/singleLan", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the MX SSIDs in a network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ListTheMXSSIDsInANetworkResponse> ListTheMXSSIDsInANetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ListTheMXSSIDsInANetworkResponse>($"networks/{networkId}/appliance/ssids", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return a single MX SSID
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="number">(Required) Number</param>
        public async Task<ReturnASingleMXSSIDResponse> ReturnASingleMXSSID(string networkId, string number, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnASingleMXSSIDResponse>($"networks/{networkId}/appliance/ssids/{number}", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the attributes of an MX SSID
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| The name of the SSID.
        /// enabled| boolean| Whether or not the SSID is enabled.
        /// defaultVlanId| integer| The VLAN ID of the VLAN associated to this SSID. This parameter is only valid if the network is in routed mode.
        /// authMode| string| The association control method for the SSID ('open', 'psk', '8021x-meraki' or '8021x-radius').
        /// psk| string| The passkey for the SSID. This param is only valid if the authMode is 'psk'.
        /// radiusServers| array| The RADIUS 802.1x servers to be used for authentication. This param is only valid if the authMode is '8021x-radius'.
        /// encryptionMode| string| The psk encryption mode for the SSID ('wep' or 'wpa'). This param is only valid if the authMode is 'psk'.
        /// wpaEncryptionMode| string| The types of WPA encryption. ('WPA1 and WPA2', 'WPA2 only', 'WPA3 Transition Mode' or 'WPA3 only'). This param is only valid if (1) the authMode is 'psk' & the encryptionMode is 'wpa' OR (2) the authMode is '8021x-meraki' OR (3) the authMode is '8021x-radius'
        /// visible| boolean| Boolean indicating whether the MX should advertise or hide this SSID.
        /// dhcpEnforcedDeauthentication| object| DHCP Enforced Deauthentication enables the disassociation of wireless clients in addition to Mandatory DHCP. This param is only valid on firmware versions >= MX 17.0 where the associated LAN has Mandatory DHCP Enabled 
        /// dot11w| object| The current setting for Protected Management Frames (802.11w).
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/appliance/ssids/{number}` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="number">(Required) Number</param>
        public async Task<UpdateTheAttributesOfAnMXSSIDResponse> UpdateTheAttributesOfAnMXSSID(UpdateTheAttributesOfAnMXSSIDRequest request, string networkId, string number, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateTheAttributesOfAnMXSSIDResponse>($"networks/{networkId}/appliance/ssids/{number}", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the static routes for an MX or teleworker network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ListTheStaticRoutesForAnMXOrTeleworkerNetworkResponse> ListTheStaticRoutesForAnMXOrTeleworkerNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ListTheStaticRoutesForAnMXOrTeleworkerNetworkResponse>($"networks/{networkId}/appliance/staticRoutes", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Add a static route for an MX or teleworker network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| The name of the new static route
        /// subnet| string| The subnet of the static route
        /// gatewayIp| string| The gateway IP (next hop) of the static route
        /// gatewayVlanId| string| The gateway IP (next hop) VLAN ID of the static route
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<Stream> AddAStaticRouteForAnMXOrTeleworkerNetwork(AddAStaticRouteForAnMXOrTeleworkerNetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"networks/{networkId}/appliance/staticRoutes", request, cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Return a static route for an MX or teleworker network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="staticRouteId">(Required) Static route ID</param>
        public async Task<ReturnAStaticRouteForAnMXOrTeleworkerNetworkResponse> ReturnAStaticRouteForAnMXOrTeleworkerNetwork(string networkId, string staticRouteId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnAStaticRouteForAnMXOrTeleworkerNetworkResponse>($"networks/{networkId}/appliance/staticRoutes/{staticRouteId}", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update a static route for an MX or teleworker network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| The name of the static route
        /// subnet| string| The subnet of the static route
        /// gatewayIp| string| The gateway IP (next hop) of the static route
        /// gatewayVlanId| string| The gateway IP (next hop) VLAN ID of the static route
        /// enabled| boolean| The enabled state of the static route
        /// fixedIpAssignments| object| The DHCP fixed IP assignments on the static route. This should be an object that contains mappings from MAC addresses to objects that themselves each contain "ip" and "name" string fields. See the sample request/response for more details.
        /// reservedIpRanges| array| The DHCP reserved IP ranges on the static route
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="staticRouteId">(Required) Static route ID</param>
        public async Task<UpdateAStaticRouteForAnMXOrTeleworkerNetworkResponse> UpdateAStaticRouteForAnMXOrTeleworkerNetwork(UpdateAStaticRouteForAnMXOrTeleworkerNetworkRequest request, string networkId, string staticRouteId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateAStaticRouteForAnMXOrTeleworkerNetworkResponse>($"networks/{networkId}/appliance/staticRoutes/{staticRouteId}", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Delete a static route from an MX or teleworker network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="staticRouteId">(Required) Static route ID</param>
        public async Task<Stream> DeleteAStaticRouteFromAnMXOrTeleworkerNetwork(string networkId, string staticRouteId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"networks/{networkId}/appliance/staticRoutes/{staticRouteId}", cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Display the traffic shaping settings for an MX network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<IsplayTheTrafficShapingSettingsForAnMXNetworkResponse> IsplayTheTrafficShapingSettingsForAnMXNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<IsplayTheTrafficShapingSettingsForAnMXNetworkResponse>($"networks/{networkId}/appliance/trafficShaping", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the traffic shaping settings for an MX network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// globalBandwidthLimits| object| Global per-client bandwidth limit
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<UpdateTheTrafficShapingSettingsForAnMXNetworkResponse> UpdateTheTrafficShapingSettingsForAnMXNetwork(UpdateTheTrafficShapingSettingsForAnMXNetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateTheTrafficShapingSettingsForAnMXNetworkResponse>($"networks/{networkId}/appliance/trafficShaping", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the VLANs for an MX network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ListTheVLANsForAnMXNetworkResponse> ListTheVLANsForAnMXNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ListTheVLANsForAnMXNetworkResponse>($"networks/{networkId}/appliance/vlans", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Add a VLAN
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// id| string| The VLAN ID of the new VLAN (must be between 1 and 4094)
        /// name| string| The name of the new VLAN
        /// subnet| string| The subnet of the VLAN
        /// applianceIp| string| The local IP of the appliance on the VLAN
        /// groupPolicyId| string| The id of the desired group policy to apply to the VLAN
        /// templateVlanType| string| Type of subnetting of the VLAN. Applicable only for template network.
        /// cidr| string| CIDR of the pool of subnets. Applicable only for template network. Each network bound to the template will automatically pick a subnet from this pool to build its own VLAN.
        /// mask| integer| Mask used for the subnet of all bound to the template networks. Applicable only for template network.
        /// ipv6| object| IPv6 configuration on the VLAN
        /// mandatoryDhcp| object| Mandatory DHCP will enforce that clients connecting to this VLAN must use the IP address assigned by the DHCP server. Clients who use a static IP address won't be able to associate. Only available on firmware versions 17.0 and above
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/appliance/vlans` | create
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<Stream> AddAVLAN(AddAVLANRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"networks/{networkId}/appliance/vlans", request, cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Return a VLAN
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="vlanId">(Required) Vlan ID</param>
        public async Task<ReturnAVLANResponse> ReturnAVLAN(string networkId, string vlanId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnAVLANResponse>($"networks/{networkId}/appliance/vlans/{vlanId}", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update a VLAN
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| The name of the VLAN
        /// subnet| string| The subnet of the VLAN
        /// applianceIp| string| The local IP of the appliance on the VLAN
        /// groupPolicyId| string| The id of the desired group policy to apply to the VLAN
        /// vpnNatSubnet| string| The translated VPN subnet if VPN and VPN subnet translation are enabled on the VLAN
        /// dhcpHandling| string| The appliance's handling of DHCP requests on this VLAN. One of: 'Run a DHCP server', 'Relay DHCP to another server' or 'Do not respond to DHCP requests'
        /// dhcpRelayServerIps| array| The IPs of the DHCP servers that DHCP requests should be relayed to
        /// dhcpLeaseTime| string| The term of DHCP leases if the appliance is running a DHCP server on this VLAN. One of: '30 minutes', '1 hour', '4 hours', '12 hours', '1 day' or '1 week'
        /// dhcpBootOptionsEnabled| boolean| Use DHCP boot options specified in other properties
        /// dhcpBootNextServer| string| DHCP boot option to direct boot clients to the server to load the boot file from
        /// dhcpBootFilename| string| DHCP boot option for boot filename
        /// fixedIpAssignments| object| The DHCP fixed IP assignments on the VLAN. This should be an object that contains mappings from MAC addresses to objects that themselves each contain "ip" and "name" string fields. See the sample request/response for more details.
        /// reservedIpRanges| array| The DHCP reserved IP ranges on the VLAN
        /// dnsNameservers| string| The DNS nameservers used for DHCP responses, either "upstream_dns", "google_dns", "opendns", or a newline seperated string of IP addresses or domain names
        /// dhcpOptions| array| The list of DHCP options that will be included in DHCP responses. Each object in the list should have "code", "type", and "value" properties.
        /// templateVlanType| string| Type of subnetting of the VLAN. Applicable only for template network.
        /// cidr| string| CIDR of the pool of subnets. Applicable only for template network. Each network bound to the template will automatically pick a subnet from this pool to build its own VLAN.
        /// mask| integer| Mask used for the subnet of all bound to the template networks. Applicable only for template network.
        /// ipv6| object| IPv6 configuration on the VLAN
        /// mandatoryDhcp| object| Mandatory DHCP will enforce that clients connecting to this VLAN must use the IP address assigned by the DHCP server. Clients who use a static IP address won't be able to associate. Only available on firmware versions 17.0 and above
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/appliance/vlans/{vlanId}` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="vlanId">(Required) Vlan ID</param>
        public async Task<UpdateAVLANResponse> UpdateAVLAN(UpdateAVLANRequest request, string networkId, string vlanId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateAVLANResponse>($"networks/{networkId}/appliance/vlans/{vlanId}", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Delete a VLAN from a network
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/appliance/vlans/{vlanId}` | destroy
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="vlanId">(Required) Vlan ID</param>
        public async Task<Stream> DeleteAVLANFromANetwork(string networkId, string vlanId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"networks/{networkId}/appliance/vlans/{vlanId}", cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Return a Hub BGP Configuration
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ReturnAHubBGPConfigurationResponse> ReturnAHubBGPConfiguration(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnAHubBGPConfigurationResponse>($"networks/{networkId}/appliance/vpn/bgp", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update a Hub BGP Configuration
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// enabled| boolean| Boolean value to enable or disable the BGP configuration. When BGP is enabled, the asNumber (ASN) will be autopopulated with the preconfigured ASN at other Hubs or a default value if there is no ASN configured.
        /// asNumber| integer| An Autonomous System Number (ASN) is required if you are to run BGP and peer with another BGP Speaker outside of the Auto VPN domain. This ASN will be applied to the entire Auto VPN domain. The entire 4-byte ASN range is supported. So, the ASN must be an integer between 1 and 4294967295. When absent, this field is not updated. If no value exists then it defaults to 64512.
        /// ibgpHoldTimer| integer| The IBGP holdtimer in seconds. The IBGP holdtimer must be an integer between 12 and 240. When absent, this field is not updated. If no value exists then it defaults to 240.
        /// neighbors| array| List of BGP neighbors. This list replaces the existing set of neighbors. When absent, this field is not updated.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/appliance/vpn/bgp` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<UpdateAHubBGPConfigurationResponse> UpdateAHubBGPConfiguration(UpdateAHubBGPConfigurationRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateAHubBGPConfigurationResponse>($"networks/{networkId}/appliance/vpn/bgp", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the site-to-site VPN settings of a network. Only valid for MX networks.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ReturnTheSiteToSiteVPNSettingsOfANetworkResponse> ReturnTheSiteToSiteVPNSettingsOfANetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnTheSiteToSiteVPNSettingsOfANetworkResponse>($"networks/{networkId}/appliance/vpn/siteToSiteVpn", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the site-to-site VPN settings of a network. Only valid for MX networks in NAT mode.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<UpdateTheSiteToSiteVPNSettingsOfANetworkResponse> UpdateTheSiteToSiteVPNSettingsOfANetwork(UpdateTheSiteToSiteVPNSettingsOfANetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateTheSiteToSiteVPNSettingsOfANetworkResponse>($"networks/{networkId}/appliance/vpn/siteToSiteVpn", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the third party VPN peers for an organization
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<ReturnTheThirdPartyVPNPeersForAnOrganizationResponse> ReturnTheThirdPartyVPNPeersForAnOrganization(string organizationId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnTheThirdPartyVPNPeersForAnOrganizationResponse>($"organizations/{organizationId}/appliance/vpn/thirdPartyVPNPeers", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the third party VPN peers for an organization
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// peers| array| The list of VPN peers
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/organizations/{organizationId}/appliance/vpn/thirdPartyVPNPeers` | update
        /// 
        /// 
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<UpdateTheThirdPartyVPNPeersForAnOrganizationResponse> UpdateTheThirdPartyVPNPeersForAnOrganization(UpdateTheThirdPartyVPNPeersForAnOrganizationRequest request, string organizationId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateTheThirdPartyVPNPeersForAnOrganizationResponse>($"organizations/{organizationId}/appliance/vpn/thirdPartyVPNPeers", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the firewall rules for an organization's site-to-site VPN
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<EFirewallRulesForAnOrganizationsSiteToSiteVPNResponse> EFirewallRulesForAnOrganizationsSiteToSiteVPN(string organizationId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<EFirewallRulesForAnOrganizationsSiteToSiteVPNResponse>($"organizations/{organizationId}/appliance/vpn/vpnFirewallRules", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the firewall rules of an organization's site-to-site VPN
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// rules| array| An ordered array of the firewall rules (not including the default rule)
        /// syslogDefaultRule| boolean| Log the special default rule (boolean value - enable only if you've configured a syslog server) (optional)
        /// 
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<HeFirewallRulesOfAnOrganizationsSiteToSiteVPNResponse> HeFirewallRulesOfAnOrganizationsSiteToSiteVPN(HeFirewallRulesOfAnOrganizationsSiteToSiteVPNRequest request, string organizationId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<HeFirewallRulesOfAnOrganizationsSiteToSiteVPNResponse>($"organizations/{organizationId}/appliance/vpn/vpnFirewallRules", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return MX warm spare settings
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ReturnMXWarmSpareSettingsResponse> ReturnMXWarmSpareSettings(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnMXWarmSpareSettingsResponse>($"networks/{networkId}/appliance/warmSpare", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update MX warm spare settings
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// enabled| boolean| Enable warm spare
        /// spareSerial| string| Serial number of the warm spare appliance
        /// uplinkMode| string| Uplink mode, either virtual or public
        /// virtualIp1| string| The WAN 1 shared IP
        /// virtualIp2| string| The WAN 2 shared IP
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/appliance/warmSpare` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<UpdateMXWarmSpareSettingsResponse> UpdateMXWarmSpareSettings(UpdateMXWarmSpareSettingsRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateMXWarmSpareSettingsResponse>($"networks/{networkId}/appliance/warmSpare", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Swap MX primary and warm spare appliances
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<SwapMXPrimaryAndWarmSpareAppliancesResponse> SwapMXPrimaryAndWarmSpareAppliances(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PostFromJsonAsync<SwapMXPrimaryAndWarmSpareAppliancesResponse>($"networks/{networkId}/appliance/warmSpare/swap", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Generate a snapshot of what the camera sees at the specified time and return a link to that image.
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// timestamp| string| [optional] The snapshot will be taken from this time on the camera. The timestamp is expected to be in ISO 8601 format. If no timestamp is specified, we will assume current time.
        /// fullframe| boolean| [optional] If set to "true" the snapshot will be taken at full sensor resolution. This will error if used with timestamp.
        /// 
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<Stream> EsAtTheSpecifiedTimeAndReturnALinkToThatImage(EsAtTheSpecifiedTimeAndReturnALinkToThatImageRequest request, string serial, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"devices/{serial}/camera/generateSnapshot", request, cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Return custom analytics settings for a camera
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<ReturnCustomAnalyticsSettingsForACameraResponse> ReturnCustomAnalyticsSettingsForACamera(string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnCustomAnalyticsSettingsForACameraResponse>($"devices/{serial}/camera/customAnalytics", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update custom analytics settings for a camera
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// enabled| boolean| Enable custom analytics
        /// artifactId| string| The ID of the custom analytics artifact
        /// parameters| array| Parameters for the custom analytics workload
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/devices/{serial}/camera/customAnalytics` | update
        /// 
        /// 
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<UpdateCustomAnalyticsSettingsForACameraResponse> UpdateCustomAnalyticsSettingsForACamera(UpdateCustomAnalyticsSettingsForACameraRequest request, string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateCustomAnalyticsSettingsForACameraResponse>($"devices/{serial}/camera/customAnalytics", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Returns quality and retention settings for the given camera
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<SQualityAndRetentionSettingsForTheGivenCameraResponse> SQualityAndRetentionSettingsForTheGivenCamera(string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<SQualityAndRetentionSettingsForTheGivenCameraResponse>($"devices/{serial}/camera/qualityAndRetention", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update quality and retention settings for the given camera
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// profileId| string| The ID of a quality and retention profile to assign to the camera. The profile's settings will override all of the per-camera quality and retention settings. If the value of this parameter is null, any existing profile will be unassigned from the camera.
        /// motionBasedRetentionEnabled| boolean| Boolean indicating if motion-based retention is enabled(true) or disabled(false) on the camera.
        /// audioRecordingEnabled| boolean| Boolean indicating if audio recording is enabled(true) or disabled(false) on the camera
        /// restrictedBandwidthModeEnabled| boolean| Boolean indicating if restricted bandwidth is enabled(true) or disabled(false) on the camera. This setting does not apply to MV2 cameras.
        /// quality| string| Quality of the camera. Can be one of 'Standard', 'High' or 'Enhanced'. Not all qualities are supported by every camera model.
        /// resolution| string| Resolution of the camera. Can be one of '1280x720', '1920x1080', '1080x1080', '2058x2058', '2112x2112', '2880x2880', '2688x1512' or '3840x2160'.Not all resolutions are supported by every camera model.
        /// motionDetectorVersion| integer| The version of the motion detector that will be used by the camera. Only applies to Gen 2 cameras. Defaults to v2.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/devices/{serial}/camera/qualityAndRetention` | update
        /// 
        /// 
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<EQualityAndRetentionSettingsForTheGivenCameraResponse> EQualityAndRetentionSettingsForTheGivenCamera(EQualityAndRetentionSettingsForTheGivenCameraRequest request, string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<EQualityAndRetentionSettingsForTheGivenCameraResponse>($"devices/{serial}/camera/qualityAndRetention", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Returns sense settings for a given camera
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<ReturnsSenseSettingsForAGivenCameraResponse> ReturnsSenseSettingsForAGivenCamera(string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnsSenseSettingsForAGivenCameraResponse>($"devices/{serial}/camera/sense", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update sense settings for the given camera
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// senseEnabled| boolean| Boolean indicating if sense(license) is enabled(true) or disabled(false) on the camera
        /// mqttBrokerId| string| The ID of the MQTT broker to be enabled on the camera. A value of null will disable MQTT on the camera
        /// audioDetection| object| The details of the audio detection config.
        /// detectionModelId| string| The ID of the object detection model
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/devices/{serial}/camera/sense` | update
        /// 
        /// 
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<UpdateSenseSettingsForTheGivenCameraResponse> UpdateSenseSettingsForTheGivenCamera(UpdateSenseSettingsForTheGivenCameraRequest request, string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateSenseSettingsForTheGivenCameraResponse>($"devices/{serial}/camera/sense", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Returns video settings for the given camera
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<ReturnsVideoSettingsForTheGivenCameraResponse> ReturnsVideoSettingsForTheGivenCamera(string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnsVideoSettingsForTheGivenCameraResponse>($"devices/{serial}/camera/video/settings", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update video settings for the given camera
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// externalRtspEnabled| boolean| Boolean indicating if external rtsp stream is exposed
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/devices/{serial}/camera/video/settings` | update
        /// 
        /// 
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<UpdateVideoSettingsForTheGivenCameraResponse> UpdateVideoSettingsForTheGivenCamera(UpdateVideoSettingsForTheGivenCameraRequest request, string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateVideoSettingsForTheGivenCameraResponse>($"devices/{serial}/camera/video/settings", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Returns video link to the specified camera. If a timestamp is supplied, it links to that timestamp.
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<ReturnsvideolinktothespecifiedcameraResponse> Returnsvideolinktothespecifiedcamera(ReturnsvideolinktothespecifiedcameraParameters queryParameters, string serial, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"devices/{serial}/camera/videoLink", parametersDict);
            return await _httpClient.GetFromJsonAsync<ReturnsvideolinktothespecifiedcameraResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Returns wireless profile assigned to the given camera
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<EturnsWirelessProfileAssignedToTheGivenCameraResponse> EturnsWirelessProfileAssignedToTheGivenCamera(string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<EturnsWirelessProfileAssignedToTheGivenCameraResponse>($"devices/{serial}/camera/wirelessProfiles", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Assign wireless profiles to the given camera. Incremental updates are not supported, all profile assignment need to be supplied at once.
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<AssignWirelessProfilesToTheGivenCameraResponse> AssignWirelessProfilesToTheGivenCamera(AssignWirelessProfilesToTheGivenCameraRequest request, string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<AssignWirelessProfilesToTheGivenCameraResponse>($"devices/{serial}/camera/wirelessProfiles", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Creates a new camera wireless profile for this network.
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| The name of the camera wireless profile. This parameter is required.
        /// ssid| object| The details of the SSID config.
        /// identity| object| The identity of the wireless profile. Required for creating wireless profiles in 8021x-radius auth mode.
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ReatesANewCameraWirelessProfileForThisNetworkResponse> ReatesANewCameraWirelessProfileForThisNetwork(ReatesANewCameraWirelessProfileForThisNetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PostJsonAsync<ReatesANewCameraWirelessProfileForThisNetworkResponse>($"networks/{networkId}/camera/wirelessProfiles", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the camera wireless profiles for this network.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ListTheCameraWirelessProfilesForThisNetworkResponse> ListTheCameraWirelessProfilesForThisNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ListTheCameraWirelessProfilesForThisNetworkResponse>($"networks/{networkId}/camera/wirelessProfiles", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Retrieve a single camera wireless profile.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="wirelessProfileId">(Required) Wireless profile ID</param>
        public async Task<RetrieveASingleCameraWirelessProfileResponse> RetrieveASingleCameraWirelessProfile(string networkId, string wirelessProfileId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<RetrieveASingleCameraWirelessProfileResponse>($"networks/{networkId}/camera/wirelessProfiles/{wirelessProfileId}", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update an existing camera wireless profile in this network.
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| The name of the camera wireless profile.
        /// ssid| object| The details of the SSID config.
        /// identity| object| The identity of the wireless profile. Required for creating wireless profiles in 8021x-radius auth mode.
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="wirelessProfileId">(Required) Wireless profile ID</param>
        public async Task<EAnExistingCameraWirelessProfileInThisNetworkResponse> EAnExistingCameraWirelessProfileInThisNetwork(EAnExistingCameraWirelessProfileInThisNetworkRequest request, string networkId, string wirelessProfileId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<EAnExistingCameraWirelessProfileInThisNetworkResponse>($"networks/{networkId}/camera/wirelessProfiles/{wirelessProfileId}", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Delete an existing camera wireless profile for this network.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="wirelessProfileId">(Required) Wireless profile ID</param>
        public async Task<Stream> AnExistingCameraWirelessProfileForThisNetwork(string networkId, string wirelessProfileId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"networks/{networkId}/camera/wirelessProfiles/{wirelessProfileId}", cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// List the quality retention profiles for this network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ListTheQualityRetentionProfilesForThisNetworkResponse> ListTheQualityRetentionProfilesForThisNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ListTheQualityRetentionProfilesForThisNetworkResponse>($"networks/{networkId}/camera/qualityRetentionProfiles", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Creates new quality retention profile for this network.
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| The name of the new profile. Must be unique. This parameter is required.
        /// motionBasedRetentionEnabled| boolean| Deletes footage older than 3 days in which no motion was detected. Can be either true or false. Defaults to false. This setting does not apply to MV2 cameras.
        /// restrictedBandwidthModeEnabled| boolean| Disable features that require additional bandwidth such as Motion Recap. Can be either true or false. Defaults to false. This setting does not apply to MV2 cameras.
        /// audioRecordingEnabled| boolean| Whether or not to record audio. Can be either true or false. Defaults to false.
        /// cloudArchiveEnabled| boolean| Create redundant video backup using Cloud Archive. Can be either true or false. Defaults to false.
        /// motionDetectorVersion| integer| The version of the motion detector that will be used by the camera. Only applies to Gen 2 cameras. Defaults to v2.
        /// scheduleId| string| Schedule for which this camera will record video, or 'null' to always record.
        /// maxRetentionDays| integer| The maximum number of days for which the data will be stored, or 'null' to keep data until storage space runs out. If the former, it can be one of [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 14, 30, 60, 90] days.
        /// videoSettings| object| Video quality and resolution settings for all the camera models.
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<EatesNewQualityRetentionProfileForThisNetworkResponse> EatesNewQualityRetentionProfileForThisNetwork(EatesNewQualityRetentionProfileForThisNetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PostJsonAsync<EatesNewQualityRetentionProfileForThisNetworkResponse>($"networks/{networkId}/camera/qualityRetentionProfiles", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update an existing quality retention profile for this network.
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| The name of the new profile. Must be unique.
        /// motionBasedRetentionEnabled| boolean| Deletes footage older than 3 days in which no motion was detected. Can be either true or false. Defaults to false. This setting does not apply to MV2 cameras.
        /// restrictedBandwidthModeEnabled| boolean| Disable features that require additional bandwidth such as Motion Recap. Can be either true or false. Defaults to false. This setting does not apply to MV2 cameras.
        /// audioRecordingEnabled| boolean| Whether or not to record audio. Can be either true or false. Defaults to false.
        /// cloudArchiveEnabled| boolean| Create redundant video backup using Cloud Archive. Can be either true or false. Defaults to false.
        /// motionDetectorVersion| integer| The version of the motion detector that will be used by the camera. Only applies to Gen 2 cameras. Defaults to v2.
        /// scheduleId| string| Schedule for which this camera will record video, or 'null' to always record.
        /// maxRetentionDays| integer| The maximum number of days for which the data will be stored, or 'null' to keep data until storage space runs out. If the former, it can be one of [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 14, 30, 60, 90] days.
        /// videoSettings| object| Video quality and resolution settings for all the camera models.
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="qualityRetentionProfileId">(Required) Quality retention profile ID</param>
        public async Task<ExistingQualityRetentionProfileForThisNetworkResponse> ExistingQualityRetentionProfileForThisNetwork(ExistingQualityRetentionProfileForThisNetworkRequest request, string networkId, string qualityRetentionProfileId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<ExistingQualityRetentionProfileForThisNetworkResponse>($"networks/{networkId}/camera/qualityRetentionProfiles/{qualityRetentionProfileId}", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Delete an existing quality retention profile for this network.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="qualityRetentionProfileId">(Required) Quality retention profile ID</param>
        public async Task<Stream> ExistingQualityRetentionProfileForThisNetwork2(string networkId, string qualityRetentionProfileId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"networks/{networkId}/camera/qualityRetentionProfiles/{qualityRetentionProfileId}", cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Retrieve a single quality retention profile
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="qualityRetentionProfileId">(Required) Quality retention profile ID</param>
        public async Task<RetrieveASingleQualityRetentionProfileResponse> RetrieveASingleQualityRetentionProfile(string networkId, string qualityRetentionProfileId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<RetrieveASingleQualityRetentionProfileResponse>($"networks/{networkId}/camera/qualityRetentionProfiles/{qualityRetentionProfileId}", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Returns a list of all camera recording schedules.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ReturnsalistofallcamerarecordingschedulesResponse> Returnsalistofallcamerarecordingschedules(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnsalistofallcamerarecordingschedulesResponse>($"networks/{networkId}/camera/schedules", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Fetch onboarding status of cameras
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<FetchOnboardingStatusOfCamerasResponse> FetchOnboardingStatusOfCameras(FetchOnboardingStatusOfCamerasParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/camera/onboarding/statuses", parametersDict);
            return await _httpClient.GetFromJsonAsync<FetchOnboardingStatusOfCamerasResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Notify that credential handoff to camera has completed
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// serial| string| Serial of camera
        /// wirelessCredentialsSent| boolean| Note whether credentials were sent successfully
        /// 
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<TifyThatCredentialHandoffToCameraHasCompletedResponse> TifyThatCredentialHandoffToCameraHasCompleted(TifyThatCredentialHandoffToCameraHasCompletedRequest request, string organizationId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<TifyThatCredentialHandoffToCameraHasCompletedResponse>($"organizations/{organizationId}/camera/onboarding/statuses", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Show the LAN Settings of a MG
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<ShowTheLANSettingsOfAMGResponse> ShowTheLANSettingsOfAMG(string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ShowTheLANSettingsOfAMGResponse>($"devices/{serial}/cellularGateway/lan", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the LAN Settings for a single MG.
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// reservedIpRanges| array| list of all reserved IP ranges for a single MG
        /// fixedIpAssignments| array| list of all fixed IP assignments for a single MG
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/devices/{serial}/cellularGateway/lan` | update
        /// 
        /// 
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<UpdateTheLANSettingsForASingleMGResponse> UpdateTheLANSettingsForASingleMG(UpdateTheLANSettingsForASingleMGRequest request, string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateTheLANSettingsForASingleMGResponse>($"devices/{serial}/cellularGateway/lan", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Returns the port forwarding rules for a single MG.
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<ReturnsThePortForwardingRulesForASingleMGResponse> ReturnsThePortForwardingRulesForASingleMG(string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnsThePortForwardingRulesForASingleMGResponse>($"devices/{serial}/cellularGateway/portForwardingRules", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Updates the port forwarding rules for a single MG.
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// rules| array| An array of port forwarding params
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/devices/{serial}/cellularGateway/portForwardingRules` | update
        /// 
        /// 
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<UpdatesThePortForwardingRulesForASingleMGResponse> UpdatesThePortForwardingRulesForASingleMG(UpdatesThePortForwardingRulesForASingleMGRequest request, string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdatesThePortForwardingRulesForASingleMGResponse>($"devices/{serial}/cellularGateway/portForwardingRules", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the connectivity testing destinations for an MG network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ConnectivityTestingDestinationsForAnMGNetworkResponse> ConnectivityTestingDestinationsForAnMGNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ConnectivityTestingDestinationsForAnMGNetworkResponse>($"networks/{networkId}/cellularGateway/connectivityMonitoringDestinations", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the connectivity testing destinations for an MG network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// destinations| array| The list of connectivity monitoring destinations
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/cellularGateway/connectivityMonitoringDestinations` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ConnectivityTestingDestinationsForAnMGNetwork2Response> ConnectivityTestingDestinationsForAnMGNetwork2(ConnectivityTestingDestinationsForAnMGNetwork2Request request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<ConnectivityTestingDestinationsForAnMGNetwork2Response>($"networks/{networkId}/cellularGateway/connectivityMonitoringDestinations", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List common DHCP settings of MGs
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ListCommonDHCPSettingsOfMGsResponse> ListCommonDHCPSettingsOfMGs(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ListCommonDHCPSettingsOfMGsResponse>($"networks/{networkId}/cellularGateway/dhcp", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update common DHCP settings of MGs
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// dhcpLeaseTime| string| DHCP Lease time for all MG of the network. Possible values are '30 minutes', '1 hour', '4 hours', '12 hours', '1 day' or '1 week'.
        /// dnsNameservers| string| DNS name servers mode for all MG of the network. Possible values are: 'upstream_dns', 'google_dns', 'opendns', 'custom'.
        /// dnsCustomNameservers| array| list of fixed IPs representing the the DNS Name servers when the mode is 'custom'
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/cellularGateway/dhcp` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<UpdateCommonDHCPSettingsOfMGsResponse> UpdateCommonDHCPSettingsOfMGs(UpdateCommonDHCPSettingsOfMGsRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateCommonDHCPSettingsOfMGsResponse>($"networks/{networkId}/cellularGateway/dhcp", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the subnet pool and mask configured for MGs in the network.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<SubnetPoolAndMaskConfiguredForMGsInTheNetworkResponse> SubnetPoolAndMaskConfiguredForMGsInTheNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<SubnetPoolAndMaskConfiguredForMGsInTheNetworkResponse>($"networks/{networkId}/cellularGateway/subnetPool", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the subnet pool and mask configuration for MGs in the network.
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// mask| integer| Mask used for the subnet of all MGs in  this network.
        /// cidr| string| CIDR of the pool of subnets. Each MG in this network will automatically pick a subnet from this pool.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/cellularGateway/subnetPool` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<NetPoolAndMaskConfigurationForMGsInTheNetworkResponse> NetPoolAndMaskConfigurationForMGsInTheNetwork(NetPoolAndMaskConfigurationForMGsInTheNetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<NetPoolAndMaskConfigurationForMGsInTheNetworkResponse>($"networks/{networkId}/cellularGateway/subnetPool", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Returns the uplink settings for your MG network.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ReturnsTheUplinkSettingsForYourMGNetworkResponse> ReturnsTheUplinkSettingsForYourMGNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnsTheUplinkSettingsForYourMGNetworkResponse>($"networks/{networkId}/cellularGateway/uplink", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Updates the uplink settings for your MG network.
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// bandwidthLimits| object| The bandwidth settings for the 'cellular' uplink
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/cellularGateway/uplink` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<UpdatesTheUplinkSettingsForYourMGNetworkResponse> UpdatesTheUplinkSettingsForYourMGNetwork(UpdatesTheUplinkSettingsForYourMGNetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdatesTheUplinkSettingsForYourMGNetworkResponse>($"networks/{networkId}/cellularGateway/uplink", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the uplink status of every Meraki MG cellular gateway in the organization
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<EveryMerakiMGcellulargatewayintheorganizationResponse> EveryMerakiMGcellulargatewayintheorganization(EveryMerakiMGcellulargatewayintheorganizationParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/cellularGateway/uplink/statuses", parametersDict);
            return await _httpClient.GetFromJsonAsync<EveryMerakiMGcellulargatewayintheorganizationResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the sensor roles for a given sensor or camera device.
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<StTheSensorRolesForAGivenSensorOrCameraDeviceResponse> StTheSensorRolesForAGivenSensorOrCameraDevice(string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<StTheSensorRolesForAGivenSensorOrCameraDeviceResponse>($"devices/{serial}/sensor/relationships", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Assign one or more sensor roles to a given sensor or camera device.
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// livestream| object| A role defined between an MT sensor and an MV camera that adds the camera's livestream to the sensor's details page. Snapshots from the camera will also appear in alert notifications that the sensor triggers.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/devices/{serial}/sensor/relationships` | update
        /// 
        /// 
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<OrMoreSensorRolesToAGivenSensorOrCameraDeviceResponse> OrMoreSensorRolesToAGivenSensorOrCameraDevice(OrMoreSensorRolesToAGivenSensorOrCameraDeviceRequest request, string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<OrMoreSensorRolesToAGivenSensorOrCameraDeviceResponse>($"devices/{serial}/sensor/relationships", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the sensor roles for devices in a given network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ListTheSensorRolesForDevicesInAGivenNetworkResponse> ListTheSensorRolesForDevicesInAGivenNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ListTheSensorRolesForDevicesInAGivenNetworkResponse>($"networks/{networkId}/sensor/relationships", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Lists all sensor alert profiles for a network.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ListsAllSensorAlertProfilesForANetworkResponse> ListsAllSensorAlertProfilesForANetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ListsAllSensorAlertProfilesForANetworkResponse>($"networks/{networkId}/sensor/alerts/profiles", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Creates a sensor alert profile for a network.
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| Name of the sensor alert profile.
        /// schedule| object| The sensor schedule to use with the alert profile.
        /// conditions| array| List of conditions that will cause the profile to send an alert.
        /// recipients| object| List of recipients that will receive the alert.
        /// serials| array| List of device serials assigned to this sensor alert profile.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/sensor/alerts/profiles` | create
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<CreatesASensorAlertProfileForANetworkResponse> CreatesASensorAlertProfileForANetwork(CreatesASensorAlertProfileForANetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PostJsonAsync<CreatesASensorAlertProfileForANetworkResponse>($"networks/{networkId}/sensor/alerts/profiles", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Show details of a sensor alert profile for a network.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="id">(Required) ID</param>
        public async Task<ShowDetailsOfASensorAlertProfileForANetworkResponse> ShowDetailsOfASensorAlertProfileForANetwork(string networkId, string id, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ShowDetailsOfASensorAlertProfileForANetworkResponse>($"networks/{networkId}/sensor/alerts/profiles/{id}", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Updates a sensor alert profile for a network.
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| Name of the sensor alert profile.
        /// schedule| object| The sensor schedule to use with the alert profile.
        /// conditions| array| List of conditions that will cause the profile to send an alert.
        /// recipients| object| List of recipients that will receive the alert.
        /// serials| array| List of device serials assigned to this sensor alert profile.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/sensor/alerts/profiles/{id}` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="id">(Required) ID</param>
        public async Task<UpdatesASensorAlertProfileForANetworkResponse> UpdatesASensorAlertProfileForANetwork(UpdatesASensorAlertProfileForANetworkRequest request, string networkId, string id, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdatesASensorAlertProfileForANetworkResponse>($"networks/{networkId}/sensor/alerts/profiles/{id}", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Deletes a sensor alert profile from a network.
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/sensor/alerts/profiles/{id}` | destroy
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="id">(Required) ID</param>
        public async Task<Stream> DeletesASensorAlertProfileFromANetwork(string networkId, string id, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"networks/{networkId}/sensor/alerts/profiles/{id}", cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// List the sensor settings of all MQTT brokers for this network. To get the brokers themselves, use /networks/{networkId}/mqttBrokers.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ESensorSettingsOfAllMQTTBrokersForThisNetworkResponse> ESensorSettingsOfAllMQTTBrokersForThisNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ESensorSettingsOfAllMQTTBrokersForThisNetworkResponse>($"networks/{networkId}/sensor/mqttBrokers", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the sensor settings of an MQTT broker. To get the broker itself, use /networks/{networkId}/mqttBrokers/{mqttBrokerId}.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="mqttBrokerId">(Required) Mqtt broker ID</param>
        public async Task<ReturnTheSensorSettingsOfAnMQTTBrokerResponse> ReturnTheSensorSettingsOfAnMQTTBroker(string networkId, string mqttBrokerId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnTheSensorSettingsOfAnMQTTBrokerResponse>($"networks/{networkId}/sensor/mqttBrokers/{mqttBrokerId}", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the sensor settings of an MQTT broker. To update the broker itself, use /networks/{networkId}/mqttBrokers/{mqttBrokerId}.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="mqttBrokerId">(Required) Mqtt broker ID</param>
        public async Task<UpdateTheSensorSettingsOfAnMQTTBrokerResponse> UpdateTheSensorSettingsOfAnMQTTBroker(UpdateTheSensorSettingsOfAnMQTTBrokerRequest request, string networkId, string mqttBrokerId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateTheSensorSettingsOfAnMQTTBrokerResponse>($"networks/{networkId}/sensor/mqttBrokers/{mqttBrokerId}", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return an overview of currently alerting sensors by metric
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<NanoverviewofcurrentlyalertingsensorsbymetricResponse> Nanoverviewofcurrentlyalertingsensorsbymetric(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<NanoverviewofcurrentlyalertingsensorsbymetricResponse>($"networks/{networkId}/sensor/alerts/current/overview/byMetric", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return an overview of alert occurrences over a timespan, by metric
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ErviewofalertoccurrencesoveratimespanbymetricResponse> Erviewofalertoccurrencesoveratimespanbymetric(ErviewofalertoccurrencesoveratimespanbymetricParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/sensor/alerts/overview/byMetric", parametersDict);
            return await _httpClient.GetFromJsonAsync<ErviewofalertoccurrencesoveratimespanbymetricResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return all reported readings from sensors in a given timespan, sorted by timestamp
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<SfromsensorsinagiventimespansortedbytimestampResponse> Sfromsensorsinagiventimespansortedbytimestamp(SfromsensorsinagiventimespansortedbytimestampParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/sensor/readings/history", parametersDict);
            return await _httpClient.GetFromJsonAsync<SfromsensorsinagiventimespansortedbytimestampResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the latest available reading for each metric from each sensor, sorted by sensor serial
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<ReachmetricfromeachsensorsortedbysensorserialResponse> Reachmetricfromeachsensorsortedbysensorserial(ReachmetricfromeachsensorsortedbysensorserialParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/sensor/readings/latest", parametersDict);
            return await _httpClient.GetFromJsonAsync<ReachmetricfromeachsensorsortedbysensorserialResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the switch ports for a switch
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<ListTheSwitchPortsForASwitchResponse> ListTheSwitchPortsForASwitch(string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ListTheSwitchPortsForASwitchResponse>($"devices/{serial}/switch/ports", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return a switch port
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        /// <param name="portId">(Required) Port ID</param>
        public async Task<ReturnASwitchPortResponse> ReturnASwitchPort(string serial, string portId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnASwitchPortResponse>($"devices/{serial}/switch/ports/{portId}", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update a switch port
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| The name of the switch port.
        /// tags| array| The list of tags of the switch port.
        /// enabled| boolean| The status of the switch port.
        /// poeEnabled| boolean| The PoE status of the switch port.
        /// type| string| The type of the switch port ('trunk' or 'access').
        /// vlan| integer| The VLAN of the switch port. A null value will clear the value set for trunk ports.
        /// voiceVlan| integer| The voice VLAN of the switch port. Only applicable to access ports.
        /// allowedVlans| string| The VLANs allowed on the switch port. Only applicable to trunk ports.
        /// isolationEnabled| boolean| The isolation status of the switch port.
        /// rstpEnabled| boolean| The rapid spanning tree protocol status.
        /// stpGuard| string| The state of the STP guard ('disabled', 'root guard', 'bpdu guard' or 'loop guard').
        /// linkNegotiation| string| The link speed for the switch port.
        /// portScheduleId| string| The ID of the port schedule. A value of null will clear the port schedule.
        /// udld| string| The action to take when Unidirectional Link is detected (Alert only, Enforce). Default configuration is Alert only.
        /// accessPolicyType| string| The type of the access policy of the switch port. Only applicable to access ports. Can be one of 'Open', 'Custom access policy', 'MAC allow list' or 'Sticky MAC allow list'.
        /// accessPolicyNumber| integer| The number of a custom access policy to configure on the switch port. Only applicable when 'accessPolicyType' is 'Custom access policy'.
        /// macAllowList| array| Only devices with MAC addresses specified in this list will have access to this port. Up to 20 MAC addresses can be defined. Only applicable when 'accessPolicyType' is 'MAC allow list'.
        /// stickyMacAllowList| array| The initial list of MAC addresses for sticky Mac allow list. Only applicable when 'accessPolicyType' is 'Sticky MAC allow list'.
        /// stickyMacAllowListLimit| integer| The maximum number of MAC addresses for sticky MAC allow list. Only applicable when 'accessPolicyType' is 'Sticky MAC allow list'.
        /// stormControlEnabled| boolean| The storm control status of the switch port.
        /// adaptivePolicyGroupId| string| The adaptive policy group ID that will be used to tag traffic through this switch port. This ID must pre-exist during the configuration, else needs to be created using adaptivePolicy/groups API. Cannot be applied to a port on a switch bound to profile.
        /// peerSgtCapable| boolean| If true, Peer SGT is enabled for traffic through this switch port. Applicable to trunk port only, not access port. Cannot be applied to a port on a switch bound to profile.
        /// flexibleStackingEnabled| boolean| For supported switches (e.g. MS420/MS425), whether or not the port has flexible stacking enabled.
        /// daiTrusted| boolean| If true, ARP packets for this port will be considered trusted, and Dynamic ARP Inspection will allow the traffic.
        /// profile| object| Profile attributes
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/devices/{serial}/switch/ports/{portId}` | update
        /// 
        /// 
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        /// <param name="portId">(Required) Port ID</param>
        public async Task<UpdateASwitchPortResponse> UpdateASwitchPort(UpdateASwitchPortRequest request, string serial, string portId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateASwitchPortResponse>($"devices/{serial}/switch/ports/{portId}", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List layer 3 interfaces for a switch. Those for a stack may be found under switch stack routing.
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<ListLayer3InterfacesForASwitchResponse> ListLayer3InterfacesForASwitch(string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ListLayer3InterfacesForASwitchResponse>($"devices/{serial}/switch/routing/interfaces", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Create a layer 3 interface for a switch
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| A friendly name or description for the interface or VLAN.
        /// subnet| string| The network that this routed interface is on, in CIDR notation (ex. 10.1.1.0/24).
        /// interfaceIp| string| The IP address this switch will use for layer 3 routing on this VLAN or subnet. This cannot be the same         as the switch's management IP.
        /// multicastRouting| string| Enable multicast support if, multicast routing between VLANs is required. Options are:         'disabled', 'enabled' or 'IGMP snooping querier'. Default is 'disabled'.
        /// vlanId| integer| The VLAN this routed interface is on. VLAN must be between 1 and 4094.
        /// defaultGateway| string| The next hop for any traffic that isn't going to a directly connected subnet or over a static route.         This IP address must exist in a subnet with a routed interface. Required if this is the first IPv4 interface.
        /// ospfSettings| object| The OSPF routing settings of the interface.
        /// ospfV3| object| The OSPFv3 routing settings of the interface.
        /// ipv6| object| The IPv6 settings of the interface.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/devices/{serial}/switch/routing/interfaces` | create
        /// 
        /// 
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<Stream> CreateALayer3InterfaceForASwitch(CreateALayer3InterfaceForASwitchRequest request, string serial, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"devices/{serial}/switch/routing/interfaces", request, cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Return a layer 3 interface for a switch
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        /// <param name="interfaceId">(Required) Interface ID</param>
        public async Task<ReturnALayer3InterfaceForASwitchResponse> ReturnALayer3InterfaceForASwitch(string serial, string interfaceId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnALayer3InterfaceForASwitchResponse>($"devices/{serial}/switch/routing/interfaces/{interfaceId}", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update a layer 3 interface for a switch
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| A friendly name or description for the interface or VLAN.
        /// subnet| string| The network that this routed interface is on, in CIDR notation (ex. 10.1.1.0/24).
        /// interfaceIp| string| The IP address this switch will use for layer 3 routing on this VLAN or subnet. This cannot be the same         as the switch's management IP.
        /// multicastRouting| string| Enable multicast support if, multicast routing between VLANs is required. Options are:         'disabled', 'enabled' or 'IGMP snooping querier'. Default is 'disabled'.
        /// vlanId| integer| The VLAN this routed interface is on. VLAN must be between 1 and 4094.
        /// defaultGateway| string| The next hop for any traffic that isn't going to a directly connected subnet or over a static route.         This IP address must exist in a subnet with a routed interface. Required if this is the first IPv4 interface.
        /// ospfSettings| object| The OSPF routing settings of the interface.
        /// ospfV3| object| The OSPFv3 routing settings of the interface.
        /// ipv6| object| The IPv6 settings of the interface.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/devices/{serial}/switch/routing/interfaces/{interfaceId}` | update
        /// 
        /// 
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        /// <param name="interfaceId">(Required) Interface ID</param>
        public async Task<UpdateALayer3InterfaceForASwitchResponse> UpdateALayer3InterfaceForASwitch(UpdateALayer3InterfaceForASwitchRequest request, string serial, string interfaceId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateALayer3InterfaceForASwitchResponse>($"devices/{serial}/switch/routing/interfaces/{interfaceId}", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Delete a layer 3 interface from the switch
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/devices/{serial}/switch/routing/interfaces/{interfaceId}` | destroy
        /// 
        /// 
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        /// <param name="interfaceId">(Required) Interface ID</param>
        public async Task<Stream> DeleteALayer3InterfaceFromTheSwitch(string serial, string interfaceId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"devices/{serial}/switch/routing/interfaces/{interfaceId}", cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// List layer 3 static routes for a switch
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<ListLayer3StaticRoutesForASwitchResponse> ListLayer3StaticRoutesForASwitch(string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ListLayer3StaticRoutesForASwitchResponse>($"devices/{serial}/switch/routing/staticRoutes", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Create a layer 3 static route for a switch
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| Name or description for layer 3 static route
        /// subnet| string| The subnet which is routed via this static route and should be specified in CIDR notation (ex. 1.2.3.0/24)
        /// nextHopIp| string| IP address of the next hop device to which the device sends its traffic for the subnet
        /// advertiseViaOspfEnabled| boolean| Option to advertise static route via OSPF
        /// preferOverOspfRoutesEnabled| boolean| Option to prefer static route over OSPF routes
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/devices/{serial}/switch/routing/staticRoutes` | create
        /// 
        /// 
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<Stream> CreateALayer3StaticRouteForASwitch(CreateALayer3StaticRouteForASwitchRequest request, string serial, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"devices/{serial}/switch/routing/staticRoutes", request, cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Return a layer 3 static route for a switch
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        /// <param name="staticRouteId">(Required) Static route ID</param>
        public async Task<ReturnALayer3StaticRouteForASwitchResponse> ReturnALayer3StaticRouteForASwitch(string serial, string staticRouteId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnALayer3StaticRouteForASwitchResponse>($"devices/{serial}/switch/routing/staticRoutes/{staticRouteId}", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update a layer 3 static route for a switch
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| Name or description for layer 3 static route
        /// subnet| string| The subnet which is routed via this static route and should be specified in CIDR notation (ex. 1.2.3.0/24)
        /// nextHopIp| string| IP address of the next hop device to which the device sends its traffic for the subnet
        /// advertiseViaOspfEnabled| boolean| Option to advertise static route via OSPF
        /// preferOverOspfRoutesEnabled| boolean| Option to prefer static route over OSPF routes
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/devices/{serial}/switch/routing/staticRoutes/{staticRouteId}` | update
        /// 
        /// 
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        /// <param name="staticRouteId">(Required) Static route ID</param>
        public async Task<UpdateALayer3StaticRouteForASwitchResponse> UpdateALayer3StaticRouteForASwitch(UpdateALayer3StaticRouteForASwitchRequest request, string serial, string staticRouteId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateALayer3StaticRouteForASwitchResponse>($"devices/{serial}/switch/routing/staticRoutes/{staticRouteId}", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Delete a layer 3 static route for a switch
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/devices/{serial}/switch/routing/staticRoutes/{staticRouteId}` | destroy
        /// 
        /// 
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        /// <param name="staticRouteId">(Required) Static route ID</param>
        public async Task<Stream> DeleteALayer3StaticRouteForASwitch(string serial, string staticRouteId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"devices/{serial}/switch/routing/staticRoutes/{staticRouteId}", cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Return multicast settings for a network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ReturnMulticastSettingsForANetworkResponse> ReturnMulticastSettingsForANetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnMulticastSettingsForANetworkResponse>($"networks/{networkId}/switch/routing/multicast", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update multicast settings for a network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// defaultSettings| object| Default multicast setting for entire network. IGMP snooping and Flood unknown multicast traffic settings are enabled by default.
        /// overrides| array| Array of paired switches/stacks/profiles and corresponding multicast settings. An empty array will clear the multicast settings.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/switch/routing/multicast` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<UpdateMulticastSettingsForANetworkResponse> UpdateMulticastSettingsForANetwork(UpdateMulticastSettingsForANetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateMulticastSettingsForANetworkResponse>($"networks/{networkId}/switch/routing/multicast", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return layer 3 OSPF routing configuration
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ReturnLayer3OSPFRoutingConfigurationResponse> ReturnLayer3OSPFRoutingConfiguration(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnLayer3OSPFRoutingConfigurationResponse>($"networks/{networkId}/switch/routing/ospf", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update layer 3 OSPF routing configuration
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// enabled| boolean| Boolean value to enable or disable OSPF routing. OSPF routing is disabled by default.
        /// helloTimerInSeconds| integer| Time interval in seconds at which hello packet will be sent to OSPF neighbors to maintain connectivity. Value must be between 1 and 255. Default is 10 seconds.
        /// deadTimerInSeconds| integer| Time interval to determine when the peer will be declared inactive/dead. Value must be between 1 and 65535
        /// areas| array| OSPF areas
        /// v3| object| OSPF v3 configuration
        /// md5AuthenticationEnabled| boolean| Boolean value to enable or disable MD5 authentication. MD5 authentication is disabled by default.
        /// md5AuthenticationKey| object| MD5 authentication credentials. This param is only relevant if md5AuthenticationEnabled is true
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/switch/routing/ospf` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<UpdateLayer3OSPFRoutingConfigurationResponse> UpdateLayer3OSPFRoutingConfiguration(UpdateLayer3OSPFRoutingConfigurationRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateLayer3OSPFRoutingConfigurationResponse>($"networks/{networkId}/switch/routing/ospf", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return warm spare configuration for a switch
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<ReturnWarmSpareConfigurationForASwitchResponse> ReturnWarmSpareConfigurationForASwitch(string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnWarmSpareConfigurationForASwitchResponse>($"devices/{serial}/switch/warmSpare", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update warm spare configuration for a switch. The spare will use the same L3 configuration as the primary. Note that this will irreversibly destroy any existing L3 configuration on the spare.
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<UpdateWarmSpareConfigurationForASwitchResponse> UpdateWarmSpareConfigurationForASwitch(UpdateWarmSpareConfigurationForASwitchRequest request, string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateWarmSpareConfigurationForASwitchResponse>($"devices/{serial}/switch/warmSpare", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the access control lists for a MS network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ReturnTheAccessControlListsForAMSNetworkResponse> ReturnTheAccessControlListsForAMSNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnTheAccessControlListsForAMSNetworkResponse>($"networks/{networkId}/switch/accessControlLists", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the access control lists for a MS network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// rules| array| An ordered array of the access control list rules (not including the default rule). An empty array will clear the rules.
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<UpdateTheAccessControlListsForAMSNetworkResponse> UpdateTheAccessControlListsForAMSNetwork(UpdateTheAccessControlListsForAMSNetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateTheAccessControlListsForAMSNetworkResponse>($"networks/{networkId}/switch/accessControlLists", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the access policies for a switch network. Only returns access policies with 'my RADIUS server' as authentication method
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ListTheAccessPoliciesForASwitchNetworkResponse> ListTheAccessPoliciesForASwitchNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ListTheAccessPoliciesForASwitchNetworkResponse>($"networks/{networkId}/switch/accessPolicies", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Create an access policy for a switch network. If you would like to enable Meraki Authentication, set radiusServers to empty array.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<Stream> CreateAnAccessPolicyForASwitchNetwork(CreateAnAccessPolicyForASwitchNetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"networks/{networkId}/switch/accessPolicies", request, cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Return a specific access policy for a switch network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="accessPolicyNumber">(Required) Access policy number</param>
        public async Task<ReturnASpecificAccessPolicyForASwitchNetworkResponse> ReturnASpecificAccessPolicyForASwitchNetwork(string networkId, string accessPolicyNumber, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnASpecificAccessPolicyForASwitchNetworkResponse>($"networks/{networkId}/switch/accessPolicies/{accessPolicyNumber}", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update an access policy for a switch network. If you would like to enable Meraki Authentication, set radiusServers to empty array.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="accessPolicyNumber">(Required) Access policy number</param>
        public async Task<UpdateAnAccessPolicyForASwitchNetworkResponse> UpdateAnAccessPolicyForASwitchNetwork(UpdateAnAccessPolicyForASwitchNetworkRequest request, string networkId, string accessPolicyNumber, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateAnAccessPolicyForASwitchNetworkResponse>($"networks/{networkId}/switch/accessPolicies/{accessPolicyNumber}", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Delete an access policy for a switch network
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/switch/accessPolicies/{accessPolicyNumber}` | destroy
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="accessPolicyNumber">(Required) Access policy number</param>
        public async Task<Stream> DeleteAnAccessPolicyForASwitchNetwork(string networkId, string accessPolicyNumber, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"networks/{networkId}/switch/accessPolicies/{accessPolicyNumber}", cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Return the switch alternate management interface for the network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ItchAlternateManagementInterfaceForTheNetworkResponse> ItchAlternateManagementInterfaceForTheNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ItchAlternateManagementInterfaceForTheNetworkResponse>($"networks/{networkId}/switch/alternateManagementInterface", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the switch alternate management interface for the network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// enabled| boolean| Boolean value to enable or disable AMI configuration. If enabled, VLAN and protocols must be set
        /// vlanId| integer| Alternate management VLAN, must be between 1 and 4094
        /// protocols| array| Can be one or more of the following values: 'radius', 'snmp' or 'syslog'
        /// switches| array| Array of switch serial number and IP assignment. If parameter is present, it cannot have empty body. Note: switches parameter is not applicable for template networks, in other words, do not put 'switches' in the body when updating template networks. Also, an empty 'switches' array will remove all previous assignments
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/switch/alternateManagementInterface` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ItchAlternateManagementInterfaceForTheNetwork2Response> ItchAlternateManagementInterfaceForTheNetwork2(ItchAlternateManagementInterfaceForTheNetwork2Request request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<ItchAlternateManagementInterfaceForTheNetwork2Response>($"networks/{networkId}/switch/alternateManagementInterface", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the network's DHCPv4 servers seen within the selected timeframe (default 1 day)
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<Versseenwithintheselectedtimeframedefault1dayResponse> Versseenwithintheselectedtimeframedefault1day(Versseenwithintheselectedtimeframedefault1dayParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/switch/dhcp/v4/servers/seen", parametersDict);
            return await _httpClient.GetFromJsonAsync<Versseenwithintheselectedtimeframedefault1dayResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the DHCP server settings. Blocked/allowed servers are only applied when default policy is allow/block, respectively
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ReturnTheDHCPServerSettingsResponse> ReturnTheDHCPServerSettings(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnTheDHCPServerSettingsResponse>($"networks/{networkId}/switch/dhcpServerPolicy", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the DHCP server settings. Blocked/allowed servers are only applied when default policy is allow/block, respectively
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<UpdateTheDHCPServerSettingsResponse> UpdateTheDHCPServerSettings(UpdateTheDHCPServerSettingsRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateTheDHCPServerSettingsResponse>($"networks/{networkId}/switch/dhcpServerPolicy", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the DSCP to CoS mappings
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ReturnTheDSCPToCoSMappingsResponse> ReturnTheDSCPToCoSMappings(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnTheDSCPToCoSMappingsResponse>($"networks/{networkId}/switch/dscpToCosMappings", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the DSCP to CoS mappings
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// mappings| array| An array of DSCP to CoS mappings. An empty array will reset the mappings to default.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/switch/dscpToCosMappings` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<UpdateTheDSCPToCoSMappingsResponse> UpdateTheDSCPToCoSMappings(UpdateTheDSCPToCoSMappingsRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateTheDSCPToCoSMappingsResponse>($"networks/{networkId}/switch/dscpToCosMappings", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List link aggregation groups
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ListLinkAggregationGroupsResponse> ListLinkAggregationGroups(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ListLinkAggregationGroupsResponse>($"networks/{networkId}/switch/linkAggregations", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Create a link aggregation group
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// switchPorts| array| Array of switch or stack ports for creating aggregation group. Minimum 2 and maximum 8 ports are supported.
        /// switchProfilePorts| array| Array of switch profile ports for creating aggregation group. Minimum 2 and maximum 8 ports are supported.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/switch/linkAggregations` | create
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<Stream> CreateALinkAggregationGroup(CreateALinkAggregationGroupRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"networks/{networkId}/switch/linkAggregations", request, cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Update a link aggregation group
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// switchPorts| array| Array of switch or stack ports for updating aggregation group. Minimum 2 and maximum 8 ports are supported.
        /// switchProfilePorts| array| Array of switch profile ports for updating aggregation group. Minimum 2 and maximum 8 ports are supported.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/switch/linkAggregations/{linkAggregationId}` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="linkAggregationId">(Required) Link aggregation ID</param>
        public async Task<UpdateALinkAggregationGroupResponse> UpdateALinkAggregationGroup(UpdateALinkAggregationGroupRequest request, string networkId, string linkAggregationId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateALinkAggregationGroupResponse>($"networks/{networkId}/switch/linkAggregations/{linkAggregationId}", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Split a link aggregation group into separate ports
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/switch/linkAggregations/{linkAggregationId}` | destroy
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="linkAggregationId">(Required) Link aggregation ID</param>
        public async Task<Stream> SplitALinkAggregationGroupIntoSeparatePorts(string networkId, string linkAggregationId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"networks/{networkId}/switch/linkAggregations/{linkAggregationId}", cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Return the MTU configuration
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ReturnTheMTUConfigurationResponse> ReturnTheMTUConfiguration(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnTheMTUConfigurationResponse>($"networks/{networkId}/switch/mtu", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the MTU configuration
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// defaultMtuSize| integer| MTU size for the entire network. Default value is 9578.
        /// overrides| array| Override MTU size for individual switches or switch profiles. An empty array will clear overrides.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/switch/mtu` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<UpdateTheMTUConfigurationResponse> UpdateTheMTUConfiguration(UpdateTheMTUConfigurationRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateTheMTUConfigurationResponse>($"networks/{networkId}/switch/mtu", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List switch port schedules
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ListSwitchPortSchedulesResponse> ListSwitchPortSchedules(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ListSwitchPortSchedulesResponse>($"networks/{networkId}/switch/portSchedules", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Add a switch port schedule
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| The name for your port schedule. Required
        /// portSchedule| object|     The schedule for switch port scheduling. Schedules are applied to days of the week.
        ///     When it's empty, default schedule with all days of a week are configured.
        ///     Any unspecified day in the schedule is added as a default schedule configuration of the day.
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<Stream> AddASwitchPortSchedule(AddASwitchPortScheduleRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"networks/{networkId}/switch/portSchedules", request, cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Delete a switch port schedule
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="portScheduleId">(Required) Port schedule ID</param>
        public async Task<Stream> DeleteASwitchPortSchedule(string networkId, string portScheduleId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"networks/{networkId}/switch/portSchedules/{portScheduleId}", cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Update a switch port schedule
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| The name for your port schedule.
        /// portSchedule| object|     The schedule for switch port scheduling. Schedules are applied to days of the week.
        ///     When it's empty, default schedule with all days of a week are configured.
        ///     Any unspecified day in the schedule is added as a default schedule configuration of the day.
        /// 
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/switch/portSchedules/{portScheduleId}` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="portScheduleId">(Required) Port schedule ID</param>
        public async Task<UpdateASwitchPortScheduleResponse> UpdateASwitchPortSchedule(UpdateASwitchPortScheduleRequest request, string networkId, string portScheduleId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateASwitchPortScheduleResponse>($"networks/{networkId}/switch/portSchedules/{portScheduleId}", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List quality of service rules
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ListQualityOfServiceRulesResponse> ListQualityOfServiceRules(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ListQualityOfServiceRulesResponse>($"networks/{networkId}/switch/qosRules", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Add a quality of service rule
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// vlan| integer| The VLAN of the incoming packet. A null value will match any VLAN.
        /// protocol| string| The protocol of the incoming packet. Can be one of "ANY", "TCP" or "UDP". Default value is "ANY"
        /// srcPort| integer| The source port of the incoming packet. Applicable only if protocol is TCP or UDP.
        /// srcPortRange| string| The source port range of the incoming packet. Applicable only if protocol is set to TCP or UDP. Example: 70-80
        /// dstPort| integer| The destination port of the incoming packet. Applicable only if protocol is TCP or UDP.
        /// dstPortRange| string| The destination port range of the incoming packet. Applicable only if protocol is set to TCP or UDP. Example: 70-80
        /// dscp| integer| DSCP tag. Set this to -1 to trust incoming DSCP. Default value is 0
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/switch/qosRules` | create
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<Stream> AddAQualityOfServiceRule(AddAQualityOfServiceRuleRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"networks/{networkId}/switch/qosRules", request, cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Return a quality of service rule
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="qosRuleId">(Required) Qos rule ID</param>
        public async Task<ReturnAQualityOfServiceRuleResponse> ReturnAQualityOfServiceRule(string networkId, string qosRuleId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnAQualityOfServiceRuleResponse>($"networks/{networkId}/switch/qosRules/{qosRuleId}", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Delete a quality of service rule
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/switch/qosRules/{qosRuleId}` | destroy
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="qosRuleId">(Required) Qos rule ID</param>
        public async Task<Stream> DeleteAQualityOfServiceRule(string networkId, string qosRuleId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"networks/{networkId}/switch/qosRules/{qosRuleId}", cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Update a quality of service rule
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// vlan| integer| The VLAN of the incoming packet. A null value will match any VLAN.
        /// protocol| string| The protocol of the incoming packet. Can be one of "ANY", "TCP" or "UDP". Default value is "ANY".
        /// srcPort| integer| The source port of the incoming packet. Applicable only if protocol is TCP or UDP.
        /// srcPortRange| string| The source port range of the incoming packet. Applicable only if protocol is set to TCP or UDP. Example: 70-80
        /// dstPort| integer| The destination port of the incoming packet. Applicable only if protocol is TCP or UDP.
        /// dstPortRange| string| The destination port range of the incoming packet. Applicable only if protocol is set to TCP or UDP. Example: 70-80
        /// dscp| integer| DSCP tag that should be assigned to incoming packet. Set this to -1 to trust incoming DSCP. Default value is 0.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/switch/qosRules/{qosRuleId}` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="qosRuleId">(Required) Qos rule ID</param>
        public async Task<UpdateAQualityOfServiceRuleResponse> UpdateAQualityOfServiceRule(UpdateAQualityOfServiceRuleRequest request, string networkId, string qosRuleId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateAQualityOfServiceRuleResponse>($"networks/{networkId}/switch/qosRules/{qosRuleId}", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Returns the switch network settings
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ReturnsTheSwitchNetworkSettingsResponse> ReturnsTheSwitchNetworkSettings(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnsTheSwitchNetworkSettingsResponse>($"networks/{networkId}/switch/settings", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update switch network settings
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// vlan| integer| Management VLAN
        /// useCombinedPower| boolean| The use Combined Power as the default behavior of secondary power supplies on supported devices.
        /// powerExceptions| array| Exceptions on a per switch basis to "useCombinedPower"
        /// uplinkClientSampling| object| Uplink client sampling
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/switch/settings` | settings/update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<UpdateSwitchNetworkSettingsResponse> UpdateSwitchNetworkSettings(UpdateSwitchNetworkSettingsRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateSwitchNetworkSettingsResponse>($"networks/{networkId}/switch/settings", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the switch stacks in a network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ListTheSwitchStacksInANetworkResponse> ListTheSwitchStacksInANetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ListTheSwitchStacksInANetworkResponse>($"networks/{networkId}/switch/stacks", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Create a stack
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| The name of the new stack
        /// serials| array| An array of switch serials to be added into the new stack
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<Stream> CreateAStack(CreateAStackRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"networks/{networkId}/switch/stacks", request, cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Show a switch stack
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="switchStackId">(Required) Switch stack ID</param>
        public async Task<ShowASwitchStackResponse> ShowASwitchStack(string networkId, string switchStackId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ShowASwitchStackResponse>($"networks/{networkId}/switch/stacks/{switchStackId}", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Delete a stack
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="switchStackId">(Required) Switch stack ID</param>
        public async Task<Stream> DeleteAStack(string networkId, string switchStackId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"networks/{networkId}/switch/stacks/{switchStackId}", cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Add a switch to a stack
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// serial| string| The serial of the switch to be added
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="switchStackId">(Required) Switch stack ID</param>
        public async Task<AddASwitchToAStackResponse> AddASwitchToAStack(AddASwitchToAStackRequest request, string networkId, string switchStackId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PostJsonAsync<AddASwitchToAStackResponse>($"networks/{networkId}/switch/stacks/{switchStackId}/add", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Remove a switch from a stack
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// serial| string| The serial of the switch to be removed
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="switchStackId">(Required) Switch stack ID</param>
        public async Task<RemoveASwitchFromAStackResponse> RemoveASwitchFromAStack(RemoveASwitchFromAStackRequest request, string networkId, string switchStackId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PostJsonAsync<RemoveASwitchFromAStackResponse>($"networks/{networkId}/switch/stacks/{switchStackId}/remove", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the storm control configuration for a switch network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<TheStormControlConfigurationForASwitchNetworkResponse> TheStormControlConfigurationForASwitchNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<TheStormControlConfigurationForASwitchNetworkResponse>($"networks/{networkId}/switch/stormControl", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the storm control configuration for a switch network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// broadcastThreshold| integer| Percentage (1 to 99) of total available port bandwidth for broadcast traffic type. Default value 100 percent rate is to clear the configuration.
        /// multicastThreshold| integer| Percentage (1 to 99) of total available port bandwidth for multicast traffic type. Default value 100 percent rate is to clear the configuration.
        /// unknownUnicastThreshold| integer| Percentage (1 to 99) of total available port bandwidth for unknown unicast (dlf-destination lookup failure) traffic type. Default value 100 percent rate is to clear the configuration.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/switch/stormControl` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<TheStormControlConfigurationForASwitchNetwork2Response> TheStormControlConfigurationForASwitchNetwork2(TheStormControlConfigurationForASwitchNetwork2Request request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<TheStormControlConfigurationForASwitchNetwork2Response>($"networks/{networkId}/switch/stormControl", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Returns STP settings
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ReturnsSTPSettingsResponse> ReturnsSTPSettings(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnsSTPSettingsResponse>($"networks/{networkId}/switch/stp", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Updates STP settings
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// rstpEnabled| boolean| The spanning tree protocol status in network
        /// stpBridgePriority| array| STP bridge priority for switches/stacks or switch profiles. An empty array will clear the STP bridge priority settings.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/switch/stp` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<UpdatesSTPSettingsResponse> UpdatesSTPSettings(UpdatesSTPSettingsRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdatesSTPSettingsResponse>($"networks/{networkId}/switch/stp", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the switch profiles for your switch template configuration
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        /// <param name="configTemplateId">(Required) Config template ID</param>
        public async Task<TchProfilesForYourSwitchTemplateConfigurationResponse> TchProfilesForYourSwitchTemplateConfiguration(string organizationId, string configTemplateId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<TchProfilesForYourSwitchTemplateConfigurationResponse>($"organizations/{organizationId}/configTemplates/{configTemplateId}/switch/profiles", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Clone port-level and some switch-level configuration settings from a source switch to one or more target switches. Cloned settings include: Aggregation Groups, Power Settings, Multicast Settings, MTU Configuration, STP Bridge priority, Port Mirroring
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<NgsfromasourceswitchtooneormoretargetswitchesResponse> Ngsfromasourceswitchtooneormoretargetswitches(NgsfromasourceswitchtooneormoretargetswitchesRequest request, string organizationId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PostJsonAsync<NgsfromasourceswitchtooneormoretargetswitchesResponse>($"organizations/{organizationId}/switch/devices/clone", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Cycle a set of switch ports
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// ports| array| List of switch ports
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/devices/{serial}/switch/ports` | cycle
        /// 
        /// 
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<CycleasetofswitchportsResponse> Cycleasetofswitchports(CycleasetofswitchportsRequest request, string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PostJsonAsync<CycleasetofswitchportsResponse>($"devices/{serial}/switch/ports/cycle", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the status for all the ports of a switch
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<ReturnTheStatusForAllThePortsOfASwitchResponse> ReturnTheStatusForAllThePortsOfASwitch(ReturnTheStatusForAllThePortsOfASwitchParameters queryParameters, string serial, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"devices/{serial}/switch/ports/statuses", parametersDict);
            return await _httpClient.GetFromJsonAsync<ReturnTheStatusForAllThePortsOfASwitchResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the bluetooth settings for a wireless device
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// uuid| string| Desired UUID of the beacon. If the value is set to null it will reset to Dashboard's automatically generated value.
        /// major| integer| Desired major value of the beacon. If the value is set to null it will reset to Dashboard's automatically generated value.
        /// minor| integer| Desired minor value of the beacon. If the value is set to null it will reset to Dashboard's automatically generated value.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/devices/{serial}/wireless/bluetooth/settings` | update
        /// 
        /// 
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<UpdateTheBluetoothSettingsForAWirelessDeviceResponse> UpdateTheBluetoothSettingsForAWirelessDevice(UpdateTheBluetoothSettingsForAWirelessDeviceRequest request, string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateTheBluetoothSettingsForAWirelessDeviceResponse>($"devices/{serial}/wireless/bluetooth/settings", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the bluetooth settings for a wireless device
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<ReturnTheBluetoothSettingsForAWirelessDeviceResponse> ReturnTheBluetoothSettingsForAWirelessDevice(string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnTheBluetoothSettingsForAWirelessDeviceResponse>($"devices/{serial}/wireless/bluetooth/settings", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the Bluetooth settings for a network. <a href="https://documentation.meraki.com/MR/Bluetooth/Bluetooth_Low_Energy_(BLE)">Bluetooth settings</a> must be enabled on the network.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<LEBluetoothSettingsAMustBeEnabledOnTheNetworkResponse> LEBluetoothSettingsAMustBeEnabledOnTheNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<LEBluetoothSettingsAMustBeEnabledOnTheNetworkResponse>($"networks/{networkId}/wireless/bluetooth/settings", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the Bluetooth settings for a network. See the docs page for <a href="https://documentation.meraki.com/MR/Bluetooth/Bluetooth_Low_Energy_(BLE)">Bluetooth settings</a>.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<UpdateTheBluetoothSettingsForANetworkResponse> UpdateTheBluetoothSettingsForANetwork(UpdateTheBluetoothSettingsForANetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateTheBluetoothSettingsForANetworkResponse>($"networks/{networkId}/wireless/bluetooth/settings", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the radio settings of a device
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<ReturnTheRadioSettingsOfADeviceResponse> ReturnTheRadioSettingsOfADevice(string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnTheRadioSettingsOfADeviceResponse>($"devices/{serial}/wireless/radio/settings", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the radio settings of a device
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// rfProfileId| string| The ID of an RF profile to assign to the device. If the value of this parameter is null, the appropriate basic RF profile (indoor or outdoor) will be assigned to the device. Assigning an RF profile will clear ALL manually configured overrides on the device (channel width, channel, power).
        /// twoFourGhzSettings| object| Manual radio settings for 2.4 GHz.
        /// fiveGhzSettings| object| Manual radio settings for 5 GHz.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/devices/{serial}/wireless/radio/settings` | update
        /// 
        /// 
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<UpdateTheRadioSettingsOfADeviceResponse> UpdateTheRadioSettingsOfADevice(UpdateTheRadioSettingsOfADeviceRequest request, string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateTheRadioSettingsOfADeviceResponse>($"devices/{serial}/wireless/radio/settings", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return alternate management interface and devices with IP assigned
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<TeManagementInterfaceAndDevicesWithIPAssignedResponse> TeManagementInterfaceAndDevicesWithIPAssigned(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<TeManagementInterfaceAndDevicesWithIPAssignedResponse>($"networks/{networkId}/wireless/alternateManagementInterface", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update alternate management interface and device static IP
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// enabled| boolean| Boolean value to enable or disable alternate management interface
        /// vlanId| integer| Alternate management interface VLAN, must be between 1 and 4094
        /// protocols| array| Can be one or more of the following values: 'radius', 'snmp', 'syslog' or 'ldap'
        /// accessPoints| array| Array of access point serial number and IP assignment. Note: accessPoints IP assignment is not applicable for template networks, in other words, do not put 'accessPoints' in the body when updating template networks. Also, an empty 'accessPoints' array will remove all previous static IP assignments
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/wireless/alternateManagementInterface` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<AlternateManagementInterfaceAndDeviceStaticIPResponse> AlternateManagementInterfaceAndDeviceStaticIP(AlternateManagementInterfaceAndDeviceStaticIPRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<AlternateManagementInterfaceAndDeviceStaticIPResponse>($"networks/{networkId}/wireless/alternateManagementInterface", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the billing settings of this network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ReturnTheBillingSettingsOfThisNetworkResponse> ReturnTheBillingSettingsOfThisNetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnTheBillingSettingsOfThisNetworkResponse>($"networks/{networkId}/wireless/billing", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the billing settings
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// currency| string| The currency code of this node group's billing plans
        /// plans| array| Array of billing plans in the node group. (Can configure a maximum of 5)
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/wireless/billing` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<UpdateTheBillingSettingsResponse> UpdateTheBillingSettings(UpdateTheBillingSettingsRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateTheBillingSettingsResponse>($"networks/{networkId}/wireless/billing", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List RF profiles for this network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ListRFprofilesforthisnetworkResponse> ListRFprofilesforthisnetwork(ListRFprofilesforthisnetworkParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/wireless/rfProfiles", parametersDict);
            return await _httpClient.GetFromJsonAsync<ListRFprofilesforthisnetworkResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the wireless settings for a network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ReturnTheWirelessSettingsForANetworkResponse> ReturnTheWirelessSettingsForANetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnTheWirelessSettingsForANetworkResponse>($"networks/{networkId}/wireless/settings", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the wireless settings for a network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// meshingEnabled| boolean| Toggle for enabling or disabling meshing in a network
        /// ipv6BridgeEnabled| boolean| Toggle for enabling or disabling IPv6 bridging in a network (Note: if enabled, SSIDs must also be configured to use bridge mode)
        /// locationAnalyticsEnabled| boolean| Toggle for enabling or disabling location analytics for your network
        /// upgradeStrategy| string| The upgrade strategy to apply to the network. Must be one of 'minimizeUpgradeTime' or 'minimizeClientDowntime'. Requires firmware version MR 26.8 or higher'
        /// ledLightsOn| boolean| Toggle for enabling or disabling LED lights on all APs in the network (making them run dark)
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/wireless/settings` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<UpdateTheWirelessSettingsForANetworkResponse> UpdateTheWirelessSettingsForANetwork(UpdateTheWirelessSettingsForANetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateTheWirelessSettingsForANetworkResponse>($"networks/{networkId}/wireless/settings", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the MR SSIDs in a network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ListTheMRSSIDsInANetworkResponse> ListTheMRSSIDsInANetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ListTheMRSSIDsInANetworkResponse>($"networks/{networkId}/wireless/ssids", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return a single MR SSID
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="number">(Required) Number</param>
        public async Task<ReturnASingleMRSSIDResponse> ReturnASingleMRSSID(string networkId, string number, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnASingleMRSSIDResponse>($"networks/{networkId}/wireless/ssids/{number}", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the attributes of an MR SSID
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| The name of the SSID
        /// enabled| boolean| Whether or not the SSID is enabled
        /// authMode| string| The association control method for the SSID ('open', 'open-enhanced', 'psk', 'open-with-radius', 'open-with-nac', '8021x-meraki', '8021x-nac', '8021x-radius', '8021x-google', '8021x-localradius', 'ipsk-with-radius' or 'ipsk-without-radius')
        /// enterpriseAdminAccess| string| Whether or not an SSID is accessible by 'enterprise' administrators ('access disabled' or 'access enabled')
        /// encryptionMode| string| The psk encryption mode for the SSID ('wep' or 'wpa'). This param is only valid if the authMode is 'psk'
        /// psk| string| The passkey for the SSID. This param is only valid if the authMode is 'psk'
        /// wpaEncryptionMode| string| The types of WPA encryption. ('WPA1 only', 'WPA1 and WPA2', 'WPA2 only', 'WPA3 Transition Mode', 'WPA3 only' or 'WPA3 192-bit Security')
        /// dot11w| object| The current setting for Protected Management Frames (802.11w).
        /// dot11r| object| The current setting for 802.11r
        /// splashPage| string| The type of splash page for the SSID ('None', 'Click-through splash page', 'Billing', 'Password-protected with Meraki RADIUS', 'Password-protected with custom RADIUS', 'Password-protected with Active Directory', 'Password-protected with LDAP', 'SMS authentication', 'Systems Manager Sentry', 'Facebook Wi-Fi', 'Google OAuth', 'Sponsored guest', 'Cisco ISE' or 'Google Apps domain'). This attribute is not supported for template children.
        /// splashGuestSponsorDomains| array| Array of valid sponsor email domains for sponsored guest splash type.
        /// oauth| object| The OAuth settings of this SSID. Only valid if splashPage is 'Google OAuth'.
        /// localRadius| object| The current setting for Local Authentication, a built-in RADIUS server on the access point. Only valid if authMode is '8021x-localradius'.
        /// ldap| object| The current setting for LDAP. Only valid if splashPage is 'Password-protected with LDAP'.
        /// activeDirectory| object| The current setting for Active Directory. Only valid if splashPage is 'Password-protected with Active Directory'
        /// radiusServers| array| The RADIUS 802.1X servers to be used for authentication. This param is only valid if the authMode is 'open-with-radius', '8021x-radius' or 'ipsk-with-radius'
        /// radiusProxyEnabled| boolean| If true, Meraki devices will proxy RADIUS messages through the Meraki cloud to the configured RADIUS auth and accounting servers.
        /// radiusTestingEnabled| boolean| If true, Meraki devices will periodically send Access-Request messages to configured RADIUS servers using identity 'meraki_8021x_test' to ensure that the RADIUS servers are reachable.
        /// radiusCalledStationId| string| The template of the called station identifier to be used for RADIUS (ex. $NODE_MAC$:$VAP_NUM$).
        /// radiusAuthenticationNasId| string| The template of the NAS identifier to be used for RADIUS authentication (ex. $NODE_MAC$:$VAP_NUM$).
        /// radiusServerTimeout| integer| The amount of time for which a RADIUS client waits for a reply from the RADIUS server (must be between 1-10 seconds).
        /// radiusServerAttemptsLimit| integer| The maximum number of transmit attempts after which a RADIUS server is failed over (must be between 1-5).
        /// radiusFallbackEnabled| boolean| Whether or not higher priority RADIUS servers should be retried after 60 seconds.
        /// radiusCoaEnabled| boolean| If true, Meraki devices will act as a RADIUS Dynamic Authorization Server and will respond to RADIUS Change-of-Authorization and Disconnect messages sent by the RADIUS server.
        /// radiusFailoverPolicy| string| This policy determines how authentication requests should be handled in the event that all of the configured RADIUS servers are unreachable ('Deny access' or 'Allow access')
        /// radiusLoadBalancingPolicy| string| This policy determines which RADIUS server will be contacted first in an authentication attempt and the ordering of any necessary retry attempts ('Strict priority order' or 'Round robin')
        /// radiusAccountingEnabled| boolean| Whether or not RADIUS accounting is enabled. This param is only valid if the authMode is 'open-with-radius', '8021x-radius' or 'ipsk-with-radius'
        /// radiusAccountingServers| array| The RADIUS accounting 802.1X servers to be used for authentication. This param is only valid if the authMode is 'open-with-radius', '8021x-radius' or 'ipsk-with-radius' and radiusAccountingEnabled is 'true'
        /// radiusAccountingInterimInterval| integer| The interval (in seconds) in which accounting information is updated and sent to the RADIUS accounting server.
        /// radiusAttributeForGroupPolicies| string| Specify the RADIUS attribute used to look up group policies ('Filter-Id', 'Reply-Message', 'Airespace-ACL-Name' or 'Aruba-User-Role'). Access points must receive this attribute in the RADIUS Access-Accept message
        /// ipAssignmentMode| string| The client IP assignment mode ('NAT mode', 'Bridge mode', 'Layer 3 roaming', 'Ethernet over GRE', 'Layer 3 roaming with a concentrator' or 'VPN')
        /// useVlanTagging| boolean| Whether or not traffic should be directed to use specific VLANs. This param is only valid if the ipAssignmentMode is 'Bridge mode' or 'Layer 3 roaming'
        /// concentratorNetworkId| string| The concentrator to use when the ipAssignmentMode is 'Layer 3 roaming with a concentrator' or 'VPN'.
        /// secondaryConcentratorNetworkId| string| The secondary concentrator to use when the ipAssignmentMode is 'VPN'. If configured, the APs will switch to using this concentrator if the primary concentrator is unreachable. This param is optional. ('disabled' represents no secondary concentrator.)
        /// disassociateClientsOnVpnFailover| boolean| Disassociate clients when 'VPN' concentrator failover occurs in order to trigger clients to re-associate and generate new DHCP requests. This param is only valid if ipAssignmentMode is 'VPN'.
        /// vlanId| integer| The VLAN ID used for VLAN tagging. This param is only valid when the ipAssignmentMode is 'Layer 3 roaming with a concentrator' or 'VPN'
        /// defaultVlanId| integer| The default VLAN ID used for 'all other APs'. This param is only valid when the ipAssignmentMode is 'Bridge mode' or 'Layer 3 roaming'
        /// apTagsAndVlanIds| array| The list of tags and VLAN IDs used for VLAN tagging. This param is only valid when the ipAssignmentMode is 'Bridge mode' or 'Layer 3 roaming'
        /// walledGardenEnabled| boolean| Allow access to a configurable list of IP ranges, which users may access prior to sign-on.
        /// walledGardenRanges| array| Specify your walled garden by entering an array of addresses, ranges using CIDR notation, domain names, and domain wildcards (e.g. '192.168.1.1/24', '192.168.37.10/32', 'www.yahoo.com', '*.google.com']). Meraki's splash page is automatically included in your walled garden.
        /// gre| object| Ethernet over GRE settings
        /// radiusOverride| boolean| If true, the RADIUS response can override VLAN tag. This is not valid when ipAssignmentMode is 'NAT mode'.
        /// radiusGuestVlanEnabled| boolean| Whether or not RADIUS Guest VLAN is enabled. This param is only valid if the authMode is 'open-with-radius' and addressing mode is not set to 'isolated' or 'nat' mode
        /// radiusGuestVlanId| integer| VLAN ID of the RADIUS Guest VLAN. This param is only valid if the authMode is 'open-with-radius' and addressing mode is not set to 'isolated' or 'nat' mode
        /// minBitrate| number| The minimum bitrate in Mbps of this SSID in the default indoor RF profile. ('1', '2', '5.5', '6', '9', '11', '12', '18', '24', '36', '48' or '54')
        /// bandSelection| string| The client-serving radio frequencies of this SSID in the default indoor RF profile. ('Dual band operation', '5 GHz band only' or 'Dual band operation with Band Steering')
        /// perClientBandwidthLimitUp| integer| The upload bandwidth limit in Kbps. (0 represents no limit.)
        /// perClientBandwidthLimitDown| integer| The download bandwidth limit in Kbps. (0 represents no limit.)
        /// perSsidBandwidthLimitUp| integer| The total upload bandwidth limit in Kbps. (0 represents no limit.)
        /// perSsidBandwidthLimitDown| integer| The total download bandwidth limit in Kbps. (0 represents no limit.)
        /// lanIsolationEnabled| boolean| Boolean indicating whether Layer 2 LAN isolation should be enabled or disabled. Only configurable when ipAssignmentMode is 'Bridge mode'.
        /// visible| boolean| Boolean indicating whether APs should advertise or hide this SSID. APs will only broadcast this SSID if set to true
        /// availableOnAllAps| boolean| Boolean indicating whether all APs should broadcast the SSID or if it should be restricted to APs matching any availability tags. Can only be false if the SSID has availability tags.
        /// availabilityTags| array| Accepts a list of tags for this SSID. If availableOnAllAps is false, then the SSID will only be broadcast by APs with tags matching any of the tags in this list.
        /// mandatoryDhcpEnabled| boolean| If true, Mandatory DHCP will enforce that clients connecting to this SSID must use the IP address assigned by the DHCP server. Clients who use a static IP address won't be able to associate.
        /// adultContentFilteringEnabled| boolean| Boolean indicating whether or not adult content will be blocked
        /// dnsRewrite| object| DNS servers rewrite settings
        /// speedBurst| object| The SpeedBurst setting for this SSID'
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/wireless/ssids/{number}` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="number">(Required) Number</param>
        public async Task<UpdateTheAttributesOfAnMRSSIDResponse> UpdateTheAttributesOfAnMRSSID(UpdateTheAttributesOfAnMRSSIDRequest request, string networkId, string number, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateTheAttributesOfAnMRSSIDResponse>($"networks/{networkId}/wireless/ssids/{number}", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Aggregated connectivity info for a given AP on this network
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<GatedConnectivityInfoForAGivenAPOnThisNetworkResponse> GatedConnectivityInfoForAGivenAPOnThisNetwork(GatedConnectivityInfoForAGivenAPOnThisNetworkParameters queryParameters, string serial, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"devices/{serial}/wireless/connectionStats", parametersDict);
            return await _httpClient.GetFromJsonAsync<GatedConnectivityInfoForAGivenAPOnThisNetworkResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Aggregated connectivity info for this network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<AggregatedConnectivityInfoForThisNetworkResponse> AggregatedConnectivityInfoForThisNetwork(AggregatedConnectivityInfoForThisNetworkParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/wireless/connectionStats", parametersDict);
            return await _httpClient.GetFromJsonAsync<AggregatedConnectivityInfoForThisNetworkResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Aggregated latency info for a given AP on this network
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<AggregatedLatencyInfoForAGivenAPOnThisNetworkResponse> AggregatedLatencyInfoForAGivenAPOnThisNetwork(AggregatedLatencyInfoForAGivenAPOnThisNetworkParameters queryParameters, string serial, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"devices/{serial}/wireless/latencyStats", parametersDict);
            return await _httpClient.GetFromJsonAsync<AggregatedLatencyInfoForAGivenAPOnThisNetworkResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Aggregated latency info for this network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<AggregatedLatencyInfoForThisNetworkResponse> AggregatedLatencyInfoForThisNetwork(AggregatedLatencyInfoForThisNetworkParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/wireless/latencyStats", parametersDict);
            return await _httpClient.GetFromJsonAsync<AggregatedLatencyInfoForThisNetworkResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the SSID statuses of an access point
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<ReturntheSSIDstatusesofanaccesspointResponse> ReturntheSSIDstatusesofanaccesspoint(string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturntheSSIDstatusesofanaccesspointResponse>($"devices/{serial}/wireless/status", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List Air Marshal scan results from a network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ListAirMarshalscanresultsfromanetworkResponse> ListAirMarshalscanresultsfromanetwork(ListAirMarshalscanresultsfromanetworkParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/wireless/airMarshal", parametersDict);
            return await _httpClient.GetFromJsonAsync<ListAirMarshalscanresultsfromanetworkResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return AP channel utilization over time for a device or network client
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<LutilizationovertimeforadeviceornetworkclientResponse> Lutilizationovertimeforadeviceornetworkclient(LutilizationovertimeforadeviceornetworkclientParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/wireless/channelUtilizationHistory", parametersDict);
            return await _httpClient.GetFromJsonAsync<LutilizationovertimeforadeviceornetworkclientResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return wireless client counts over time for a network, device, or network client
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<OuntsovertimeforanetworkdeviceornetworkclientResponse> Ountsovertimeforanetworkdeviceornetworkclient(OuntsovertimeforanetworkdeviceornetworkclientParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/wireless/clientCountHistory", parametersDict);
            return await _httpClient.GetFromJsonAsync<OuntsovertimeforanetworkdeviceornetworkclientResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Aggregated connectivity info for this network, grouped by clients
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<OnnectivityInfoForThisNetworkGroupedByClientsResponse> OnnectivityInfoForThisNetworkGroupedByClients(OnnectivityInfoForThisNetworkGroupedByClientsParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/wireless/clients/connectionStats", parametersDict);
            return await _httpClient.GetFromJsonAsync<OnnectivityInfoForThisNetworkGroupedByClientsResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Aggregated connectivity info for a given client on this network. Clients are identified by their MAC.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="clientId">(Required) Client ID</param>
        public async Task<DConnectivityInfoForAGivenClientOnThisNetworkResponse> DConnectivityInfoForAGivenClientOnThisNetwork(DConnectivityInfoForAGivenClientOnThisNetworkParameters queryParameters, string networkId, string clientId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/wireless/clients/{clientId}/connectionStats", parametersDict);
            return await _httpClient.GetFromJsonAsync<DConnectivityInfoForAGivenClientOnThisNetworkResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Aggregated latency info for this network, grouped by clients
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<AtedLatencyInfoForThisNetworkGroupedByClientsResponse> AtedLatencyInfoForThisNetworkGroupedByClients(AtedLatencyInfoForThisNetworkGroupedByClientsParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/wireless/clients/latencyStats", parametersDict);
            return await _httpClient.GetFromJsonAsync<AtedLatencyInfoForThisNetworkGroupedByClientsResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Aggregated latency info for a given client on this network. Clients are identified by their MAC.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="clientId">(Required) Client ID</param>
        public async Task<EgatedLatencyInfoForAGivenClientOnThisNetworkResponse> EgatedLatencyInfoForAGivenClientOnThisNetwork(EgatedLatencyInfoForAGivenClientOnThisNetworkParameters queryParameters, string networkId, string clientId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/wireless/clients/{clientId}/latencyStats", parametersDict);
            return await _httpClient.GetFromJsonAsync<EgatedLatencyInfoForAGivenClientOnThisNetworkResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the wireless connectivity events for a client within a network in the timespan.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="clientId">(Required) Client ID</param>
        public async Task<TyeventsforaclientwithinanetworkinthetimespanResponse> Tyeventsforaclientwithinanetworkinthetimespan(TyeventsforaclientwithinanetworkinthetimespanParameters queryParameters, string networkId, string clientId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/wireless/clients/{clientId}/connectivityEvents", parametersDict);
            return await _httpClient.GetFromJsonAsync<TyeventsforaclientwithinanetworkinthetimespanResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the latency history for a client. Clients can be identified by a client key or either the MAC or IP depending on whether the network uses Track-by-IP. The latency data is from a sample of 2% of packets and is grouped into 4 traffic categories: background, best effort, video, voice. Within these categories the sampled packet counters are bucketed by latency in milliseconds.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="clientId">(Required) Client ID</param>
        public async Task<ReturnthelatencyhistoryforaclientResponse> Returnthelatencyhistoryforaclient(ReturnthelatencyhistoryforaclientParameters queryParameters, string networkId, string clientId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/wireless/clients/{clientId}/latencyHistory", parametersDict);
            return await _httpClient.GetFromJsonAsync<ReturnthelatencyhistoryforaclientResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return PHY data rates over time for a network, device, or network client
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<RatesovertimeforanetworkdeviceornetworkclientResponse> Ratesovertimeforanetworkdeviceornetworkclient(RatesovertimeforanetworkdeviceornetworkclientParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/wireless/dataRateHistory", parametersDict);
            return await _httpClient.GetFromJsonAsync<RatesovertimeforanetworkdeviceornetworkclientResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Aggregated connectivity info for this network, grouped by node
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<EdconnectivityinfoforthisnetworkgroupedbynodeResponse> Edconnectivityinfoforthisnetworkgroupedbynode(EdconnectivityinfoforthisnetworkgroupedbynodeParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/wireless/devices/connectionStats", parametersDict);
            return await _httpClient.GetFromJsonAsync<EdconnectivityinfoforthisnetworkgroupedbynodeResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Aggregated latency info for this network, grouped by node
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<RegatedlatencyinfoforthisnetworkgroupedbynodeResponse> Regatedlatencyinfoforthisnetworkgroupedbynode(RegatedlatencyinfoforthisnetworkgroupedbynodeParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/wireless/devices/latencyStats", parametersDict);
            return await _httpClient.GetFromJsonAsync<RegatedlatencyinfoforthisnetworkgroupedbynodeResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Get average channel utilization for all bands in a network, split by AP
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<NnelutilizationforallbandsinanetworksplitbyAPResponse> NnelutilizationforallbandsinanetworksplitbyAP(NnelutilizationforallbandsinanetworksplitbyAPParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/wireless/devices/channelUtilization/byDevice", parametersDict);
            return await _httpClient.GetFromJsonAsync<NnelutilizationforallbandsinanetworksplitbyAPResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Get average channel utilization across all bands for all networks in the organization
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<AcrossallbandsforallnetworksintheorganizationResponse> Acrossallbandsforallnetworksintheorganization(AcrossallbandsforallnetworksintheorganizationParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/wireless/devices/channelUtilization/byNetwork", parametersDict);
            return await _httpClient.GetFromJsonAsync<AcrossallbandsforallnetworksintheorganizationResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Get a time-series of average channel utilization for all bands, segmented by device.
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<HannelutilizationforallbandssegmentedbydeviceResponse> Hannelutilizationforallbandssegmentedbydevice(HannelutilizationforallbandssegmentedbydeviceParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/wireless/devices/channelUtilization/history/byDevice/byInterval", parametersDict);
            return await _httpClient.GetFromJsonAsync<HannelutilizationforallbandssegmentedbydeviceResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Get a time-series of average channel utilization for all bands
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<EseriesofaveragechannelutilizationforallbandsResponse> Eseriesofaveragechannelutilizationforallbands(EseriesofaveragechannelutilizationforallbandsParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/wireless/devices/channelUtilization/history/byNetwork/byInterval", parametersDict);
            return await _httpClient.GetFromJsonAsync<EseriesofaveragechannelutilizationforallbandsResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Endpoint to see power status for wireless devices
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<EndpointtoseepowerstatusforwirelessdevicesResponse> Endpointtoseepowerstatusforwirelessdevices(EndpointtoseepowerstatusforwirelessdevicesParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/wireless/devices/ethernet/statuses", parametersDict);
            return await _httpClient.GetFromJsonAsync<EndpointtoseepowerstatusforwirelessdevicesResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List of all failed client connection events on this network in a given time range
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<OnnectioneventsonthisnetworkinagiventimerangeResponse> Onnectioneventsonthisnetworkinagiventimerange(OnnectioneventsonthisnetworkinagiventimerangeParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/wireless/failedConnections", parametersDict);
            return await _httpClient.GetFromJsonAsync<OnnectioneventsonthisnetworkinagiventimerangeResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return average wireless latency over time for a network, device, or network client
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<TencyovertimeforanetworkdeviceornetworkclientResponse> Tencyovertimeforanetworkdeviceornetworkclient(TencyovertimeforanetworkdeviceornetworkclientParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/wireless/latencyHistory", parametersDict);
            return await _httpClient.GetFromJsonAsync<TencyovertimeforanetworkdeviceornetworkclientResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List wireless mesh statuses for repeaters
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ListwirelessmeshstatusesforrepeatersResponse> Listwirelessmeshstatusesforrepeaters(ListwirelessmeshstatusesforrepeatersParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/wireless/meshStatuses", parametersDict);
            return await _httpClient.GetFromJsonAsync<ListwirelessmeshstatusesforrepeatersResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return signal quality (SNR/RSSI) over time for a device or network client
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<AlitySNRRSSIovertimeforadeviceornetworkclientResponse> AlitySNRRSSIovertimeforadeviceornetworkclient(AlitySNRRSSIovertimeforadeviceornetworkclientParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/wireless/signalQualityHistory", parametersDict);
            return await _httpClient.GetFromJsonAsync<AlitySNRRSSIovertimeforadeviceornetworkclientResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return AP usage over time for a device or network client
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<EturnAPusageovertimeforadeviceornetworkclientResponse> EturnAPusageovertimeforadeviceornetworkclient(EturnAPusageovertimeforadeviceornetworkclientParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/wireless/usageHistory", parametersDict);
            return await _httpClient.GetFromJsonAsync<EturnAPusageovertimeforadeviceornetworkclientResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Creates new RF profile for this network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| The name of the new profile. Must be unique. This param is required on creation.
        /// clientBalancingEnabled| boolean| Steers client to best available access point. Can be either true or false. Defaults to true.
        /// minBitrateType| string| Minimum bitrate can be set to either 'band' or 'ssid'. Defaults to band.
        /// bandSelectionType| string| Band selection can be set to either 'ssid' or 'ap'. This param is required on creation.
        /// apBandSettings| object| Settings that will be enabled if selectionType is set to 'ap'.
        /// twoFourGhzSettings| object| Settings related to 2.4Ghz band
        /// fiveGhzSettings| object| Settings related to 5Ghz band
        /// sixGhzSettings| object| Settings related to 6Ghz band. Only applicable to networks with 6Ghz capable APs
        /// transmission| object| Settings related to radio transmission.
        /// perSsidSettings| object| Per-SSID radio settings by number.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/wireless/rfProfiles` | create
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<Stream> CreatesNewRFProfileForThisNetwork(CreatesNewRFProfileForThisNetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"networks/{networkId}/appliance/rfProfiles", request, cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Updates specified RF profile for this network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| The name of the new profile. Must be unique.
        /// clientBalancingEnabled| boolean| Steers client to best available access point. Can be either true or false.
        /// minBitrateType| string| Minimum bitrate can be set to either 'band' or 'ssid'.
        /// bandSelectionType| string| Band selection can be set to either 'ssid' or 'ap'.
        /// apBandSettings| object| Settings that will be enabled if selectionType is set to 'ap'.
        /// twoFourGhzSettings| object| Settings related to 2.4Ghz band
        /// fiveGhzSettings| object| Settings related to 5Ghz band
        /// sixGhzSettings| object| Settings related to 6Ghz band. Only applicable to networks with 6Ghz capable APs
        /// transmission| object| Settings related to radio transmission.
        /// perSsidSettings| object| Per-SSID radio settings by number.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/wireless/rfProfiles/{rfProfileId}` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="rfProfileId">(Required) Rf profile ID</param>
        public async Task<UpdatesSpecifiedRFProfileForThisNetworkResponse> UpdatesSpecifiedRFProfileForThisNetwork(UpdatesSpecifiedRFProfileForThisNetworkRequest request, string networkId, string rfProfileId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdatesSpecifiedRFProfileForThisNetworkResponse>($"networks/{networkId}/appliance/rfProfiles/{rfProfileId}", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Delete a RF Profile
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/wireless/rfProfiles/{rfProfileId}` | destroy
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="rfProfileId">(Required) Rf profile ID</param>
        public async Task<Stream> DeleteARFProfile(string networkId, string rfProfileId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"networks/{networkId}/appliance/rfProfiles/{rfProfileId}", cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Return a RF profile
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="rfProfileId">(Required) Rf profile ID</param>
        public async Task<ReturnARFProfileResponse> ReturnARFProfile(string networkId, string rfProfileId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnARFProfileResponse>($"networks/{networkId}/appliance/rfProfiles/{rfProfileId}", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Creates new RF profile for this network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| The name of the new profile. Must be unique. This param is required on creation.
        /// clientBalancingEnabled| boolean| Steers client to best available access point. Can be either true or false. Defaults to true.
        /// minBitrateType| string| Minimum bitrate can be set to either 'band' or 'ssid'. Defaults to band.
        /// bandSelectionType| string| Band selection can be set to either 'ssid' or 'ap'. This param is required on creation.
        /// apBandSettings| object| Settings that will be enabled if selectionType is set to 'ap'.
        /// twoFourGhzSettings| object| Settings related to 2.4Ghz band
        /// fiveGhzSettings| object| Settings related to 5Ghz band
        /// sixGhzSettings| object| Settings related to 6Ghz band. Only applicable to networks with 6Ghz capable APs
        /// transmission| object| Settings related to radio transmission.
        /// perSsidSettings| object| Per-SSID radio settings by number.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/wireless/rfProfiles` | create
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<Stream> CreatesNewRFProfileForThisNetwork2(CreatesNewRFProfileForThisNetwork2Request request, string networkId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"networks/{networkId}/wireless/rfProfiles", request, cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Updates specified RF profile for this network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| The name of the new profile. Must be unique.
        /// clientBalancingEnabled| boolean| Steers client to best available access point. Can be either true or false.
        /// minBitrateType| string| Minimum bitrate can be set to either 'band' or 'ssid'.
        /// bandSelectionType| string| Band selection can be set to either 'ssid' or 'ap'.
        /// apBandSettings| object| Settings that will be enabled if selectionType is set to 'ap'.
        /// twoFourGhzSettings| object| Settings related to 2.4Ghz band
        /// fiveGhzSettings| object| Settings related to 5Ghz band
        /// sixGhzSettings| object| Settings related to 6Ghz band. Only applicable to networks with 6Ghz capable APs
        /// transmission| object| Settings related to radio transmission.
        /// perSsidSettings| object| Per-SSID radio settings by number.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/wireless/rfProfiles/{rfProfileId}` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="rfProfileId">(Required) Rf profile ID</param>
        public async Task<UpdatesSpecifiedRFProfileForThisNetwork2Response> UpdatesSpecifiedRFProfileForThisNetwork2(UpdatesSpecifiedRFProfileForThisNetwork2Request request, string networkId, string rfProfileId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdatesSpecifiedRFProfileForThisNetwork2Response>($"networks/{networkId}/wireless/rfProfiles/{rfProfileId}", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Delete a RF Profile
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/wireless/rfProfiles/{rfProfileId}` | destroy
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="rfProfileId">(Required) Rf profile ID</param>
        public async Task<Stream> DeleteARFProfile2(string networkId, string rfProfileId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"networks/{networkId}/wireless/rfProfiles/{rfProfileId}", cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Return a RF profile
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="rfProfileId">(Required) Rf profile ID</param>
        public async Task<ReturnARFProfile2Response> ReturnARFProfile2(string networkId, string rfProfileId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnARFProfile2Response>($"networks/{networkId}/wireless/rfProfiles/{rfProfileId}", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Get application health by time
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="applicationId">(Required) Application ID</param>
        public async Task<GetapplicationhealthbytimeResponse> Getapplicationhealthbytime(GetapplicationhealthbytimeParameters queryParameters, string networkId, string applicationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/insight/applications/{applicationId}/healthByTime", parametersDict);
            return await _httpClient.GetFromJsonAsync<GetapplicationhealthbytimeResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List all Insight tracked applications
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<ListallInsighttrackedapplicationsResponse> ListallInsighttrackedapplications(string organizationId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ListallInsighttrackedapplicationsResponse>($"organizations/{organizationId}/insight/applications", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the monitored media servers for this organization. Only valid for organizations with Meraki Insight.
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<StTheMonitoredMediaServersForThisOrganizationResponse> StTheMonitoredMediaServersForThisOrganization(string organizationId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<StTheMonitoredMediaServersForThisOrganizationResponse>($"organizations/{organizationId}/insight/monitoredMediaServers", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Add a media server to be monitored for this organization. Only valid for organizations with Meraki Insight.
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<Stream> DAMediaServerToBeMonitoredForThisOrganization(DAMediaServerToBeMonitoredForThisOrganizationRequest request, string organizationId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"organizations/{organizationId}/insight/monitoredMediaServers", request, cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Return a monitored media server for this organization. Only valid for organizations with Meraki Insight.
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        /// <param name="monitoredMediaServerId">(Required) Monitored media server ID</param>
        public async Task<EturnAMonitoredMediaServerForThisOrganizationResponse> EturnAMonitoredMediaServerForThisOrganization(string organizationId, string monitoredMediaServerId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<EturnAMonitoredMediaServerForThisOrganizationResponse>($"organizations/{organizationId}/insight/monitoredMediaServers/{monitoredMediaServerId}", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update a monitored media server for this organization. Only valid for organizations with Meraki Insight.
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        /// <param name="monitoredMediaServerId">(Required) Monitored media server ID</param>
        public async Task<PdateAMonitoredMediaServerForThisOrganizationResponse> PdateAMonitoredMediaServerForThisOrganization(PdateAMonitoredMediaServerForThisOrganizationRequest request, string organizationId, string monitoredMediaServerId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<PdateAMonitoredMediaServerForThisOrganizationResponse>($"organizations/{organizationId}/insight/monitoredMediaServers/{monitoredMediaServerId}", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Delete a monitored media server from this organization. Only valid for organizations with Meraki Insight.
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        /// <param name="monitoredMediaServerId">(Required) Monitored media server ID</param>
        public async Task<Stream> LeteAMonitoredMediaServerFromThisOrganization(string organizationId, string monitoredMediaServerId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"organizations/{organizationId}/insight/monitoredMediaServers/{monitoredMediaServerId}", cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Bypass activation lock attempt
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// ids| array| The ids of the devices to attempt activation lock bypass.
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<Stream> BypassActivationLockAttempt(BypassActivationLockAttemptRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"networks/{networkId}/sm/bypassActivationLockAttempts", request, cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Bypass activation lock attempt status
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="attemptId">(Required) Attempt ID</param>
        public async Task<BypassActivationLockAttemptStatusResponse> BypassActivationLockAttemptStatus(string networkId, string attemptId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<BypassActivationLockAttemptStatusResponse>($"networks/{networkId}/sm/bypassActivationLockAttempts/{attemptId}", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the devices enrolled in an SM network with various specified fields and filters
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<SMNetworkWithVariousSpecifiedFieldsAndFiltersResponse> SMNetworkWithVariousSpecifiedFieldsAndFilters(SMNetworkWithVariousSpecifiedFieldsAndFiltersParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/sm/devices", parametersDict);
            return await _httpClient.GetFromJsonAsync<SMNetworkWithVariousSpecifiedFieldsAndFiltersResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Force check-in a set of devices
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// wifiMacs| array| The wifiMacs of the devices to be checked-in.
        /// ids| array| The ids of the devices to be checked-in.
        /// serials| array| The serials of the devices to be checked-in.
        /// scope| array| The scope (one of all, none, withAny, withAll, withoutAny, or withoutAll) and a set of tags of the devices to be checked-in.
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ForceCheckInASetOfDevicesResponse> ForceCheckInASetOfDevices(ForceCheckInASetOfDevicesRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PostJsonAsync<ForceCheckInASetOfDevicesResponse>($"networks/{networkId}/sm/devices/checkin", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Lock a set of devices
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// wifiMacs| array| The wifiMacs of the devices to be locked.
        /// ids| array| The ids of the devices to be locked.
        /// serials| array| The serials of the devices to be locked.
        /// scope| array| The scope (one of all, none, withAny, withAll, withoutAny, or withoutAll) and a set of tags of the devices to be wiped.
        /// pin| integer| The pin number for locking macOS devices (a six digit number). Required only for macOS devices.
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<LockASetOfDevicesResponse> LockASetOfDevices(LockASetOfDevicesRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PostJsonAsync<LockASetOfDevicesResponse>($"networks/{networkId}/sm/devices/lock", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Add, delete, or update the tags of a set of devices
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// wifiMacs| array| The wifiMacs of the devices to be modified.
        /// ids| array| The ids of the devices to be modified.
        /// serials| array| The serials of the devices to be modified.
        /// scope| array| The scope (one of all, none, withAny, withAll, withoutAny, or withoutAll) and a set of tags of the devices to be modified.
        /// tags| array| The tags to be added, deleted, or updated.
        /// updateAction| string| One of add, delete, or update. Only devices that have been modified will be returned.
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<AddDeleteOrUpdateTheTagsOfASetOfDevicesResponse> AddDeleteOrUpdateTheTagsOfASetOfDevices(AddDeleteOrUpdateTheTagsOfASetOfDevicesRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PostJsonAsync<AddDeleteOrUpdateTheTagsOfASetOfDevicesResponse>($"networks/{networkId}/sm/devices/modifyTags", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Move a set of devices to a new network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// wifiMacs| array| The wifiMacs of the devices to be moved.
        /// ids| array| The ids of the devices to be moved.
        /// serials| array| The serials of the devices to be moved.
        /// scope| array| The scope (one of all, none, withAny, withAll, withoutAny, or withoutAll) and a set of tags of the devices to be moved.
        /// newNetwork| string| The new network to which the devices will be moved.
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<MoveASetOfDevicesToANewNetworkResponse> MoveASetOfDevicesToANewNetwork(MoveASetOfDevicesToANewNetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PostJsonAsync<MoveASetOfDevicesToANewNetworkResponse>($"networks/{networkId}/sm/devices/move", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Wipe a device
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// wifiMac| string| The wifiMac of the device to be wiped.
        /// id| string| The id of the device to be wiped.
        /// serial| string| The serial of the device to be wiped.
        /// pin| integer| The pin number (a six digit value) for wiping a macOS device. Required only for macOS devices.
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<WipeADeviceResponse> WipeADevice(WipeADeviceRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PostJsonAsync<WipeADeviceResponse>($"networks/{networkId}/sm/devices/wipe", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Refresh the details of a device
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="deviceId">(Required) Device ID</param>
        public async Task<EmptyResponse> RefreshTheDetailsOfADevice(string networkId, string deviceId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PostFromJsonAsync<EmptyResponse>($"networks/{networkId}/sm/devices/{deviceId}/refreshDetails", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Unenroll a device
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="deviceId">(Required) Device ID</param>
        public async Task<UnenrollADeviceResponse> UnenrollADevice(string networkId, string deviceId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PostFromJsonAsync<UnenrollADeviceResponse>($"networks/{networkId}/sm/devices/{deviceId}/unenroll", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List all profiles in a network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ListallprofilesinanetworkResponse> Listallprofilesinanetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ListallprofilesinanetworkResponse>($"networks/{networkId}/sm/profiles", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the target groups in this network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ListTheTargetGroupsInThisNetworkResponse> ListTheTargetGroupsInThisNetwork(ListTheTargetGroupsInThisNetworkParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/sm/targetGroups", parametersDict);
            return await _httpClient.GetFromJsonAsync<ListTheTargetGroupsInThisNetworkResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Add a target group
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| The name of this target group
        /// scope| string| The scope and tag options of the target group. Comma separated values beginning with one of withAny, withAll, withoutAny, withoutAll, all, none, followed by tags. Default to none if empty.
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<Stream> AddATargetGroup(AddATargetGroupRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"networks/{networkId}/sm/targetGroups", request, cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Return a target group
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="targetGroupId">(Required) Target group ID</param>
        public async Task<ReturnATargetGroupResponse> ReturnATargetGroup(ReturnATargetGroupParameters queryParameters, string networkId, string targetGroupId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/sm/targetGroups/{targetGroupId}", parametersDict);
            return await _httpClient.GetFromJsonAsync<ReturnATargetGroupResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update a target group
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| The name of this target group
        /// scope| string| The scope and tag options of the target group. Comma separated values beginning with one of withAny, withAll, withoutAny, withoutAll, all, none, followed by tags. Default to none if empty.
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="targetGroupId">(Required) Target group ID</param>
        public async Task<UpdateATargetGroupResponse> UpdateATargetGroup(UpdateATargetGroupRequest request, string networkId, string targetGroupId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateATargetGroupResponse>($"networks/{networkId}/sm/targetGroups/{targetGroupId}", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Delete a target group from a network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="targetGroupId">(Required) Target group ID</param>
        public async Task<Stream> DeleteATargetGroupFromANetwork(string networkId, string targetGroupId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"networks/{networkId}/sm/targetGroups/{targetGroupId}", cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// List Trusted Access Configs
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ListTrustedAccessConfigsResponse> ListTrustedAccessConfigs(ListTrustedAccessConfigsParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/sm/trustedAccessConfigs", parametersDict);
            return await _httpClient.GetFromJsonAsync<ListTrustedAccessConfigsResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List User Access Devices and its Trusted Access Connections
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ErAccessDevicesAndItsTrustedAccessConnectionsResponse> ErAccessDevicesAndItsTrustedAccessConnections(ErAccessDevicesAndItsTrustedAccessConnectionsParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/sm/userAccessDevices", parametersDict);
            return await _httpClient.GetFromJsonAsync<ErAccessDevicesAndItsTrustedAccessConnectionsResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Delete a User Access Device
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}/sm/userAccessDevices/{userAccessDeviceId}` | destroy
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="userAccessDeviceId">(Required) User access device ID</param>
        public async Task<Stream> DeleteAUserAccessDevice(string networkId, string userAccessDeviceId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"networks/{networkId}/sm/userAccessDevices/{userAccessDeviceId}", cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// List the owners in an SM network with various specified fields and filters
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<SMNetworkWithVariousSpecifiedFieldsAndFilters2Response> SMNetworkWithVariousSpecifiedFieldsAndFilters2(SMNetworkWithVariousSpecifiedFieldsAndFilters2Parameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/sm/users", parametersDict);
            return await _httpClient.GetFromJsonAsync<SMNetworkWithVariousSpecifiedFieldsAndFilters2Response>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Get the organization's APNS certificate
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<GettheorganizationsAPNScertificateResponse> GettheorganizationsAPNScertificate(string organizationId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<GettheorganizationsAPNScertificateResponse>($"organizations/{organizationId}/sm/apnsCert", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the VPP accounts in the organization
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<ListTheVPPAccountsInTheOrganizationResponse> ListTheVPPAccountsInTheOrganization(string organizationId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ListTheVPPAccountsInTheOrganizationResponse>($"organizations/{organizationId}/sm/vppAccounts", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Get a hash containing the unparsed token of the VPP account with the given ID
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        /// <param name="vppAccountId">(Required) Vpp account ID</param>
        public async Task<TheUnparsedTokenOfTheVPPAccountWithTheGivenIDResponse> TheUnparsedTokenOfTheVPPAccountWithTheGivenID(string organizationId, string vppAccountId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<TheUnparsedTokenOfTheVPPAccountWithTheGivenIDResponse>($"organizations/{organizationId}/sm/vppAccounts/{vppAccountId}", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the client's daily cellular data usage history. Usage data is in kilobytes.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="deviceId">(Required) Device ID</param>
        public async Task<ReturntheclientsdailycellulardatausagehistoryResponse> Returntheclientsdailycellulardatausagehistory(string networkId, string deviceId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturntheclientsdailycellulardatausagehistoryResponse>($"networks/{networkId}/sm/devices/{deviceId}/cellularUsageHistory", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Returns historical connectivity data (whether a device is regularly checking in to Dashboard).
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="deviceId">(Required) Device ID</param>
        public async Task<HetheradeviceisregularlycheckingintoDashboardResponse> HetheradeviceisregularlycheckingintoDashboard(HetheradeviceisregularlycheckingintoDashboardParameters queryParameters, string networkId, string deviceId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/sm/devices/{deviceId}/connectivity", parametersDict);
            return await _httpClient.GetFromJsonAsync<HetheradeviceisregularlycheckingintoDashboardResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return historical records of various Systems Manager network connection details for desktop devices.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="deviceId">(Required) Device ID</param>
        public async Task<AgernetworkconnectiondetailsfordesktopdevicesResponse> Agernetworkconnectiondetailsfordesktopdevices(AgernetworkconnectiondetailsfordesktopdevicesParameters queryParameters, string networkId, string deviceId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/sm/devices/{deviceId}/desktopLogs", parametersDict);
            return await _httpClient.GetFromJsonAsync<AgernetworkconnectiondetailsfordesktopdevicesResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return historical records of commands sent to Systems Manager devices. Note that this will include the name of the Dashboard user who initiated the command if it was generated by a Dashboard admin rather than the automatic behavior of the system; you may wish to filter this out of any reports.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="deviceId">(Required) Device ID</param>
        public async Task<LrecordsofcommandssenttoSystemsManagerdevicesResponse> LrecordsofcommandssenttoSystemsManagerdevices(LrecordsofcommandssenttoSystemsManagerdevicesParameters queryParameters, string networkId, string deviceId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/sm/devices/{deviceId}/deviceCommandLogs", parametersDict);
            return await _httpClient.GetFromJsonAsync<LrecordsofcommandssenttoSystemsManagerdevicesResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return historical records of various Systems Manager client metrics for desktop devices.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="deviceId">(Required) Device ID</param>
        public async Task<SSystemsManagerclientmetricsfordesktopdevicesResponse> SSystemsManagerclientmetricsfordesktopdevices(SSystemsManagerclientmetricsfordesktopdevicesParameters queryParameters, string networkId, string deviceId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/sm/devices/{deviceId}/performanceHistory", parametersDict);
            return await _httpClient.GetFromJsonAsync<SSystemsManagerclientmetricsfordesktopdevicesResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the licenses in a coterm organization
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<ListTheLicensesInACotermOrganizationResponse> ListTheLicensesInACotermOrganization(ListTheLicensesInACotermOrganizationParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/licensing/coterm/licenses", parametersDict);
            return await _httpClient.GetFromJsonAsync<ListTheLicensesInACotermOrganizationResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Moves a license to a different organization (coterm only)
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// destination| object| Destination data for the license move
        /// licenses| array| The list of licenses to move
        /// 
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<VesALicenseToADifferentOrganizationCotermOnlyResponse> VesALicenseToADifferentOrganizationCotermOnly(VesALicenseToADifferentOrganizationCotermOnlyRequest request, string organizationId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PostJsonAsync<VesALicenseToADifferentOrganizationCotermOnlyResponse>($"organizations/{organizationId}/licensing/coterm/licenses/move", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return a single device
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<ReturnASingleDeviceResponse> ReturnASingleDevice(string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnASingleDeviceResponse>($"devices/{serial}", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update the attributes of a device
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| The name of a device
        /// tags| array| The list of tags of a device
        /// lat| number| The latitude of a device
        /// lng| number| The longitude of a device
        /// address| string| The address of a device
        /// notes| string| The notes for the device. String. Limited to 255 characters.
        /// moveMapMarker| boolean| Whether or not to set the latitude and longitude of a device based on the new address. Only applies when lat and lng are not specified.
        /// switchProfileId| string| The ID of a switch profile to bind to the device (for available switch profiles, see the 'Switch Profiles' endpoint). Use null to unbind the switch device from the current profile. For a device to be bindable to a switch profile, it must (1) be a switch, and (2) belong to a network that is bound to a configuration template.
        /// floorPlanId| string| The floor plan to associate to this device. null disassociates the device from the floorplan.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/devices/{serial}` | update
        /// 
        /// 
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<UpdateTheAttributesOfADeviceResponse> UpdateTheAttributesOfADevice(UpdateTheAttributesOfADeviceRequest request, string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateTheAttributesOfADeviceResponse>($"devices/{serial}", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Blink the LEDs on a device
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// duration| integer| The duration in seconds. Must be between 5 and 120. Default is 20 seconds
        /// period| integer| The period in milliseconds. Must be between 100 and 1000. Default is 160 milliseconds
        /// duty| integer| The duty cycle as the percent active. Must be between 10 and 90. Default is 50.
        /// 
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<Stream> BlinkTheLEDsOnADevice(BlinkTheLEDsOnADeviceRequest request, string serial, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"devices/{serial}/blinkLeds", request, cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Reboot a device
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<Stream> RebootADevice(string serial, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsync($"devices/{serial}/reboot", cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// List the clients of a device, up to a maximum of a month ago. The usage of each client is returned in kilobytes. If the device is a switch, the switchport is returned; otherwise the switchport field is null.
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<IsttheclientsofadeviceuptoamaximumofamonthagoResponse> Isttheclientsofadeviceuptoamaximumofamonthago(IsttheclientsofadeviceuptoamaximumofamonthagoParameters queryParameters, string serial, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"devices/{serial}/clients", parametersDict);
            return await _httpClient.GetFromJsonAsync<IsttheclientsofadeviceuptoamaximumofamonthagoResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List LLDP and CDP information for a device
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<ListLLDPandCDPinformationforadeviceResponse> ListLLDPandCDPinformationforadevice(string serial, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ListLLDPandCDPinformationforadeviceResponse>($"devices/{serial}/lldpCdp", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Get the uplink loss percentage and latency in milliseconds, and goodput in kilobits per second for a wired network device.
        /// </summary>
        /// <param name="serial">(Required) Serial</param>
        public async Task<DputinkilobitspersecondforawirednetworkdeviceResponse> Dputinkilobitspersecondforawirednetworkdevice(DputinkilobitspersecondforawirednetworkdeviceParameters queryParameters, string serial, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"devices/{serial}/lossAndLatencyHistory", parametersDict);
            return await _httpClient.GetFromJsonAsync<DputinkilobitspersecondforawirednetworkdeviceResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Bind a network to a template.
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// configTemplateId| string| The ID of the template to which the network should be bound.
        /// autoBind| boolean| Optional boolean indicating whether the network's switches should automatically bind to profiles of the same model. Defaults to false if left unspecified. This option only affects switch networks and switch templates. Auto-bind is not valid unless the switch template has at least one profile and has at most one profile per switch model.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}` | bind
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<BindANetworkToATemplateResponse> BindANetworkToATemplate(BindANetworkToATemplateRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PostJsonAsync<BindANetworkToATemplateResponse>($"networks/{networkId}/bind", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Delete a network
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}` | destroy
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<Stream> DeleteANetwork(string networkId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"networks/{networkId}", cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Return a network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ReturnANetworkResponse> ReturnANetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnANetworkResponse>($"networks/{networkId}", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Split a combined network into individual networks for each type of device
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<WorkIntoIndividualNetworksForEachTypeOfDeviceResponse> WorkIntoIndividualNetworksForEachTypeOfDevice(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PostFromJsonAsync<WorkIntoIndividualNetworksForEachTypeOfDeviceResponse>($"networks/{networkId}/split", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Unbind a network from a template.
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// retainConfigs| boolean| Optional boolean to retain all the current configs given by the template.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}` | unbind
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<UnbindANetworkFromATemplateResponse> UnbindANetworkFromATemplate(UnbindANetworkFromATemplateRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PostJsonAsync<UnbindANetworkFromATemplateResponse>($"networks/{networkId}/unbind", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update a network
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| The name of the network
        /// timeZone| string| The timezone of the network. For a list of allowed timezones, please see the 'TZ' column in the table in <a target='_blank' href='https://en.wikipedia.org/wiki/List_of_tz_database_time_zones'>this article.</a>
        /// tags| array| A list of tags to be applied to the network
        /// enrollmentString| string| A unique identifier which can be used for device enrollment or easy access through the Meraki SM Registration page or the Self Service Portal. Please note that changing this field may cause existing bookmarks to break.
        /// notes| string| Add any notes or additional information about this network here.
        /// 
        /// #### Supports Action Batches
        /// 
        /// **Resource**|**Operation**
        /// :-------------: |:-------------:
        /// `/networks/{networkId}` | update
        /// 
        /// 
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<UpdateANetworkResponse> UpdateANetwork(UpdateANetworkRequest request, string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateANetworkResponse>($"networks/{networkId}", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the alert history for this network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ReturnthealerthistoryforthisnetworkResponse> Returnthealerthistoryforthisnetwork(ReturnthealerthistoryforthisnetworkParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/alerts/history", parametersDict);
            return await _httpClient.GetFromJsonAsync<ReturnthealerthistoryforthisnetworkResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the Bluetooth clients seen by APs in this network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ListTheBluetoothClientsSeenByAPsInThisNetworkResponse> ListTheBluetoothClientsSeenByAPsInThisNetwork(ListTheBluetoothClientsSeenByAPsInThisNetworkParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/bluetoothClients", parametersDict);
            return await _httpClient.GetFromJsonAsync<ListTheBluetoothClientsSeenByAPsInThisNetworkResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return a Bluetooth client. Bluetooth clients can be identified by their ID or their MAC.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="bluetoothClientId">(Required) Bluetooth client ID</param>
        public async Task<ReturnABluetoothClientResponse> ReturnABluetoothClient(ReturnABluetoothClientParameters queryParameters, string networkId, string bluetoothClientId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/bluetoothClients/{bluetoothClientId}", parametersDict);
            return await _httpClient.GetFromJsonAsync<ReturnABluetoothClientResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the clients that have used this network in the timespan
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<HeClientsThatHaveUsedThisNetworkInTheTimespanResponse> HeClientsThatHaveUsedThisNetworkInTheTimespan(HeClientsThatHaveUsedThisNetworkInTheTimespanParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/clients", parametersDict);
            return await _httpClient.GetFromJsonAsync<HeClientsThatHaveUsedThisNetworkInTheTimespanResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the client associated with the given identifier. Clients can be identified by a client key or either the MAC or IP depending on whether the network uses Track-by-IP.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        /// <param name="clientId">(Required) Client ID</param>
        public async Task<TurnTheClientAssociatedWithTheGivenIdentifierResponse> TurnTheClientAssociatedWithTheGivenIdentifier(string networkId, string clientId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<TurnTheClientAssociatedWithTheGivenIdentifierResponse>($"networks/{networkId}/clients/{clientId}", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the events for the network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ListTheEventsForTheNetworkResponse> ListTheEventsForTheNetwork(ListTheEventsForTheNetworkParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/events", parametersDict);
            return await _httpClient.GetFromJsonAsync<ListTheEventsForTheNetworkResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Get the channel utilization over each radio for all APs in a network.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ElutilizationovereachradioforallAPsinanetworkResponse> ElutilizationovereachradioforallAPsinanetwork(ElutilizationovereachradioforallAPsinanetworkParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/networkHealth/channelUtilization", parametersDict);
            return await _httpClient.GetFromJsonAsync<ElutilizationovereachradioforallAPsinanetworkResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the splash login attempts for a network
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ListthesplashloginattemptsforanetworkResponse> Listthesplashloginattemptsforanetwork(ListthesplashloginattemptsforanetworkParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/splashLoginAttempts", parametersDict);
            return await _httpClient.GetFromJsonAsync<ListthesplashloginattemptsforanetworkResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the LLDP and CDP information for all discovered devices and connections in a network.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<RalldiscovereddevicesandconnectionsinanetworkResponse> Ralldiscovereddevicesandconnectionsinanetwork(string networkId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<RalldiscovereddevicesandconnectionsinanetworkResponse>($"networks/{networkId}/topology/linkLayer", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the traffic analysis data for this network. Traffic analysis with hostname visibility must be enabled on the network.
        /// </summary>
        /// <param name="networkId">(Required) Network ID</param>
        public async Task<ReturnthetrafficanalysisdataforthisnetworkResponse> Returnthetrafficanalysisdataforthisnetwork(ReturnthetrafficanalysisdataforthisnetworkParameters queryParameters, string networkId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"networks/{networkId}/traffic", parametersDict);
            return await _httpClient.GetFromJsonAsync<ReturnthetrafficanalysisdataforthisnetworkResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Claim a list of devices, licenses, and/or orders into an organization. When claiming by order, all devices and licenses in the order will be claimed; licenses will be added to the organization and devices will be placed in the organization's inventory.
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<FDevicesLicensesAndOrOrdersIntoAnOrganizationResponse> FDevicesLicensesAndOrOrdersIntoAnOrganization(FDevicesLicensesAndOrOrdersIntoAnOrganizationRequest request, string organizationId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PostJsonAsync<FDevicesLicensesAndOrOrdersIntoAnOrganizationResponse>($"organizations/{organizationId}/claim", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Create a new organization
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| The name of the organization
        /// management| object| Information about the organization's management system
        /// 
        /// </summary>
        public async Task<Stream> CreateANewOrganization(CreateANewOrganizationRequest request, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"organizations", request, cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Create a new organization by cloning the addressed organization
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| The name of the new organization
        /// 
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<Stream> OrganizationByCloningTheAddressedOrganization(OrganizationByCloningTheAddressedOrganizationRequest request, string organizationId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"organizations/{organizationId}/clone", request, cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// Delete an organization
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<Stream> DeleteAnOrganization(string organizationId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"organizations/{organizationId}", cancellationToken: cancellationToken);
            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    
        /// <summary>
        /// List the organizations that the user has privileges on
        /// </summary>
        public async Task<IstTheOrganizationsThatTheUserHasPrivilegesOnResponse> IstTheOrganizationsThatTheUserHasPrivilegesOn(CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<IstTheOrganizationsThatTheUserHasPrivilegesOnResponse>($"organizations", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return an organization
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<ReturnAnOrganizationResponse> ReturnAnOrganization(string organizationId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ReturnAnOrganizationResponse>($"organizations/{organizationId}", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Update an organization
        ///  #### Body Parameters 
        /// **Parameter**|**Type**|**Description** 
        ///  :-------------: |:-------------: |:-------------: 
        /// name| string| The name of the organization
        /// management| object| Information about the organization's management system
        /// api| object| API-specific settings
        /// 
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<UpdateAnOrganizationResponse> UpdateAnOrganization(UpdateAnOrganizationRequest request, string organizationId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.PutJsonAsync<UpdateAnOrganizationResponse>($"organizations/{organizationId}", request, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Returns adaptive policy aggregate statistics for an organization
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<IvepolicyaggregatestatisticsforanorganizationResponse> Ivepolicyaggregatestatisticsforanorganization(string organizationId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<IvepolicyaggregatestatisticsforanorganizationResponse>($"organizations/{organizationId}/adaptivePolicy/overview", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the API requests made by an organization
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<ListTheAPIRequestsMadeByAnOrganizationResponse> ListTheAPIRequestsMadeByAnOrganization(ListTheAPIRequestsMadeByAnOrganizationParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/apiRequests", parametersDict);
            return await _httpClient.GetFromJsonAsync<ListTheAPIRequestsMadeByAnOrganizationResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return data usage (in megabits per second) over time for all clients in the given organization within a given time range.
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<TsinthegivenorganizationwithinagiventimerangeResponse> Tsinthegivenorganizationwithinagiventimerange(TsinthegivenorganizationwithinagiventimerangeParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/clients/bandwidthUsageHistory", parametersDict);
            return await _httpClient.GetFromJsonAsync<TsinthegivenorganizationwithinagiventimerangeResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return summary information around client data usage (in mb) across the given organization.
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<ClientdatausageinmbacrossthegivenorganizationResponse> Clientdatausageinmbacrossthegivenorganization(ClientdatausageinmbacrossthegivenorganizationParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/clients/overview", parametersDict);
            return await _httpClient.GetFromJsonAsync<ClientdatausageinmbacrossthegivenorganizationResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// View the Change Log for your organization
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<ViewtheChangeLogforyourorganizationResponse> ViewtheChangeLogforyourorganization(ViewtheChangeLogforyourorganizationParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/configurationChanges", parametersDict);
            return await _httpClient.GetFromJsonAsync<ViewtheChangeLogforyourorganizationResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the availability information for devices in an organization. The data returned by this endpoint is updated every 5 minutes.
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<LabilityinformationfordevicesinanorganizationResponse> Labilityinformationfordevicesinanorganization(LabilityinformationfordevicesinanorganizationParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/devices/availabilities", parametersDict);
            return await _httpClient.GetFromJsonAsync<LabilityinformationfordevicesinanorganizationResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the power status information for devices in an organization. The data returned by this endpoint is updated every 5 minutes.
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<ErstatusinformationfordevicesinanorganizationResponse> Erstatusinformationfordevicesinanorganization(ErstatusinformationfordevicesinanorganizationParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/devices/powerModules/statuses/byDevice", parametersDict);
            return await _httpClient.GetFromJsonAsync<ErstatusinformationfordevicesinanorganizationResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the provisioning statuses information for devices in an organization.
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<StatusesinformationfordevicesinanorganizationResponse> Statusesinformationfordevicesinanorganization(StatusesinformationfordevicesinanorganizationParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/devices/provisioning/statuses", parametersDict);
            return await _httpClient.GetFromJsonAsync<StatusesinformationfordevicesinanorganizationResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the status of every Meraki device in the organization
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<TheStatusOfEveryMerakiDeviceInTheOrganizationResponse> TheStatusOfEveryMerakiDeviceInTheOrganization(TheStatusOfEveryMerakiDeviceInTheOrganizationParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/devices/statuses", parametersDict);
            return await _httpClient.GetFromJsonAsync<TheStatusOfEveryMerakiDeviceInTheOrganizationResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the current uplink addresses for devices in an organization.
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<RentuplinkaddressesfordevicesinanorganizationResponse> Rentuplinkaddressesfordevicesinanorganization(RentuplinkaddressesfordevicesinanorganizationParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/devices/uplinks/addresses/byDevice", parametersDict);
            return await _httpClient.GetFromJsonAsync<RentuplinkaddressesfordevicesinanorganizationResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the uplink loss and latency for every MX in the organization from at latest 2 minutes ago
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<EryMXintheorganizationfromatlatest2minutesagoResponse> EryMXintheorganizationfromatlatest2minutesago(EryMXintheorganizationfromatlatest2minutesagoParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/devices/uplinksLossAndLatency", parametersDict);
            return await _httpClient.GetFromJsonAsync<EryMXintheorganizationfromatlatest2minutesagoResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return an overview of the license state for an organization
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<NanoverviewofthelicensestateforanorganizationResponse> Nanoverviewofthelicensestateforanorganization(string organizationId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<NanoverviewofthelicensestateforanorganizationResponse>($"organizations/{organizationId}/licenses/overview", cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the OpenAPI Specification of the organization's API documentation in JSON
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<AtionoftheorganizationsAPIdocumentationinJSONResponse> AtionoftheorganizationsAPIdocumentationinJSON(AtionoftheorganizationsAPIdocumentationinJSONParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/openapiSpec", parametersDict);
            return await _httpClient.GetFromJsonAsync<AtionoftheorganizationsAPIdocumentationinJSONResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the top 10 appliances sorted by utilization over given time range.
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<PliancessortedbyutilizationovergiventimerangeResponse> Pliancessortedbyutilizationovergiventimerange(PliancessortedbyutilizationovergiventimerangeParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/summary/top/appliances/byUtilization", parametersDict);
            return await _httpClient.GetFromJsonAsync<PliancessortedbyutilizationovergiventimerangeResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return metrics for organization's top 10 clients by data usage (in mb) over given time range.
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<Top10clientsbydatausageinmbovergiventimerangeResponse> Top10clientsbydatausageinmbovergiventimerange(Top10clientsbydatausageinmbovergiventimerangeParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/summary/top/clients/byUsage", parametersDict);
            return await _httpClient.GetFromJsonAsync<Top10clientsbydatausageinmbovergiventimerangeResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return metrics for organization's top clients by data usage (in mb) over given time range, grouped by manufacturer.
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<GeinmbovergiventimerangegroupedbymanufacturerResponse> Geinmbovergiventimerangegroupedbymanufacturer(GeinmbovergiventimerangegroupedbymanufacturerParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/summary/top/clients/manufacturers/byUsage", parametersDict);
            return await _httpClient.GetFromJsonAsync<GeinmbovergiventimerangegroupedbymanufacturerResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return metrics for organization's top 10 devices sorted by data usage over given time range. Default unit is megabytes.
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<P10devicessortedbydatausageovergiventimerangeResponse> P10devicessortedbydatausageovergiventimerange(P10devicessortedbydatausageovergiventimerangeParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/summary/top/devices/byUsage", parametersDict);
            return await _httpClient.GetFromJsonAsync<P10devicessortedbydatausageovergiventimerangeResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return metrics for organization's top 10 device models sorted by data usage over given time range. Default unit is megabytes.
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<VicemodelssortedbydatausageovergiventimerangeResponse> Vicemodelssortedbydatausageovergiventimerange(VicemodelssortedbydatausageovergiventimerangeParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/summary/top/devices/models/byUsage", parametersDict);
            return await _httpClient.GetFromJsonAsync<VicemodelssortedbydatausageovergiventimerangeResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return metrics for organization's top 10 ssids by data usage over given time range. Default unit is megabytes.
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<Ationstop10ssidsbydatausageovergiventimerangeResponse> Ationstop10ssidsbydatausageovergiventimerange(Ationstop10ssidsbydatausageovergiventimerangeParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/summary/top/ssids/byUsage", parametersDict);
            return await _httpClient.GetFromJsonAsync<Ationstop10ssidsbydatausageovergiventimerangeResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return metrics for organization's top 10 switches by energy usage over given time range. Default unit is joules.
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<Stop10switchesbyenergyusageovergiventimerangeResponse> Stop10switchesbyenergyusageovergiventimerange(Stop10switchesbyenergyusageovergiventimerangeParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/summary/top/switches/byEnergyUsage", parametersDict);
            return await _httpClient.GetFromJsonAsync<Stop10switchesbyenergyusageovergiventimerangeResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// List the uplink status of every Meraki MX, MG and Z series devices in the organization
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<YMerakiMXMGandZseriesdevicesintheorganizationResponse> YMerakiMXMGandZseriesdevicesintheorganization(YMerakiMXMGandZseriesdevicesintheorganizationParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/uplinks/statuses", parametersDict);
            return await _httpClient.GetFromJsonAsync<YMerakiMXMGandZseriesdevicesintheorganizationResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return a list of alert types to be used with managing webhook alerts
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<OfalerttypestobeusedwithmanagingwebhookalertsResponse> Ofalerttypestobeusedwithmanagingwebhookalerts(OfalerttypestobeusedwithmanagingwebhookalertsParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/webhooks/alertTypes", parametersDict);
            return await _httpClient.GetFromJsonAsync<OfalerttypestobeusedwithmanagingwebhookalertsResponse>(queryString, cancellationToken: cancellationToken);
        }
    
        /// <summary>
        /// Return the log of webhook POSTs sent
        /// </summary>
        /// <param name="organizationId">(Required) Organization ID</param>
        public async Task<ReturnthelogofwebhookPOSTssentResponse> ReturnthelogofwebhookPOSTssent(ReturnthelogofwebhookPOSTssentParameters queryParameters, string organizationId, CancellationToken cancellationToken = default)
        {
            var parametersDict = queryParameters.ToDictionary();
            var queryString = QueryHelpers.AddQueryString($"organizations/{organizationId}/webhooks/logs", parametersDict);
            return await _httpClient.GetFromJsonAsync<ReturnthelogofwebhookPOSTssentResponse>(queryString, cancellationToken: cancellationToken);
        }
    }
}