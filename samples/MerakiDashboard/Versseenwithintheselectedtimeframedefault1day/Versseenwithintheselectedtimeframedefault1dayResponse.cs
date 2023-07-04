using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<VersSeenWithinTheSelectedTimeframeDefault1DayResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class VersSeenWithinTheSelectedTimeframeDefault1DayResponse
    {
        public string Mac { get; set; }
        public int Vlan { get; set; }
        public string ClientId { get; set; }
        public bool IsAllowed { get; set; }
        public DateTime LastSeenAt { get; set; }
        public List<SeenBy> SeenBy { get; set; }
        public string Type { get; set; }
        public Device2 Device { get; set; }
        public Ipv4 Ipv4 { get; set; }
        public bool IsConfigured { get; set; }
        public LastAck LastAck { get; set; }
        public LastPacket LastPacket { get; set; }
    }

    public class Fields
    {
        public int Op { get; set; }
        public int Htype { get; set; }
        public int Hlen { get; set; }
        public int Hops { get; set; }
        public string Xid { get; set; }
        public int Secs { get; set; }
        public string Flags { get; set; }
        public string Ciaddr { get; set; }
        public string Yiaddr { get; set; }
        public string Siaddr { get; set; }
        public string Giaddr { get; set; }
        public string Chaddr { get; set; }
        public string Sname { get; set; }
        public string MagicCookie { get; set; }
        public List<Parameters2> Options { get; set; }
    }

    public class Ip
    {
        public string Id { get; set; }
        public int Version { get; set; }
        public int Length { get; set; }
        public int HeaderLength { get; set; }
        public int Protocol { get; set; }
        public int Ttl { get; set; }
        public Dscp Dscp { get; set; }
    }

    public class LastPacket
    {
        public Source Source { get; set; }
        public Destination Destination { get; set; }
        public string Type { get; set; }
        public Rules11 Ethernet { get; set; }
        public Ip Ip { get; set; }
        public Udp Udp { get; set; }
        public Fields Fields { get; set; }
    }

    public class Device2
    {
        public string Serial { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Interface Interface { get; set; }
    }

    public class Destination
    {
        public string Mac { get; set; }
        public Ipv4 Ipv4 { get; set; }
        public int Port { get; set; }
    }

    public class Ipv47
    {
        public string Address { get; set; }
        public string Subnet { get; set; }
        public string Gateway { get; set; }
    }

    public class SeenBy
    {
        public string Serial { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class Source
    {
        public string Mac { get; set; }
        public Ipv4 Ipv4 { get; set; }
        public int Port { get; set; }
    }

    public class Dscp
    {
        public int Tag { get; set; }
        public int Ecn { get; set; }
    }

    public class Interface
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class LastAck
    {
        public DateTime Ts { get; set; }
        public Ipv4 Ipv4 { get; set; }
    }

    public class Udp
    {
        public int Length { get; set; }
        public string Checksum { get; set; }
    }
}