using System.Collections.Generic;

namespace Postman2CSharp.Core.Models.Insomnia.SpecificResources;

public class CookieJar : BaseResource
{
    public List<object>? Cookies { get; set; }
}
