namespace WordFinder.Interfaces;

public interface ITrieNode<T>
{
  bool IsWord { get; set; }

  Dictionary<char, TrieNode> Children { get; }

  bool TryGetNode(char prefix, out TrieNode? node);
}