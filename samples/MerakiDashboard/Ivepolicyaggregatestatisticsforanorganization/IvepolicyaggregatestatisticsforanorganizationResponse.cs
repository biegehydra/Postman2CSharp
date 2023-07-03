using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<IvepolicyaggregatestatisticsforanorganizationResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class IvepolicyaggregatestatisticsforanorganizationResponse
    {
        public Counts11 Counts { get; set; }
        public Limits Limits { get; set; }
    }

    public class Counts11
    {
        public int Groups { get; set; }
        public int CustomGroups { get; set; }
        public int CustomAcls { get; set; }
        public int Policies { get; set; }
        public int DenyPolicies { get; set; }
        public int AllowPolicies { get; set; }
        public int PolicyObjects { get; set; }
    }

    public class Limits
    {
        public int CustomGroups { get; set; }
        public int RulesInAnAcl { get; set; }
        public int AclsInAPolicy { get; set; }
        public int PolicyObjects { get; set; }
    }
}