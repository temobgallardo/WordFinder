using WordFinder.Interfaces;

namespace WordFinder.MockServices;

public class RequestWordsToSearchService : IRequestWordsToSearchService
{
  private static readonly string[] words = ["lets", "find", "some", "words", "thinking", "hi", "love", "peace",
    "carrot", "train", "deepsea", "space", "wonderful"];

  public async Task<IEnumerable<string>> GetWords() => await Task.FromResult(words);
}