using System.Data;
using System.Text;
using WordFinder.Extensions;
using WordFinder.Interfaces;

namespace WordFinder;

public class WordFinder(IEnumerable<string> matrix) : IWordFinder
{
  // TODO: Inject this as service
  private readonly IEnumerable<string> _matrix = matrix;

  // TODO: Inject this service
  private readonly Trie _trie = new();

  public IEnumerable<string> Find(IEnumerable<string> wordStream)
  {
    if (!wordStream.Any())
    {
      return [];
    }

    // If users send repeated words, distinct them
    // var distinctWords = wordStream.Distinct();
    var distinctWords = new HashSet<string>(wordStream);

    // Don't mind the case
    var lowerAdnDistinctWords = distinctWords.Select(w => w.ToLower());

    // TODO: Paralelize
    var wordCount = FindInternal(lowerAdnDistinctWords, _matrix);

    return wordCount.Keys.Take(10);
  }

  // Making this static to avoid null checks due to virtual call sites
  private Dictionary<string, int> FindInternal(IEnumerable<string> wordStream, IEnumerable<string> database)
  {
    var wordCount = new Dictionary<string, int>(wordStream.Count());
    foreach (var w in wordStream)
    {
      _trie.Add(w);
    }

    CountOccurrences(database, wordCount);

    // Hard part need to build the vertical version of the database streams
    var rotatedDb = RotateDatabaseMatrix90Degrees(database);

    CountOccurrences(rotatedDb, wordCount);

    return wordCount.OrderByDescending(item => item.Value).ToDictionary();
  }

  private void CountOccurrences(IEnumerable<string> database, Dictionary<string, int> wordCount)
  {
    foreach (var dbWord in database)
    {
      wordCount.Append(_trie.CountOccurrences(dbWord));
    }
  }

  private int CountWord(string word, IEnumerable<string> database)
  {
    this._trie.Add(word);

    int counter = 0;
    foreach (var row in database)
    {

    }

    return counter;
  }

  /// <summary>
  /// Rotate the matrix database -90 degrees. This is a O(N^2) and could be highly improved if I process the data as it comes
  /// </summary>
  /// <param name="database"></param>
  /// <returns>Rotated database by -90 degrees</returns>
  public static IEnumerable<string> RotateDatabaseMatrix90Degrees(IEnumerable<string> database)
  {
    var localToUseStackMemory = database;
    List<string> rotated = new(localToUseStackMemory.Count());
    for (int i = 0; i < localToUseStackMemory.Count(); i++)
    {
      StringBuilder vertical = new();
      for (int j = 0; j < localToUseStackMemory.Count(); j++)
      {
        vertical.Append(localToUseStackMemory.ElementAt(j)[i]);
      }

      rotated.Add(vertical.ToString());
    }

    return rotated;
  }
}