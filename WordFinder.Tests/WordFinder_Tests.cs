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
}