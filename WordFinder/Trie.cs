namespace WordFinder;

public class Trie
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
}