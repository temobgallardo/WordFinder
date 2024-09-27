using System.Globalization;

namespace WordFinder;

public class Trie //: ITrie
{
  private readonly TrieNode _rootNode = new();

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

  public bool CountOnStream(string stream, int startSearchIndex, int endSearchIndex, out int stopIndex)
  {
    if (startSearchIndex < 0 || startSearchIndex > stream.Length || endSearchIndex < 0 || endSearchIndex > stream.Length)
    {
      throw new IndexOutOfRangeException($"Either {nameof(startSearchIndex)} or {nameof(endSearchIndex)} are out of bounds.");
    }

    var node = _rootNode;
    stopIndex = 0;
    for (int i = startSearchIndex; i < endSearchIndex; i++)
    {
      stopIndex = i;

      if (!node.Children.TryGetValue(stream[i], out TrieNode? value))
      {
        return false;
      }

      if (node.IsWord)
      {
        return true;
      }

      node = value;
    }

    return node.IsWord;
  }

  public int CountOccurrences(string stream)
  {
    int counter = 0;
    for (int i = 0; i < stream.Length;)
    {
      var current = stream.Substring(i, stream.Length);
      if (CountOnStream(current, 0, current.Length, out i))
      {
        counter++;
      }
    }

    return counter;
  }
}