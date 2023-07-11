using System.Collections.Generic;

namespace Postman2CSharp.Core.Models.Insomnia.SpecificResources
{
    public class RequestGroup : BaseResource
    {
        /// <summary>
        /// Environment override data.
        /// </summary>
        public Dictionary<string, string> Environment { get; set; }
    }
}
