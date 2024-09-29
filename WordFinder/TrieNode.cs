using WordFinder.Interfaces;

namespace WordFinder;

public class TrieNode //: ITrieNode<string>
{
  public bool IsWord { get; set; }
  public Dictionary<char, TrieNode> Children { get; } = [];

  public bool TryGetNode(char prefix, out TrieNode? node) => Children.TryGetValue(prefix, out node);
  // public string Payload { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

  // public Dictionary<char, ITrieNode<string>> Children => [];
}