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
    }
}