namespace WordFinder.Interfaces;

public interface IWordFinder
{
  Task<IEnumerable<string>> Find(IEnumerable<string> wordStream);
}