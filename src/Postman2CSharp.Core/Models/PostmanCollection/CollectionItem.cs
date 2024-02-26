using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Postman2CSharp.Core.Models.PostmanCollection.Authorization;
using Postman2CSharp.Core.Models.PostmanCollection.Http.Request;
using Postman2CSharp.Core.Models.PostmanCollection.Http.Response;
using Postman2CSharp.Core.Utilities;

namespace Postman2CSharp.Core.Models.PostmanCollection
{
    public class CollectionItem : IDirtyName
    {
        [JsonRequired] public required string Name { get; set; }
        public List<CollectionItem>? Item { get; set; }
        public List<Response>? Response { get; set; }
        public Request? Request { get; set; }
        public AuthSettings? Auth { get; set; }
        public string? Description { get; set; }
        public bool IsGroupingItem => Request == null && Response == null;
        public bool IsRequestItem => Request != null;

        public IEnumerable<CollectionItem>? RequestItems() => Item?.Where(x => x.IsRequestItem);
        /// <summary>
        /// Finds all folders in hierarchy that have requests. Inclusive, checks itself
        /// </summary>
        /// <param name="root">Root collection item in hierarchy</param>
        /// <returns></returns>
        public List<CollectionItem> FindRoots()
        {
            var rootItems = new HashSet<CollectionItem>();

            if (Item == null)
            {
                return rootItems.ToList();
            }

            if (HasRequests())
            {
                rootItems.Add(this);
            }


            foreach (var child in Item)
            {
                rootItems.UnionWith(child.FindRoots());
            }

            return rootItems.ToList();
        }

        public bool HasRequests()
        {
            return Item?.Any(x => x.IsRequestItem) ?? false;
        }

        public void CascadeAuth()
        {
            if (Item == null)
            {
                return;
            }

            foreach (var item in Item)
            {
                item.Auth ??= Auth;
                if (item.Request != null)
                {
                    item.Request.Auth ??= item.Auth;
                }
                item.CascadeAuth();
            }
        }

        public void RemoveItemsWithNullUrls()
        {
            if (Request is {Url: null})
            {
                Request = null;
            }

            if (Item == null)
            {
                return;
            }

            foreach (var item in Item)
            {
                item.RemoveItemsWithNullUrls();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<CollectionItem>? ReduceNestingRecursive(List<CollectionItem> parentItems, int maxDepth, int currentLevel)
        {
            if (IsRequestItem && currentLevel >= maxDepth)
            {
                return [this];
            }

            if (Item == null || Item.Count == 0) return null;

            var initialItems = Item.ToArray();
            List<CollectionItem>? itemsToMoveUp = null;
            foreach (var item in initialItems)
            {
                var itemsToMoveUp2 = item.ReduceNestingRecursive(Item, maxDepth, currentLevel + 1);
                if (itemsToMoveUp2 == null) continue;
                if (itemsToMoveUp == null)
                {
                    itemsToMoveUp = itemsToMoveUp2;
                }
                else
                {
                    itemsToMoveUp.AddRange(itemsToMoveUp2);
                }

            }
            if (currentLevel >= maxDepth - 1)
            {
                parentItems.Remove(this);
                Item.Clear();
            }

            if (currentLevel == maxDepth - 1 && itemsToMoveUp != null)
            {
                parentItems.AddRange(itemsToMoveUp);
                return null;
            }

            return itemsToMoveUp;
        }

        public int CalculateNesting()
        {
            if (Item == null || Item.Count == 0)
            {
                return 1;
            }
            int max = 1;
            foreach (var item in Item)
            {
                int nesting = item.CalculateNesting();
                if (nesting > max)
                {
                    max = nesting;
                }
            }
            return max + 1;
        }
    }
}