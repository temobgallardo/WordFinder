namespace WordFinder;

public class TrieNode
{
  public bool IsWord { get; set; }
  public Dictionary<char, TrieNode> Children { get; } = [];
}