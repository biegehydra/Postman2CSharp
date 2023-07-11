using System.Collections.Generic;

namespace Postman2CSharp.Core.Models.Insomnia.SpecificResources
{
    public class RequestGroup : BaseResource
    {
        public string? Description { get; set; }
        public object? EnvironmentPropertyOrder { get; set; }
        /// <summary>
        /// Environment override data.
        /// </summary>
        public Dictionary<string, string>? Environment { get; set; }
    }
}
