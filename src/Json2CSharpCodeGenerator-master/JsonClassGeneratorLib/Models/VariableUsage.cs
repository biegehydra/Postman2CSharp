#nullable enable
using System;
using System.Text.Json.Serialization;

namespace Postman2CSharp.Core.Models.PostmanCollection.Http
{
    public class VariableUsage
    {
        public string Original { get; init; }

        public string CSPropertyUsage { get; init; }

        public string? Value { get; set; }

        [JsonConstructor]
        public VariableUsage(string original, string csPropertyUsage, string? value)
        {
            Original = original;
            CSPropertyUsage = csPropertyUsage;
            Value = value;
        }

        public override bool Equals(object? obj)
        {
            if (obj is VariableUsage other)
            {
                return Original == other.Original && CSPropertyUsage == other.CSPropertyUsage && Value == other.Value;
            }
            // ReSharper disable once BaseObjectEqualsIsObjectEquals
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Original, CSPropertyUsage, Value);
        }
    }
}
#nullable restore
