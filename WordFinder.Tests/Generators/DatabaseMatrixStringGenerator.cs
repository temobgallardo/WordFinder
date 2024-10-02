using System.Text;

namespace WordFinder.Tests.Generators;

public class DatabaseMatrixStringGenerator(string[] _specialWords, int _ocurrences, char[] _alphabet, int _rowLenght)
{
  private static readonly Random random = new();
  private readonly char[] chars = _alphabet ?? "abcdefghijklmnopqrstuvwxyz".ToCharArray();

  public IEnumerable<string> GenerateStrings(int count)
  {
    for (int i = 0; i < count; i++)
    {
      yield return GenerateString(count);
    }
  }

  private string GenerateString(int count)
  {
    var result = new StringBuilder(new string(Enumerable.Repeat(chars, _rowLenght).Select(s => s[random.Next(s.Length)]).ToArray()));

    foreach (var word in _specialWords)
    {
      int position = 0;
      for (int i = 0; i < _ocurrences; i++)
      {
        position += word.Length + random.Next(word.Length);

        if (position > _rowLenght - word.Length)
        {
          break;
        }

        result.Remove(position, word.Length);
        result.Insert(position, word);
      }
    }

    return result.ToString();
  }
}