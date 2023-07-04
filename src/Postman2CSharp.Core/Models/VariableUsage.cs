#nullable enable

using System;
using System.Text.Json.Serialization;
using Postman2CSharp.Core.Utilities;
using Utils = Postman2CSharp.Core.Utilities.Utils;

namespace Postman2CSharp.Core.Models.PostmanCollection.Http
{
    public class VariableUsage
    {
        public string Original { get; init; }

        public CsharpPropertyType ApiClientUsage { get; set; } 

        public string CSPropertyUsage(CsharpPropertyType type)
        {
            return Utils.NormalizeToCsharpPropertyName(Original, type);
        }

        public string? Value { get; set; }

        [JsonConstructor]
        public VariableUsage(string original, string? value, CsharpPropertyType apiClientUsage)
        {
            Original = original;
            Value = value;
            ApiClientUsage = apiClientUsage;
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
