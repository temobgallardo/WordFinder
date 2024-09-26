using System.Data;
using WordFinder.Interfaces;

namespace WordFinder;

public class WordFinder(IEnumerable<string> matrix) : IWordFinder
{
  private IEnumerable<string> matrix = matrix;

  public IEnumerable<string> Find(IEnumerable<string> wordStream)
  {
    if (!wordStream.Any())
    {
      return [];
    }

    // If users send repeated words, distinct it 
    var distinctWords = wordStream.Distinct();

    // Parelized work
    SortedDictionary<string, int> wordCount = FindInternal(wordStream);

    return [];
  }

  private SortedDictionary<string, int> FindInternal(IEnumerable<string> wordStream)
  {
    var wordCount = new SortedDictionary<string, int>();
    foreach (var w in wordStream)
    {
      var count = FindIndividualInternal(w);

      wordCount.Add(w, count);
    }

    return wordCount;
  }

  private int FindIndividualInternal(string w)
  {
    return 0;
  }
}