namespace WordFinder.Interfaces;

public interface ITrie<T>
{
  public Dictionary<string, int> WordCount { get; }
  /// <summary>
  /// Add a word to the Trie
  /// </summary>
  /// <param name="word">the word to add</param>
  void Add(T word);
  /// <summary>
  /// Return true if the word is found in the Trie. Otherwise, false
  /// </summary>
  /// <param name="word">Word to be searched in the Trie</param>
  /// <returns>True if <paramref name="word"/> is found in the Trie</returns>
  bool Search(T word);
  /// <summary>
  /// Counts the number of times that a Trie word is contained in <paramref name="toSearchIn"/>
  /// </summary>
  /// <param name="toSearchIn">Word to search occurrences of a Trie word</param>
  /// <returns>All occurences of Trie words in the <paramref name="toSearchIn"/></returns>
  Dictionary<string, int> DeepSearch(T toSearchIn);
}