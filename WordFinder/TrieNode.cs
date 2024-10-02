using WordFinder.Interfaces;

namespace WordFinder;

public class TrieNode : ITrieNode<string>
{
  public bool IsWord { get; set; }

  public Dictionary<char, TrieNode> Children { get; private set; } = [];

  public bool TryGetNode(char prefix, out TrieNode? node) => Children.TryGetValue(prefix, out node);
}