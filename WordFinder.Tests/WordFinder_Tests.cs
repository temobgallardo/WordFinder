using System.Text;
using AutoFixture;
using WordFinder.Tests.Generators;

namespace WordFinder.Tests;

public class WordFinder_Tests
{
  [Fact]
  public void RotateMatrix_Test1()
  {
    Fixture dataCreator = new();
    dataCreator.Customizations.Add(new CustomStringGenerator("abcdefghijklmnopqrstuvwxyz", 64));
    List<string> matrix = dataCreator.CreateMany<string>(64).ToList();

    var actual = WordFinder.RotateDatabaseMatrix90Degrees(matrix);

    Assert.True(actual.Any());
    Assert.True(actual.Count() == 64);
  }

  [Fact]
  public void FindTopTenWords_Test1()
  {
    Fixture dataCreator = new();
    dataCreator.Customizations.Add(new CustomStringGenerator("abcdefghijklmnopqrstuvwxyz", 64));
    List<string> matrix = dataCreator.CreateMany<string>(64).ToList();
    WordFinder sut = new(matrix);
    string[] words = ["tri", "tried", "try", "zz", "zood", "zoom", "we", "are", "humanZ"];

    Random wordIndexRamdomizer = new();
    for (int i = 0; i < matrix.Count; i++)
    {
      string row = matrix[i];
      int toGet = wordIndexRamdomizer.Next(words.Length);
      var wordToInsert = words[toGet];
      int toPut = wordIndexRamdomizer.Next(64);

      // Normilize toPut index so it does not put wordToGet in the row out of its range
      toPut = toPut + wordToInsert.Length > row.Length ? toPut - wordToInsert.Length : toPut;

      StringBuilder builder = new(row);
      String toReplace = row[toPut..(toPut + wordToInsert.Length)];
      builder.Replace(toReplace, wordToInsert);
      matrix[i] = builder.ToString();
    }

    var actual = sut.Find(words);
    Assert.True(actual.Any());
  }

  [Fact]
  public void FindTopTenWords_Test2()
  {
    Fixture dataCreator = new();
    dataCreator.Customizations.Add(new CustomStringGenerator("abcdefghijklmnopqrstuvwxyz", 64));
    List<string> matrix = dataCreator.CreateMany<string>(64).ToList();
    WordFinder sut = new(matrix);
    // 8 words
    string[] words = ["tri", "tried", "try", "zood", "zoom", "we", "are", "humanZ"];



    var actual = sut.Find(words);
    Assert.True(actual.Any());
    // If number of words changes then this assert must change
    Assert.True(actual.Count() == words.Length);
    Assert.Contains(words[0], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[1], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[2], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[7], actual, StringComparer.InvariantCultureIgnoreCase);
  }

  [Fact]
  public void FindTopTenWords_Test3()
  {
    Fixture dataCreator = new();
    dataCreator.Customizations.Add(new CustomStringGenerator("abcdefghijklmnopqrstuvwxyz", 64));
    List<string> matrix = dataCreator.CreateMany<string>(64).ToList();
    WordFinder sut = new(matrix);
    // 14 words
    string[] words = ["tri", "tried", "try", "zood", "zoom", "we", "are", "humanz", "but", "robots", "are", "thinking", "of", "gods"];

    Random wordIndexRamdomizer = new();
    for (int i = 0, j = 0; i < matrix.Count; i++, j++)
    {
      string row = matrix[i];
      j = j >= words.Length ? 0 : j;
      var wordToInsert = words[j];
      int toPut = wordIndexRamdomizer.Next(64);

      // Normilize toPut index so it does not put wordToGet in the row out of its range
      toPut = toPut + wordToInsert.Length > row.Length ? toPut - wordToInsert.Length : toPut;

      StringBuilder builder = new(row);
      String toReplace = row[toPut..(toPut + wordToInsert.Length)];
      builder.Replace(toReplace, wordToInsert.ToLower());
      matrix[i] = builder.ToString();
    }

    var actual = sut.Find(words);
    Assert.True(actual.Any());
    Assert.Contains(words[0], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[1], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[2], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[3], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[4], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[5], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[6], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[7], actual, StringComparer.InvariantCultureIgnoreCase);
  }

  [Fact]
  public void FindTopTenWords_Test4()
  {
    Fixture dataCreator = new();
    dataCreator.Customizations.Add(new CustomStringGenerator("abcdefghijklmnopqrstuvwxyz", 64));
    List<string> matrix = dataCreator.CreateMany<string>(64).ToList();
    WordFinder sut = new(matrix);
    // 14 words
    string[] words = ["tri", "tried", "try", "zood", "zoom", "we", "are", "humanz", "but", "robots", "are", "thinking", "of", "gods"];

    PrepareMatrixToTest(matrix, words);

    var actual = sut.Find(words);
    Assert.True(actual.Any());
    Assert.Contains(words[0], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[1], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[2], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[3], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[4], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[5], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[6], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[7], actual, StringComparer.InvariantCultureIgnoreCase);
  }

  private static void PrepareMatrixToTest(List<string> matrix, string[] words)
  {
    Random wordIndexRamdomizer = new();
    for (int i = 0, j = 0; i < matrix.Count; i++, j++)
    {
      string row = matrix[i];
      j = j >= words.Length ? 0 : j;
      var wordToInsert = words[j];
      int toPut = wordIndexRamdomizer.Next(64);

      // Normilize toPut index so it does not put wordToGet in the row out of its range
      toPut = toPut + wordToInsert.Length > row.Length ? toPut - wordToInsert.Length : toPut;

      StringBuilder builder = new(row);
      String toReplace = row[toPut..(toPut + wordToInsert.Length)];
      builder.Replace(toReplace, wordToInsert.ToLower());
      matrix[i] = builder.ToString();
    }
  }
}