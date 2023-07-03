using System.Collections.Generic;
namespace MerakiDashboard
{
    public static class HelperExtensions
    {
        public static Dictionary<string, string?> Unique(this Dictionary<string, string?> dict1,
            Dictionary<string, string?> dict2)
        {
            Dictionary<string, string?> result = new (dict1);
            foreach (var kvp in dict2)
            {
                result.TryAdd(kvp.Key, kvp.Value);
            }
            return result;
        }
    }
}