using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Postman2CSharp.Core.Models.Insomnia
{
    public class Resource : BaseResource
    {
        [JsonPropertyName("_type")]
        [JsonRequired]
        public required string Type { get; set; }

        private InsomniaResourceType? _resourceType { get; set; }
        public InsomniaResourceType ResourceType()
        {
            return _resourceType ??= Type switch
            {
                "request" => InsomniaResourceType.Request,
                "request_group" => InsomniaResourceType.RequestGroup,
                "workspace" => InsomniaResourceType.Workspace,
                "environment" => InsomniaResourceType.Environment,
                _ => throw new ArgumentOutOfRangeException(nameof(Type))
            };
        }

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

        public long? MetaSortKey { get; set; }

        public string? Method { get; set; }

        public string? Url { get; set; }

        public Body? Body { get; set; }

        public List<Parameter>? Parameters { get; set; }

        public List<Header>? Headers { get; set; }

        public Authentication? Authentication { get; set; }

        public enum InsomniaResourceType
        {
            Request, // request
            RequestGroup, // request_group
            Workspace, // workspace
            Environment // environment
        }
    }
}
