using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateLayer3OSPFRoutingConfigurationRequest>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class UpdateLayer3OSPFRoutingConfigurationRequest
    {
        public string Enabled { get; set; }
        public string HelloTimerInSeconds { get; set; }
        public string DeadTimerInSeconds { get; set; }
        public List<Areas2> Areas { get; set; }
        public V4 V3 { get; set; }
        public string Md5AuthenticationEnabled { get; set; }
        public Md5AuthenticationKey2 Md5AuthenticationKey { get; set; }
    }

    public class V4
    {
        public string Enabled { get; set; }
        public string HelloTimerInSeconds { get; set; }
        public string DeadTimerInSeconds { get; set; }
        public List<Areas2> Areas { get; set; }
    }

    public class Md5AuthenticationKey2
    {
        public string Id { get; set; }
        public string Passphrase { get; set; }
    }
}