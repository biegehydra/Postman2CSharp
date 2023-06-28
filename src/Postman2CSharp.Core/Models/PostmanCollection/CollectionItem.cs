using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Postman2CSharp.Core.Models.PostmanCollection.Http.Request;
using Postman2CSharp.Core.Models.PostmanCollection.Http.Response;

namespace Postman2CSharp.Core.Models.PostmanCollection
{
    public class CollectionItem
    {
        [JsonRequired] public required string Name { get; set; }
        public List<CollectionItem>? Item { get; set; }
        public List<Response>? Response { get; set; }
        public Request? Request { get; set; }
        public string? Description { get; set; }
        public bool IsGroupingItem => Request == null && Response == null;
        public bool IsRequestItem => Request != null;

        public IEnumerable<CollectionItem>? RequestItems() => Item?.Where(x => x.IsRequestItem);
        /// <summary>
        /// Finds all folders in hierarchy that have requests. Inclusive, checks itself
        /// </summary>
        /// <param name="root">Root collection item in hierarchy</param>
        /// <returns></returns>
        public List<CollectionItem> FindRoots(CollectionItem root)
        {
            var rootItems = new List<CollectionItem>();

            if (root.Item == null)
            {
                return rootItems;
            }

            if (root.HasRequests())
            {
                rootItems.Add(this);
            }


            foreach (var child in root.Item)
            {
                if (child.HasRequests())
                {
                    rootItems.Add(child);
                }
                else
                {
                    rootItems.AddRange(FindRoots(child));
                }
            }

            return rootItems;
        }

        public bool HasRequests()
        {
            return Item?.Any(x => x.IsRequestItem) ?? false;
        }
    }
}