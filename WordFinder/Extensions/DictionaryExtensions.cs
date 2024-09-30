namespace WordFinder.Extensions;

public static class DictionaryExtensions
{
  public static void Append<K, V>(this Dictionary<K, V> self, Dictionary<K, V> toAdd) where K : notnull
  {
    if (self is null) { throw new NullReferenceException(nameof(self)); }

    if (toAdd is null) { throw new NullReferenceException(nameof(toAdd)); }

    foreach (var item in toAdd)
    {
      self[item.Key] = item.Value;
    }
  }
}