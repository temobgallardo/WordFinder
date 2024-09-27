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
    // Don't mind the case
    var lowerAdnDistinctWords = distinctWords.Select(w => w.ToLower());

    // Parelized work
    SortedDictionary<string, int> wordCount = FindInternal(lowerAdnDistinctWords, matrix);

    return [];
  }

  // Making this static to avoid null checks due to virtual call sites
  private static SortedDictionary<string, int> FindInternal(IEnumerable<string> wordStream, IEnumerable<string> database)
  {
    var wordCount = new SortedDictionary<string, int>();
    foreach (var w in wordStream)
    {
      var count = CountWord(w, database);

      wordCount.Add(w, count);
    }

    return wordCount;
  }

  private static int CountWord(string word, IEnumerable<string> database)
  {
    int counter = 0;
    foreach (var row in database)
    {
      if (row.Contains(word))
      {
        counter += 0;
      }

    }

    return counter;
  }

}