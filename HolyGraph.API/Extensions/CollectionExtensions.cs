namespace HolyGraph.API.Extensions;

public static class CollectionExtensions
{
     // Extension method to add a range of key-value pairs to a dictionary
    public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<KeyValuePair<TKey, TValue>> collection)
    {
        foreach (var kvp in collection)
        {
            dictionary.Add(kvp.Key, kvp.Value);
        }
    }
}