using System.Collections.Generic;
using System.Net.Http;
using OneOf;

namespace PaypalSubscription
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