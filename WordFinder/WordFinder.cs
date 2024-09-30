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
    var distinctWords = wordStream.Distinct();
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
  /// The database is defined as a 64x64 sized matrix, hence we can use that to make this operation very efficient
  /// </summary>
  /// <param name="database"></param>
  /// <returns></returns>
  public static IEnumerable<string> RotateDatabaseMatrix90Degrees(IEnumerable<string> database)
  {
    var localToUseStackMemory = database;
    List<string> rotated = new(64);
    for (int i = 0; i < localToUseStackMemory.Count(); i++)
    {
      StringBuilder vertical = new();
      vertical.Append(localToUseStackMemory.ElementAt(0)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(1)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(2)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(3)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(4)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(5)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(6)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(7)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(8)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(9)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(10)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(11)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(12)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(13)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(14)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(15)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(16)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(17)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(18)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(19)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(20)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(21)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(22)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(23)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(24)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(25)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(26)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(27)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(28)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(29)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(30)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(31)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(32)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(33)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(34)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(35)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(36)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(37)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(38)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(39)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(40)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(41)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(42)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(43)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(44)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(45)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(46)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(47)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(48)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(49)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(50)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(51)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(52)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(53)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(54)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(55)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(56)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(57)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(58)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(59)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(60)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(61)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(62)[i]);
      vertical.Append(localToUseStackMemory.ElementAt(63)[i]);

      rotated.Add(vertical.ToString());
    }

    return rotated;
  }
}