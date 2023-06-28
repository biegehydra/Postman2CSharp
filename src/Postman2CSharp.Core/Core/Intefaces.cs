using System.Collections.Generic;
using System.Net.Http;

namespace Postman2CSharp.Core.Core;

public interface IKvp
{
    public string Key { get; set; }
    public string Value { get; set; }
}
public interface IQueryParameters
{
    Dictionary<string, string?> ToDictionary();
}

public interface IFormData
{
    FormUrlEncodedContent ToFormData();
}

public interface IMultipartFormData
{
    MultipartFormDataContent ToMultipartFormData();
}
