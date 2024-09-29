using System.Text;

namespace WordFinder;

public class Trie //: ITrie
{
  private readonly TrieNode _rootNode = new();
  private readonly Dictionary<string, int> _wordsCount = [];

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

  public int CountOnStream(string stream)
  {
    if (string.IsNullOrEmpty(stream))
    {
      return 0;
    }

    return CountOnStreamInternal(stream);
  }

  /// <summary>
  /// This is O(S) where S is the size of stream. CountOnStream is also O(S) the thing here is that we are moving through the array in both algorithms using the same index and hence don't repeat any work twice.
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  public Dictionary<string, int> CountOccurrences(string stream)
  {
    if (string.IsNullOrEmpty(stream))
    {
      return _wordsCount;
    }

    return CountOcurrencesInternal(stream);
  }

  private Dictionary<string, int> CountOcurrencesInternal(string stream)
  {
    for (int i = 0; i < stream.Length; i++)
    {
      // todo: this could be avoided if passed stream to CountOnStream with indexes
      var current = stream[i..];

      var position = CountOnStream(current);

      // avoid processing position already done
      if (position > 0)
      {
        i += position;
      }
    }

    return _wordsCount.OrderByDescending(item => item.Value).ToDictionary();
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