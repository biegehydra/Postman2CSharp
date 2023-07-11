using System;
using System.Collections.Generic;

namespace Postman2CSharp.Core.Models.Insomnia
{
    public class Resource : BaseResource
    {
        public string? Description { get; set; }

        public bool? IsPrivate { get; set; }

        public bool? SettingStoreCookies { get; set; }

        public bool? SettingSendCookies { get; set; }

        public bool? SettingDisableRenderRequestBody { get; set; }

        public bool? SettingEncodeUrl { get; set; }

        public bool? SettingRebuildPath { get; set; }

        public string? SettingFollowRedirects { get; set; }

        public Dictionary<string, string>? Environment { get; set; }

        public object? EnvironmentPropertyOrder { get; set; }

        public string? Scope { get; set; }

        public string? Method { get; set; }

        public string? Url { get; set; }

        public Body? Body { get; set; }

        public List<Parameter>? Parameters { get; set; }

        public List<Header>? Headers { get; set; }

        public Authentication? Authentication { get; set; }

        private InsomniaResourceType? _insomniaResourceType;
        public InsomniaResourceType ResourceType()
        {
            return _insomniaResourceType ??= Type switch
            {
                "request" => InsomniaResourceType.Request,
                "request_group" => InsomniaResourceType.RequestGroup,
                "workspace" => InsomniaResourceType.Workspace,
                "environment" => InsomniaResourceType.Environment,
                "cookie_jar" => InsomniaResourceType.CookieJar,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
