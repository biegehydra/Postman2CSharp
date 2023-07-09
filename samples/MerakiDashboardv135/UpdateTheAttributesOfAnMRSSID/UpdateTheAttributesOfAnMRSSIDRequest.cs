using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheAttributesOfAnMRSSIDRequest>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class UpdateTheAttributesOfAnMRSSIDRequest
    {
        public string Name { get; set; }
        public string Enabled { get; set; }
        public string AuthMode { get; set; }
        public string EnterpriseAdminAccess { get; set; }
        public string EncryptionMode { get; set; }
        public string Psk { get; set; }
        public string WpaEncryptionMode { get; set; }
        public Dot11w Dot11w { get; set; }
        public Dot11r Dot11r { get; set; }
        public string SplashPage { get; set; }
        public List<string> SplashGuestSponsorDomains { get; set; }
        public Oauth Oauth { get; set; }
        public LocalRadius LocalRadius { get; set; }
        public Ldap Ldap { get; set; }
        public ActiveDirectory ActiveDirectory { get; set; }
        public List<RadiusServers4> RadiusServers { get; set; }
        public string RadiusProxyEnabled { get; set; }
        public string RadiusTestingEnabled { get; set; }
        public string RadiusCalledStationId { get; set; }
        public string RadiusAuthenticationNasId { get; set; }
        public string RadiusServerTimeout { get; set; }
        public string RadiusServerAttemptsLimit { get; set; }
        public string RadiusFallbackEnabled { get; set; }
        public string RadiusCoaEnabled { get; set; }
        public string RadiusFailoverPolicy { get; set; }
        public string RadiusLoadBalancingPolicy { get; set; }
        public string RadiusAccountingEnabled { get; set; }
        public List<RadiusAccountingServers4> RadiusAccountingServers { get; set; }
        public string RadiusAccountingInterimInterval { get; set; }
        public string RadiusAttributeForGroupPolicies { get; set; }
        public string IpAssignmentMode { get; set; }
        public string UseVlanTagging { get; set; }
        public string ConcentratorNetworkId { get; set; }
        public string SecondaryConcentratorNetworkId { get; set; }
        public string DisassociateClientsOnVpnFailover { get; set; }
        public string VlanId { get; set; }
        public string DefaultVlanId { get; set; }
        public List<ApTagsAndVlanIds> ApTagsAndVlanIds { get; set; }
        public string WalledGardenEnabled { get; set; }
        public List<string> WalledGardenRanges { get; set; }
        public Gre Gre { get; set; }
        public string RadiusOverride { get; set; }
        public string RadiusGuestVlanEnabled { get; set; }
        public string RadiusGuestVlanId { get; set; }
        public string MinBitrate { get; set; }
        public string BandSelection { get; set; }
        public string PerClientBandwidthLimitUp { get; set; }
        public string PerClientBandwidthLimitDown { get; set; }
        public string PerSsidBandwidthLimitUp { get; set; }
        public string PerSsidBandwidthLimitDown { get; set; }
        public string LanIsolationEnabled { get; set; }
        public string Visible { get; set; }
        public string AvailableOnAllAps { get; set; }
        public List<string> AvailabilityTags { get; set; }
        public string MandatoryDhcpEnabled { get; set; }
        public string AdultContentFilteringEnabled { get; set; }
        public DnsRewrite DnsRewrite { get; set; }
        public SpeedBurst SpeedBurst { get; set; }
    }

    public class RadiusServers4
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string Secret { get; set; }
        public string RadsecEnabled { get; set; }
        public string OpenRoamingCertificateId { get; set; }
        public string CaCertificate { get; set; }
    }

    public class CertificateAuthentication
    {
        public string Enabled { get; set; }
        public string UseLdap { get; set; }
        public string UseOcsp { get; set; }
        public string OcspResponderUrl { get; set; }
        public ClientRootCaCertificate ClientRootCaCertificate { get; set; }
    }

    public class RadiusAccountingServers4
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string Secret { get; set; }
        public string RadsecEnabled { get; set; }
        public string CaCertificate { get; set; }
    }

    public class Ldap
    {
        public List<Servers2> Servers { get; set; }
        public Credentials2 Credentials { get; set; }
        public string BaseDistinguishedName { get; set; }
        public ServerCaCertificate ServerCaCertificate { get; set; }
    }

    public class Credentials2
    {
        public string DistinguishedName { get; set; }
        public string Password { get; set; }
        public string LogonName { get; set; }
    }

    public class LocalRadius
    {
        public string CacheTimeout { get; set; }
        public PasswordAuthentication PasswordAuthentication { get; set; }
        public CertificateAuthentication CertificateAuthentication { get; set; }
    }

    public class ActiveDirectory
    {
        public List<Servers2> Servers { get; set; }
        public Credentials2 Credentials { get; set; }
    }

    public class ApTagsAndVlanIds
    {
        public List<string> Tags { get; set; }
        public string VlanId { get; set; }
    }

    public class DnsRewrite
    {
        public string Enabled { get; set; }
        public List<string> DnsCustomNameservers { get; set; }
    }

    public class Dot11r
    {
        public string Enabled { get; set; }
        public string Adaptive { get; set; }
    }

    public class Gre
    {
        public Concentrator Concentrator { get; set; }
        public string Key { get; set; }
    }

    public class Servers2
    {
        public string Host { get; set; }
        public string Port { get; set; }
    }

    public class ClientRootCaCertificate
    {
        public string Contents { get; set; }
    }

    public class Concentrator
    {
        public string Host { get; set; }
    }

    public class Oauth
    {
        public List<string> AllowedDomains { get; set; }
    }

    public class PasswordAuthentication
    {
        public string Enabled { get; set; }
    }

    public class ServerCaCertificate
    {
        public string Contents { get; set; }
    }

    public class SpeedBurst
    {
        public string Enabled { get; set; }
    }
}