using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnsTheIdentityOfTheCurrentUserResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnsTheIdentityOfTheCurrentUserResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime LastUsedDashboardAt { get; set; }
        public Authentication Authentication { get; set; }
    }

    public class Authentication
    {
        public string Mode { get; set; }
        public Api Api { get; set; }
        public TwoFactor TwoFactor { get; set; }
        public Saml Saml { get; set; }
    }

    public class Api
    {
        public Key Key { get; set; }
    }

    public class Key
    {
        public bool Created { get; set; }
    }

    public class Saml
    {
        public bool Enabled { get; set; }
    }

    public class TwoFactor
    {
        public bool Enabled { get; set; }
    }
}