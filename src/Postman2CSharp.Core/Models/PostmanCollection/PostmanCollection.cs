using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Postman2CSharp.Core.Models.PostmanCollection.Authorization;

namespace Postman2CSharp.Core.Models.PostmanCollection
{
    public class PostmanCollection
    {
        [JsonRequired] 
        public required CollectionInfo Info { get; set; }
        public List<CollectionItem>? Item { get; set; }
        public AuthSettings? Auth { get; set; }
        public List<CollectionVariable>? Variable { get; set; }

        public bool IsRoot()
        {
            return Item?.Any(x => x.IsRequestItem) ?? false;
        }
        public List<CollectionItem> GetRootCollections()
        {
            var rootItems = new HashSet<CollectionItem>();
            if (Item == null)
            {
                return rootItems.ToList();
            }

            if (IsRoot())
            {
                rootItems.Add(new CollectionItem
                {
                    Name = Info.Name,
                    Item = Item,
                    Description = Info.Description
                });
            }

            foreach (var item in Item)
            {
                foreach (var root in item.FindRoots())
                {
                    rootItems.Add(root);
                }
            }

            return rootItems.ToList();
        }

        public IEnumerable<CollectionItem> GetAllCollectionItems()
        {
            return GetRootCollections().SelectMany(x => x.RequestItems() ?? new List<CollectionItem>()).ToList();
        }

        /// <summary>
        /// Collection Items where the Auth is null means that item should inherit auth from it's parent. This function
        /// traverse the collection tree and sets the Auth property to the parent's Auth property if it's null.
        /// </summary>
        public void CascadeAuth()
        {
            var rootCollections = GetRootCollections();
            foreach (var rootCollection in rootCollections)
            {
                rootCollection.Auth ??= Auth;
                rootCollection.CascadeAuth();
            }
        }

        public void RemoveRequestsWithoutUrl()
        {
            if (Item == null)
            {
                return;
            }

            foreach (var item in Item)
            {
                item.RemoveItemsWithNullUrls();
            }
        }
    }
}
