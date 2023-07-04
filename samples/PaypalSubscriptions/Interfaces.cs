using System.Collections.Generic;
using System.Net.Http;

namespace PaypalSubscriptions
{
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
    }}