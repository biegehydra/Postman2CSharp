using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using Postman2CSharp.Core.Models.Insomnia;
using Postman2CSharp.Core.Models.PostmanCollection;
using Postman2CSharp.Core.Models.PostmanCollection.Authorization;
using Postman2CSharp.Core.Models.PostmanCollection.Http;
using Postman2CSharp.Core.Models.PostmanCollection.Http.Request;
using Postman2CSharp.Core.Models.PostmanCollection.Http.Response;
using Body = Postman2CSharp.Core.Models.PostmanCollection.Http.Body;
using Header = Postman2CSharp.Core.Models.PostmanCollection.Http.Header;

namespace Postman2CSharp.Core.Converter;

public class InsomniaConverter
{
    private InsomniaCollection InsomniaCollection { get; }
    private Resource? _workspace;
    private Resource Workspace => _workspace ??= GetWorkspace(InsomniaCollection);
    private List<Resource> AllResources => InsomniaCollection.Resources;
    private List<Resource>? _rootResources;
    private List<Resource> RootResources => _rootResources ??= GetRootResources();

    public InsomniaConverter(InsomniaCollection insomniaCollection)
    {
        InsomniaCollection = insomniaCollection;
    }
    public List<PostmanCollection> Convert()
    {
        List<PostmanCollection> postmanCollections = new();
        foreach (var rootResource in RootResources)
        {
            var postmanCollection = ConvertRootResource(rootResource);
            postmanCollections.Add(postmanCollection);
        }
        return postmanCollections;
    }

    private PostmanCollection ConvertRootResource(Resource rootResource)
    {
        var collectionInfo = GetCollectionInfo(rootResource);
        var variables = GetVariables(new List<Resource> { rootResource });
        var items = GetCollectionItems(rootResource);
        return new PostmanCollection()
        {
            Auth = null,
            Info = collectionInfo,
            Item = items,
            Variable = variables,
        };
    }

    private List<CollectionItem> GetCollectionItems(Resource rootResource)
    {
        var collectionItems = new List<CollectionItem>();
        var childResources = AllResources.Where(x => x.ParentId == rootResource.Id);
        foreach (var childResource in childResources)
        {
            CollectionItem collectionItem;
            switch (childResource.ResourceType())
            {
                case InsomniaResourceType.Request:
                    collectionItem = new CollectionItem()
                    {
                        Name = childResource.Name,
                        Description = childResource.Description,
                        Auth = GetAuth(rootResource),
                        Item = GetCollectionItems(childResource),
                        Request = GetRequest(childResource)
                    };
                    break;
                case InsomniaResourceType.RequestGroup:
                    collectionItem = new CollectionItem()
                    {
                        Name = childResource.Name,
                        Description = childResource.Description,
                        Auth = GetAuth(rootResource),
                        Item = GetCollectionItems(childResource)
                    };
                    break;
                case InsomniaResourceType.Workspace:
                    throw new UnreachableException();
                case InsomniaResourceType.Environment:
                    throw new UnreachableException();
                case InsomniaResourceType.CookieJar:
                    throw new UnreachableException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
            collectionItems.Add(collectionItem);
        }
        return collectionItems;
    }

    private Request GetRequest(Resource resource)
    {
        return new Request()
        {
            Auth = GetAuth(resource),
            Body = GetBody(resource),
            Description = resource.Description,
            Header = resource.Headers?.Select(x => new Header()
            {
                Key = x.Name,
                Value = x.Value,
                Description = null,
                Disabled = x.Disabled,
            }).ToList() ?? new List<Header>(),
            Method = resource.Method ?? throw new ArgumentException("Method cannot be null."),
            Url = GetUrl(resource)
        };
    }

    private Url GetUrl(Resource resource)
    {
        return new Url()
        {
            Path = null,
            Host = null,
            Port = null,
            Protocol = null,
            Query = null,
            Raw = resource.Url ?? throw new ArgumentException("Url cannot be null"),
            Variable = null
        };
    }

    private Body GetBody(Resource resource)
    {
        return new Body()
        {

        }
    }

    private List<Header> GetHeaders()
    {
        var headers = new List<Header>();
        return headers;
    }

    private Response GetResponse(Resource resource)
    {

    }

    private AuthSettings? GetAuth(Resource resource)
    {
        var authentication = resource.Authentication;
        var authenticationType = authentication?.AuthenticationType();
        if (authenticationType is null or InsomniaAuthenticationType.NoAuth)
        {
            return null;
        }
        var typeString = ConvertToPostmanAuthType(authenticationType.Value);
        if (typeString == null) return null;
        var postmanAuthType = Enum.Parse<PostmanAuthType>(typeString);
        AuthSettings authSettings;
        switch (postmanAuthType)
        {
            case PostmanAuthType.apikey:
                var inn = authentication!.AddTo == "header" ? "header" : "query";
                var keyValueTypes = new List<KeyValueType>
                {
                    new()
                    {
                        Key = "value",
                        Value = JsonSerializer.SerializeToElement(authentication.Key),
                        Type = "string"
                    },
                    new ()
                    {
                        Key = "key",
                        Value = JsonSerializer.SerializeToElement(authentication.Value),
                        Type = "string"
                    },
                    new ()
                    {
                        Key = "in",
                        Value = JsonSerializer.SerializeToElement(inn),
                        Type = "string"
                    }
                };
                authSettings = new AuthSettings()
                {
                    Type = typeString,
                    Apikey = keyValueTypes
                };
                break;
            case PostmanAuthType.oauth2:
                var keyValueTypesOAuth2 = new List<KeyValueType>
                {
                    new()
                    {
                        Key = "state",
                        Value = JsonSerializer.SerializeToElement(authentication!.State),
                        Type = "string"
                    },
                    new ()
                    {
                        Key = "scope",
                        Value = JsonSerializer.SerializeToElement(authentication.Scope),
                        Type = "string"
                    },
                    new ()
                    {
                        Key = "clientId",
                        Value = JsonSerializer.SerializeToElement(authentication.ClientId),
                        Type = "string"
                    },
                    new ()
                    {
                        Key = "clientSecret",
                        Value = JsonSerializer.SerializeToElement(authentication.ClientSecret),
                        Type = "string"
                    },
                    new ()
                    {
                        Key = "accessTokenUrl",
                        Value = JsonSerializer.SerializeToElement(authentication.AccessTokenUrl),
                        Type = "string"
                    },
                    new ()
                    {
                        Key = "grantType",
                        Value = JsonSerializer.SerializeToElement(authentication.GrantType),
                        Type = "string"
                    },
                    new ()
                    {
                        Key = "authUrl",
                        Value = JsonSerializer.SerializeToElement(authentication.AuthorizationUrl),
                        Type = "string"
                    },
                    new ()
                    {
                        Key = "redirectUri",
                        Value = JsonSerializer.SerializeToElement(authentication.RedirectUrl),
                        Type = "string"
                    },
                    new ()
                    {
                        Key = "clientId",
                        Value = JsonSerializer.SerializeToElement(authentication.ClientId),
                        Type = "string"
                    }
                };
                authSettings = new AuthSettings()
                {
                    Type = typeString,
                    Oauth2 = keyValueTypesOAuth2
                };
                break;
            default:
                authSettings = new AuthSettings()
                {
                    Type = typeString,
                };
                break;
        }
        return authSettings;
    }

    public static string? ConvertToPostmanAuthType(InsomniaAuthenticationType insomniaAuthType)
    {
        switch (insomniaAuthType)
        {
            case InsomniaAuthenticationType.Digest:
                return PostmanAuthType.digest.ToString();
            case InsomniaAuthenticationType.Netric:
                return null; 
            case InsomniaAuthenticationType.Basic:
                return PostmanAuthType.basic.ToString();
            case InsomniaAuthenticationType.OAuth1:
                return PostmanAuthType.oauth1.ToString();
            case InsomniaAuthenticationType.OAuth2:
                return PostmanAuthType.oauth2.ToString();
            case InsomniaAuthenticationType.Ntlm:
                return PostmanAuthType.ntlm.ToString();
            case InsomniaAuthenticationType.Hawk:
                return PostmanAuthType.hawk.ToString();
            case InsomniaAuthenticationType.AwsIam:
                return PostmanAuthType.awsv4.ToString();
            case InsomniaAuthenticationType.BearerToken:
                return PostmanAuthType.bearer.ToString();
            case InsomniaAuthenticationType.NoAuth:
                return PostmanAuthType.noauth.ToString();
            case InsomniaAuthenticationType.Asap:
                return null;
            case InsomniaAuthenticationType.ApiKey:
                return PostmanAuthType.apikey.ToString();
            default:
                return null;
        }
    }

    private CollectionInfo GetCollectionInfo(Resource rootResource)
    {
        var collectionInfo = new CollectionInfo()
        {
            Name = rootResource.Name,
            Description = rootResource.Description,
            Schema = "https://schema.getpostman.com/json/collection/v2.1.0/collection.json"
        };
        return collectionInfo;
    }

    private List<CollectionVariable> GetVariables(List<Resource> resources)
    {
        var variables = new List<CollectionVariable>();
        foreach (var resource in resources)
        {
            foreach (var (key, value) in resource.Environment ?? new())
            {
                var collectionVariable = new CollectionVariable()
                {
                    Key = key,
                    Value = value
                };
                variables.Add(collectionVariable);
            }
        }

        var ids = resources.Select(x => x.Id).ToList();
        var childResources = AllResources.Where(x => x.ParentId != null && ids.Contains(x.ParentId)).ToList();
        if (childResources.Any())
        {
            variables.AddRange(GetVariables(childResources));
            return variables;
        }
        return variables;
    }



    private static Resource GetWorkspace(InsomniaCollection collection)
    {
        var workSpace = collection.Resources
            .SingleOrDefault(x => x.ResourceType() == InsomniaResourceType.Workspace);
        if (workSpace == null)
        {
            throw new UnreachableException("Collection cannot be converted without workspace resource.");
        }
        return workSpace;
    }

    private List<Resource> GetRootResources()
    {
        var rootResources = AllResources
            .Where(x => x.ResourceType() == InsomniaResourceType.RequestGroup
                     && x.ParentId == Workspace.Id);
        return rootResources.ToList();
    }
}