using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<AdaptivePolicyAggregateStatisticsForAnOrganizationResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class AdaptivePolicyAggregateStatisticsForAnOrganizationResponse
    {
        public Counts6 Counts { get; set; }
        public Limits Limits { get; set; }
    }

    public class Counts6
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