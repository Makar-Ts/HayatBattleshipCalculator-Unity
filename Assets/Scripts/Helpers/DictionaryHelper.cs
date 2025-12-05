using System.Collections.Generic;

public class DictionaryHelper
{
    public static Dictionary<TKey, TValue> Merge<TKey, TValue>(
        Dictionary<TKey, TValue> dict1, Dictionary<TKey, TValue> dict2)
    {
        var mergedDictionary = new Dictionary<TKey, TValue>(dict1);

        foreach (var kvp in dict2)
        {
            if (mergedDictionary.ContainsKey(kvp.Key))
            {
                mergedDictionary[kvp.Key] = kvp.Value; 
            }
            else
            {
                mergedDictionary.Add(kvp.Key, kvp.Value);
            }
        }
        return mergedDictionary;
    }
}