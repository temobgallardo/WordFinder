using System.Text;
using WordFinder.Interfaces;

namespace WordFinder.MockServices;

public class RequestMatrixService : IRequestMatrixService
{
  private readonly int _rowLenght = 64;
  private readonly int _ocurrences = 5;
  private static readonly Random random = new();
  private readonly char[] chars = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
  private readonly string[] _specialWords = ["choose", "words", "to", "add", "tried", "humanz", "robots", "thinking", "hi", "love", "peace",
    "carrot", "train", "deepsea", "space", "wonderful"];

  public async Task<IEnumerable<string>> GetMatrix(int count = 64) => await Task.FromResult(GenerateStrings(count));

  private IEnumerable<string> GenerateStrings(int count)
  {
    for (int i = 0; i < count; i++)
    {
      yield return GenerateString();
    }
  }

  private string GenerateString()
  {
    var result = new StringBuilder(new string(Enumerable.Repeat(chars, _rowLenght).Select(s => s[random.Next(s.Length)]).ToArray()));

    foreach (var _ in _specialWords)
    {
      var indForWord = random.Next(_specialWords.Length);
      var word = _specialWords[indForWord];
      int position = 0;
      var ocurrences = random.Next(_ocurrences);
      for (int i = 0; i < ocurrences; i++)
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