using System.Data;
using System.Text;
using WordFinder.Extensions;
using WordFinder.Interfaces;

namespace WordFinder;

public class WordFinder(IRequestMatrixService requestMatrixService, ITrie<string> trie) : IWordFinder
{
  private IEnumerable<string> _matrix = [];
  private readonly ITrie<string> _trie = trie;

  public async Task<IEnumerable<string>> Find(IEnumerable<string> wordStream)
  {
    if (wordStream is null || !wordStream.Any())
    {
      return [];
    }

    if (!_matrix.Any())
    {
      _matrix = await requestMatrixService.GetMatrix();
    }

    // If users send repeated words, distinct them
    // var distinctWords = wordStream.Distinct();
    var distinctWords = new HashSet<string>(wordStream);

    // lower case
    var lowerAdnDistinctWords = distinctWords.Select(w => w.ToLower());

    var wordCount = FindInternal(lowerAdnDistinctWords, _matrix);

    return wordCount.Keys.Take(10);
  }

  private Dictionary<string, int> FindInternal(IEnumerable<string> wordStream, IEnumerable<string> database)
  {
    var wordCount = new Dictionary<string, int>(wordStream.Count());
    foreach (var w in wordStream)
    {
      _trie.Add(w);
    }

    // CountOccurrences(database, wordCount);
    CountOccurrencesParallel(database, wordCount);

    // building the vertical version of the database streams
    var rotatedDb = RotateDatabaseMatrix90Degrees(database);

    // CountOccurrences(rotatedDb, wordCount);
    CountOccurrencesParallel(rotatedDb, wordCount);

    return wordCount.OrderByDescending(item => item.Value).ToDictionary();
  }

  private void CountOccurrences(IEnumerable<string> database, Dictionary<string, int> wordCount)
  {
    foreach (var dbWord in database)
    {
      wordCount.Append(_trie.DeepSearch(dbWord));
    }
  }
  /// <summary>
  /// Search the words into the database parellaly. THis is possible due because each row of the database is independent of each other
  /// </summary>
  /// <param name="database">Array of strings to search in</param>
  /// <param name="wordCount">Result of the number of words found in <paramref name="database"/></param>
  private void CountOccurrencesParallel(IEnumerable<string> database, Dictionary<string, int> wordCount)
    => Parallel.ForEach(database, row => wordCount.Append(_trie.DeepSearch(row)));

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