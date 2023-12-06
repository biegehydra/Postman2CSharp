using Microsoft.AspNetCore.Components.Routing;
using MudBlazor;

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
        public double? SavedScrollPosition { get; set; }
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
    public class TabsService : IDisposable
    {
        private Lazy<Navigate> Navigate { get; }
        private Lazy<Interop> Interop { get; }
        public static event Func<Task>? TabsChanged;
        public TabsService (Lazy<Navigate> navigate, Lazy<Interop> interop)
        {
            Navigate = navigate;
            Interop = interop;
            Navigate.Value.LocationChanged += OnLocationChanged;
        }


        // public static CollectionTabView? ActiveTab => Tabs.Count > 0 ? Tabs[_apiClientIndex] : null;
        public static string? ActiveCollectionId => Tabs.FirstOrDefault()?.CollectionId;

        public static MudDynamicTabs? DynamicTabs;
        public static List<CollectionTabView> Tabs = new();

        private int _apiClientIndex;
        public int ApiClientIndex => _apiClientIndex;
        private string? _navigationLocation;

        public async Task SetApiClientIndex(int newIndex, bool force = false)
        {
            if (newIndex == _apiClientIndex && !force) return;
            if (newIndex < 0) return;
            if (Tabs.Count == 0) return;
            if (newIndex > Tabs.Count - 1) return;
            if (_apiClientIndex <= Tabs.Count - 1)
            {
                var oldTab = Tabs[_apiClientIndex];
                oldTab.SavedScrollPosition = await Interop.Value.GetCurrentScrollPosition();
            } 
            _apiClientIndex = newIndex;
            var tab = Tabs[_apiClientIndex];
            switch (tab.Type)
            {
                case TabType.HttpCall:
                    _navigationLocation = Navigate.Value.ToHttpCallClass(tab.CollectionId!, tab.ApiClientId!, tab.HttpCallName!, tab.Label!);
                    break;
                case TabType.HttpCalls:
                    _navigationLocation = Navigate.Value.ToHttpCalls(tab.CollectionId!, tab.ApiClientId!);
                    break;
                case TabType.ApiClient:
                    _navigationLocation = Navigate.Value.ToApiClient(tab.CollectionId!, tab.ApiClientId!);
                    break;
                case TabType.Collection:
                    _navigationLocation = Navigate.Value.ToCollection(tab.CollectionId!);
                    break;
                case TabType.CoreCsFile:
                    _navigationLocation = Navigate.Value.ToCoreCsFile(tab.Label);
                    break;
                case TabType.Interface:
                    _navigationLocation = Navigate.Value.ToApiClientInterface(tab.CollectionId!, tab.ApiClientId!);
                    break;
                case TabType.Controller:
                    _navigationLocation = Navigate.Value.ToApiClientController(tab.CollectionId!, tab.ApiClientId!);
                    break;
                case TabType.Tests:
                    _navigationLocation = Navigate.Value.ToApiClientTests(tab.CollectionId!, tab.ApiClientId!);
                    break;
                case TabType.DuplicateRoots:
                    _navigationLocation = Navigate.Value.ToApiClientDuplicateRoots(tab.CollectionId!, tab.ApiClientId!);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (TabsChanged != null)
            {
                await TabsChanged.Invoke();
            }
        }

        private async void OnLocationChanged(object? sender, LocationChangedEventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(_navigationLocation) && args.Location.EndsWith(_navigationLocation) && Tabs[_apiClientIndex].SavedScrollPosition.HasValue)
            {
                await Interop.Value.ScrollToSavedPosition(Tabs[_apiClientIndex].SavedScrollPosition!.Value);
            }
        }

        public async Task SetHome(string? collectionId, bool setIndex = false)
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
                await SetApiClientIndex(0);
            }

            TabsChanged?.Invoke();
        }

        public async Task AddApiClientTab(string collectionId, string apiClientId, string label)
        {
            if (Tabs.FirstOrDefault(x => x.Type == TabType.ApiClient && x.ApiClientId == apiClientId && x.Label == label) is { } tab)
            {
                var index = Tabs.IndexOf(tab);
                if (ApiClientIndex != index)
                    await SetApiClientIndex(index);
                return;
            }
            await SetHome(collectionId);
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
                    await SetApiClientIndex(index);
                return;
            }
            await SetHome(collectionId);
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
                    await SetApiClientIndex(index);
                return;
            }
            await SetHome(collectionId);
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
                    await SetApiClientIndex(index);
                return;
            }
            await SetHome(collectionId);
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
                    await SetApiClientIndex(index);
                return;
            }
            await SetHome(collectionId);
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
                    await SetApiClientIndex(index);
                return;
            }
            var insertIndex = 0;
            if (Tabs.Count >= 1)
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
                    await SetApiClientIndex(index);
                return;
            }
            await SetHome(collectionId);
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
                    await SetApiClientIndex(index);
                return;
            }
            await SetHome(collectionId);
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

        public async Task RemoveTab(string id)
        {
            var tabView = Tabs.SingleOrDefault(t => Equals(t.Id, id));
            if (tabView is not null)
            {
                Tabs.Remove(tabView);
            }
            if (_apiClientIndex > Tabs.Count - 1)
            {
                await SetApiClientIndex(Tabs.Count - 1);
            }
        }

        private async Task MoveToNewTab()
        {
            if (DynamicTabs == null) return;
            TabsChanged?.Invoke();
            await Task.Delay(10);
            if (Tabs.Count <= 0) return;
            if (Tabs.Count == 1) await SetApiClientIndex(0);
            else await SetApiClientIndex(1, true);
        }

        public void Dispose()
        {
            Navigate.Value.LocationChanged -= OnLocationChanged;
        }
    }
}
