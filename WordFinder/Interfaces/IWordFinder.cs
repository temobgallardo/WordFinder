namespace WordFinder.Interfaces;

public interface IWordFinder
{
  IEnumerable<string> Find(IEnumerable<string> wordStream);
}