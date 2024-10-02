namespace WordFinder.Interfaces;

public interface IRequestWordsToSearchService
{
  Task<IEnumerable<string>> GetWords();
}