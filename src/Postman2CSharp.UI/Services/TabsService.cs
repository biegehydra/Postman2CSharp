﻿using MudBlazor;

namespace Postman2CSharp.UI.Services
{
    public class CollectionTabView
    {
        public required string Id { get; init; }
        public string? HttpCallName { get; set; }
        public string? CollectionId { get; set; }
        public string? ApiClientId { get; init; }
        public required string Label { get; init; }
        public bool ShowCloseIcon { get; set; } = true;
        public required TabType Type { get; init; }
    }

    public enum TabType
    {
        HttpCalls,
        HttpCall,
        ApiClient,
        Collection,
        CoreCsFile,
        Interface,
        Controller,
        DuplicateRoots,
        Tests
    }
    public class TabsService
    {
        private Lazy<Navigate> Navigate { get; set; }
        public static event Func<Task>? TabsChanged;
        public TabsService (Lazy<Navigate> navigate)
        {
            Navigate = navigate;
        }


        // public static CollectionTabView? ActiveTab => Tabs.Count > 0 ? Tabs[_apiClientIndex] : null;
        public static string? ActiveCollectionId => Tabs.FirstOrDefault()?.CollectionId;

        public static MudDynamicTabs? DynamicTabs;
        public static List<CollectionTabView> Tabs = new();

        private int _apiClientIndex;
        public int ApiClientIndex
        {
            get => _apiClientIndex;
            set
            {
                if (value == _apiClientIndex) return;
                if (value < 0) return;
                if (Tabs.Count == 0) return;
                if (value > Tabs.Count - 1) return;
                _apiClientIndex = value;
                var tab = Tabs[_apiClientIndex];
                switch (tab.Type)
                {
                    case TabType.HttpCall:
                        Navigate.Value.ToHttpCallClass(tab.CollectionId!, tab.ApiClientId!, tab.HttpCallName!, tab.Label!);
                        break;
                    case TabType.HttpCalls:
                        Navigate.Value.ToHttpCalls(tab.CollectionId!, tab.ApiClientId!);
                        break;
                    case TabType.ApiClient:
                        Navigate.Value.ToApiClient(tab.CollectionId!, tab.ApiClientId!);
                        break;
                    case TabType.Collection:
                        Navigate.Value.ToCollection(tab.CollectionId!);
                        break;
                    case TabType.CoreCsFile:
                        Navigate.Value.ToCoreCsFile(tab.Label);
                        break;
                    case TabType.Interface:
                        Navigate.Value.ToApiClientInterface(tab.CollectionId!, tab.ApiClientId!);
                        break;
                    case TabType.Controller:
                        Navigate.Value.ToApiClientController(tab.CollectionId!, tab.ApiClientId!);
                        break;
                    case TabType.Tests:
                        Navigate.Value.ToApiClientTests(tab.CollectionId!, tab.ApiClientId!);
                        break;
                    case TabType.DuplicateRoots:
                        Navigate.Value.ToApiClientDuplicateRoots(tab.CollectionId!, tab.ApiClientId!);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                TabsChanged?.Invoke();
            }

        }

        public void SetHome(string? collectionId, bool setIndex = false)
        {
            var currentHomeTab = Tabs.FirstOrDefault();

            if (collectionId == null)
            {
                Tabs = new();
            }
            else if (currentHomeTab == null)
            {
                Tabs.Add(new CollectionTabView()
                {
                    Id = Guid.NewGuid().ToString(),
                    CollectionId = collectionId,
                    Label = "Collection",
                    ShowCloseIcon = false,
                    Type = TabType.Collection

                });
            }
            else if (currentHomeTab.Label == "Collection")
            {
                currentHomeTab.CollectionId = collectionId;
                Tabs = Tabs.Where(x => x.CollectionId == null || x.CollectionId == currentHomeTab.CollectionId).ToList();
            }
            else
            {
                Tabs.Insert(0, new CollectionTabView()
                {
                    Id = Guid.NewGuid().ToString(),
                    CollectionId = collectionId,
                    Label = "Collection",
                    ShowCloseIcon = false,
                    Type = TabType.Collection
                });
            }
            if (setIndex)
            {
                ApiClientIndex = 0;
            }

            TabsChanged?.Invoke();
        }

        public async Task AddApiClientTab(string collectionId, string apiClientId, string label)
        {
            if (Tabs.FirstOrDefault(x => x.Type == TabType.ApiClient && x.ApiClientId == apiClientId && x.Label == label) is { } tab)
            {
                var index = Tabs.IndexOf(tab);
                if (ApiClientIndex != index)
                    ApiClientIndex = index;
                return;
            }
            SetHome(collectionId);
            Tabs.Insert(1, new CollectionTabView
            {
                Id = Guid.NewGuid().ToString(),
                Label = label,
                ApiClientId = apiClientId,
                CollectionId = collectionId,
                Type = TabType.ApiClient
            });
            await MoveToNewTab();
        }

        public async Task AddApiClientInterfaceTab(string collectionId, string apiClientId, string label)
        {
            if (Tabs.FirstOrDefault(x => x.Type == TabType.Interface && x.ApiClientId == apiClientId && x.Label == label) is {} tab)
            {
                var index = Tabs.IndexOf(tab);
                if (ApiClientIndex != index)
                    ApiClientIndex = index;
                return;
            }
            SetHome(collectionId);
            Tabs.Insert(1, new CollectionTabView
            {
                Id = Guid.NewGuid().ToString(),
                Label = label,
                ApiClientId = apiClientId,
                CollectionId = collectionId,
                Type = TabType.Interface
            });
            await MoveToNewTab();
        }

        public async Task AddApiClientDuplicateRootsTab(string collectionId, string apiClientId)
        {
            if (Tabs.FirstOrDefault(x => x.Type == TabType.DuplicateRoots && x.ApiClientId == apiClientId && x.Label == "Duplicate Roots") is { } tab)
            {
                var index = Tabs.IndexOf(tab);
                if (ApiClientIndex != index)
                    ApiClientIndex = index;
                return;
            }
            SetHome(collectionId);
            Tabs.Insert(1, new CollectionTabView
            {
                Id = Guid.NewGuid().ToString(),
                Label = "Duplicate Roots",
                ApiClientId = apiClientId,
                CollectionId = collectionId,
                Type = TabType.DuplicateRoots
            });
            await MoveToNewTab();
        }

        public async Task AddApiClientControllerTab(string collectionId, string apiClientId, string label)
        {
            if (Tabs.FirstOrDefault(x => x.Type == TabType.Controller && x.ApiClientId == apiClientId && x.Label == label) is { } tab)
            {
                var index = Tabs.IndexOf(tab);
                if (ApiClientIndex != index)
                    ApiClientIndex = index;
                return;
            }
            SetHome(collectionId);
            Tabs.Insert(1, new CollectionTabView
            {
                Id = Guid.NewGuid().ToString(),
                Label = label,
                ApiClientId = apiClientId,
                CollectionId = collectionId,
                Type = TabType.Controller
            });
            await MoveToNewTab();
        }

        public async Task AddApiClientTestsTab(string collectionId, string apiClientId, string label)
        {
            if (Tabs.FirstOrDefault(x => x.Type == TabType.Tests && x.ApiClientId == apiClientId && x.Label == label) is { } tab)
            {
                var index = Tabs.IndexOf(tab);
                if (ApiClientIndex != index)
                    ApiClientIndex = index;
                return;
            }
            SetHome(collectionId);
            Tabs.Insert(1, new CollectionTabView
            {
                Id = Guid.NewGuid().ToString(),
                Label = label,
                ApiClientId = apiClientId,
                CollectionId = collectionId,
                Type = TabType.Tests
            });
            await MoveToNewTab();
        }

        public async Task AddCoreCsFileTab(string label)
        {
            if (Tabs.FirstOrDefault(x => x.Label == label) is { } tab)
            {
                var index = Tabs.IndexOf(tab);
                if (ApiClientIndex != index)
                    ApiClientIndex = index;
                return;
            }
            var insertIndex = 0;
            if (Tabs.Count > 1)
            {
                insertIndex = 1;
            }
            Tabs.Insert(insertIndex, new CollectionTabView
            {
                Id = Guid.NewGuid().ToString(),
                Label = label,
                Type = TabType.CoreCsFile
            });
            await MoveToNewTab();
        }

        public async Task AddHttpCallClass(string collectionId, string apiClientId, string httpCallName, string className)
        {
            if (Tabs.FirstOrDefault(x => x.Type == TabType.HttpCall && x.ApiClientId == apiClientId && x.Label == className && x.HttpCallName == httpCallName) is { } tab)

            {
                var index = Tabs.IndexOf(tab);
                if (ApiClientIndex != index)
                    ApiClientIndex = index;
                return;
            }
            SetHome(collectionId);
            Tabs.Insert(1, new CollectionTabView
            {
                Id = Guid.NewGuid().ToString(),
                HttpCallName = httpCallName,
                CollectionId = collectionId,
                Label = className,
                ApiClientId = apiClientId,
                Type = TabType.HttpCall
            });
            await MoveToNewTab();
        }

        public async Task AddHttpCalls(string collectionId, string apiClientId)
        {
            if (Tabs.FirstOrDefault(x => x.Type == TabType.HttpCalls && x.ApiClientId == apiClientId) is { } tab)
            {
                var index = Tabs.IndexOf(tab);
                if (ApiClientIndex != index)
                    ApiClientIndex = index;
                return;
            }
            SetHome(collectionId);
            Tabs.Insert(1, new CollectionTabView
            {
                Id = Guid.NewGuid().ToString(),
                CollectionId = collectionId,
                Label = "Http Calls",
                ApiClientId = apiClientId,
                Type = TabType.HttpCalls
            });
            await MoveToNewTab();
        }

        public void RemoveTab(string id)
        {
            var tabView = Tabs.SingleOrDefault(t => Equals(t.Id, id));
            if (tabView is not null)
            {
                Tabs.Remove(tabView);
            }
        }

        private async Task MoveToNewTab()
        {
            if (DynamicTabs == null) return;
            TabsChanged?.Invoke();
            await Task.Delay(10);
            if (Tabs.Count <= 0) return;
            if (Tabs.Count == 1) ApiClientIndex = 0;
            else ApiClientIndex = 1;
        }
    }
}