using WordFinder.Interfaces;

namespace WordFinder.MockServices;

public class RequestWordsToSearchService : IRequestWordsToSearchService
{
  private static readonly string[] words = ["lets", "find", "some", "words", "choose", "words", "to", "add", "tried", "humanz", "robots", "thinking", "hi", "love", "peace",
    "carrot", "train", "deepsea", "space", "wonderful"];

  public async Task<IEnumerable<string>> GetWords()
  {
    return await Task.FromResult(GetRandomWords());
  }

  private static IEnumerable<string> GetRandomWords()
  {
    Random random = new();
    var numberOfWordsToSearch = random.Next(words.Length);
    for (int i = 0; i < numberOfWordsToSearch; i++)
    {
      var index = random.Next(words.Length);
      yield return words[index];
    }
  }
}