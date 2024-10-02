using System.Text;
using WordFinder.Interfaces;

namespace WordFinder;

public class Trie : ITrie<string>
{
  private readonly TrieNode _rootNode = new();
  private readonly Dictionary<string, int> _wordsCount = [];

  public Dictionary<string, int> WordCount { get => _wordsCount; }

  public void Add(string word)
  {
    var node = _rootNode;

    foreach (char c in word)
    {
      if (!node.Children.TryGetValue(c, out TrieNode? value))
      {
        value = new TrieNode();
        node.Children.Add(c, value);
      }

      node = value;
    }

    _wordsCount.Add(word, 0);
    node.IsWord = true;
  }

  public bool Search(string word)
  {
    var node = _rootNode;

    foreach (char c in word)
    {
      if (!node.Children.TryGetValue(c, out TrieNode? value))
      {
        return false;
      }

      node = value;
    }

    return node.IsWord;
  }

  /// <summary>
  /// Searches the words in the Trie within the stream.
  /// </summary>
  /// <param name="toSearchIn"></param>
  /// <returns></returns>
  public Dictionary<string, int> DeepSearch(string toSearchIn)
  {
    if (string.IsNullOrEmpty(toSearchIn))
    {
      return [];
    }

    return CountOcurrencesInInternal(toSearchIn);
  }

  /// <summary>
  /// This is O(S) where S is the size of <paramref name="stream"/>. This is moving through the array in using a control index. 
  /// </summary>
  /// <param name="stream">the string to look for words</param>
  /// <returns>Words found and number of ocurrences</returns>
  private Dictionary<string, int> CountOcurrencesInInternal(string stream)
  {
    for (int i = 0; i < stream.Length; i++)
    {
      // TODO: can be avoided if passed stream and i index
      var current = stream[i..];

      var position = CountOnStream(current);

      // Any movement in the Counting? Check following chars in the stream
      if (position > 0)
      {
        i += position;
      }
    }

    return _wordsCount.Where(item => item.Value > 0).OrderByDescending(item => item.Value).ToDictionary();
  }

  private int CountOnStream(string stream)
  {
    if (string.IsNullOrEmpty(stream))
    {
      return 0;
    }

    return CountOnStreamInternal(stream);
  }

  private int CountOnStreamInternal(string stream)
  {
    int i = -1;
    var node = _rootNode;
    StringBuilder wordBuilder = new();

    foreach (var w in stream)
    {
      if (!node.TryGetNode(w, out TrieNode? crntNode))
      {
        return i;
      }

      wordBuilder.Append(w);
      if (crntNode.IsWord)
      {
        // found a word, count it
        var word = wordBuilder.ToString();
        _wordsCount[word]++;
      }

      i++;

      // keep going through the tree
      node = crntNode;
    }

    return i;
  }
}