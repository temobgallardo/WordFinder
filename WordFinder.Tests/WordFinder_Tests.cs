using System.Text;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using WordFinder.Interfaces;
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
  public async void FindTopTenWords_Test1()
  {
    var fixture = new Fixture()
            .Customize(new AutoMoqCustomization());

    fixture.Customizations.Add(new CustomStringGenerator("abcdefghijklmnopqrstuvwxyz", 64));
    List<string> matrix = fixture.CreateMany<string>(64).ToList();
    string[] words = ["tri", "tried", "try", "zz", "zood", "zoom", "we", "are", "humanZ"];

    Random wordIndexRamdomizer = new();
    for (int i = 0; i < matrix.Count; i++)
    {
      string row = matrix[i];
      int toGet = wordIndexRamdomizer.Next(words.Length);
      var wordToInsert = words[toGet];
      int toPut = wordIndexRamdomizer.Next(64);

      // Making sure toPut doesn't go out of range
      toPut = toPut + wordToInsert.Length > row.Length ? toPut - wordToInsert.Length : toPut;

      StringBuilder builder = new(row);
      string toReplace = row[toPut..(toPut + wordToInsert.Length)];
      builder.Replace(toReplace, wordToInsert);
      matrix[i] = builder.ToString();
    }

    var requestMatrixService = fixture.Freeze<Mock<IRequestMatrixService>>();
    requestMatrixService.Setup(self => self.GetMatrix(64)).ReturnsAsync(matrix);
    WordFinder sut = new(requestMatrixService.Object, new Trie());

    var actual = await sut.Find(words);
    Assert.True(actual.Any());
  }

  [Fact]
  public async void FindTopTenWords_Test2()
  {
    var fixture = new Fixture()
            .Customize(new AutoMoqCustomization());

    fixture.Customizations.Add(new CustomStringGenerator("abcdefghijklmnopqrstuvwxyz", 64));
    List<string> matrix = fixture.CreateMany<string>(64).ToList();

    // 8 words
    string[] words = ["tri", "tried", "try", "zood", "zoom", "we", "are", "humanZ"];

    PrepareMatrixToTest(matrix, words);

    var requestMatrixService = fixture.Freeze<Mock<IRequestMatrixService>>();
    requestMatrixService.Setup(self => self.GetMatrix(64)).ReturnsAsync(matrix);
    WordFinder sut = new(requestMatrixService.Object, new Trie());

    var actual = await sut.Find(words);

    Assert.True(actual.Any());
    // If number of words changes then this assert must change
    Assert.True(actual.Count() == words.Length);
    Assert.Contains(words[0], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[1], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[2], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[7], actual, StringComparer.InvariantCultureIgnoreCase);
  }

  [Fact]
  public async void FindTopTenWords_Test3()
  {
    var fixture = new Fixture()
            .Customize(new AutoMoqCustomization());

    fixture.Customizations.Add(new CustomStringGenerator("abcdefghijklmnopqrstuvwxyz", 64));
    List<string> matrix = fixture.CreateMany<string>(64).ToList();
    // 14 words
    string[] words = ["tri", "tried", "try", "zood", "zoom", "we", "the", "humanz", "but", "robots", "are", "thinking", "of", "gods"];
    PrepareMatrixToTest(matrix, words);

    var requestMatrixService = fixture.Freeze<Mock<IRequestMatrixService>>();
    requestMatrixService.Setup(self => self.GetMatrix(64)).ReturnsAsync(matrix);
    WordFinder sut = new(requestMatrixService.Object, new Trie());

    var actual = await sut.Find(words);

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
  public async void FindTopTenWords_Test4()
  {
    string[] words = ["tried", "humanz", "robots", "thinking"];
    int ocurrences = 4;
    int matrixRowLenght = 64;
    DatabaseMatrixStringGenerator matrixGen = new(words, ocurrences, _alphabet: "abcdefghijklmnopqrstuvwxyz".ToCharArray(), matrixRowLenght);
    var matrix = matrixGen.GenerateStrings(words.Length);

    var fixture = new Fixture()
            .Customize(new AutoMoqCustomization());

    var requestMatrixService = fixture.Freeze<Mock<IRequestMatrixService>>();
    requestMatrixService.Setup(self => self.GetMatrix(64)).ReturnsAsync(matrix);
    WordFinder sut = new(requestMatrixService.Object, new Trie());

    var actual = await sut.Find(words);

    Assert.True(actual.Any());
    Assert.DoesNotContain("hello", actual, StringComparer.InvariantCultureIgnoreCase);
  }

  [Fact]
  public async void FindTopTenWords_On_Matrix()
  {
    string[] words = ["tried", "humanz", "robots", "thinking", "hi", "love", "peace",
    "carrot", "train", "deepsea", "space", "wonderful"];
    List<string> matrix = [
      "jgfuhumanziefnfjiooftriedkfijoosfjrobotsosdfkojfsdd",
      "gphimkgjithinkingodgmlkgfdfibfhumanzkoopda",
      "fsjhumanzfsdsflfsdmksdmfsd",
      "aiokpddlmfljhihihuhumanzrjg",
      "dsfdhumalovenzjgdfiokapeacepdsgfdhpjifdeepseapkfdgfn",
      "kdupicarrotj[apokf8trainu4humanz2309irqw][aksd]"
    ];

    var fixture = new Fixture()
            .Customize(new AutoMoqCustomization());
    var requestMatrixService = fixture.Freeze<Mock<IRequestMatrixService>>();
    requestMatrixService.Setup(self => self.GetMatrix(64)).ReturnsAsync(matrix);
    WordFinder sut = new(requestMatrixService.Object, new Trie());

    var actual = await sut.Find(words);

    Assert.True(actual.Any());
    Assert.Contains(words[0], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[1], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[2], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[3], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[4], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[5], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[6], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[7], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[8], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.Contains(words[9], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.DoesNotContain("hello", actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.DoesNotContain(words[10], actual, StringComparer.InvariantCultureIgnoreCase);
    Assert.DoesNotContain(words[11], actual, StringComparer.InvariantCultureIgnoreCase);
  }

  [Fact]
  public async void If_NoWords_InMatrix_Then_EmptyResult()
  {
    string[] words = ["train", "space", "wonderful"];
    List<string> matrix = [
      "jgfuhumanziefnfjiooftriedkfijoosfjrobotsosdfkojfsdd",
      "gphimkgjithinkingodgmlkgfdfibfhumanzkoopda",
      "fsjhumanzfsdsflfsdmksdmfsd",
      "aiokpddlmfljhihihuhumanzrjg",
      "dsfdhumalovenzjgdfiokapeacepdsgfdhpjifdeepseapkfdgfn",
      "kdupicarrotj[apokf8grhgfhu4humanz2309irqw][aksd]"
    ];

    var fixture = new Fixture()
            .Customize(new AutoMoqCustomization());
    var requestMatrixService = fixture.Freeze<Mock<IRequestMatrixService>>();
    requestMatrixService.Setup(self => self.GetMatrix(64)).ReturnsAsync(matrix);
    WordFinder sut = new(requestMatrixService.Object, new Trie());

    var actual = await sut.Find(words);

    Assert.False(actual.Any());
  }

  [Fact]
  public async void If_NoWords_Then_EmptyResult()
  {
    string[] words = [];
    List<string> matrix = [
      "jgfuhumanziefnfjiooftriedkfijoosfjrobotsosdfkojfsdd",
      "gphimkgjithinkingodgmlkgfdfibfhumanzkoopda",
      "fsjhumanzfsdsflfsdmksdmfsd",
      "aiokpddlmfljhihihuhumanzrjg",
      "dsfdhumalovenzjgdfiokapeacepdsgfdhpjifdeepseapkfdgfn",
      "kdupicarrotj[apokf8grhgfhu4humanz2309irqw][aksd]"
    ];

    var fixture = new Fixture()
            .Customize(new AutoMoqCustomization());
    var requestMatrixService = fixture.Freeze<Mock<IRequestMatrixService>>();
    requestMatrixService.Setup(self => self.GetMatrix(64)).ReturnsAsync(matrix);
    WordFinder sut = new(requestMatrixService.Object, new Trie());

    var actual = await sut.Find(words);

    Assert.False(actual.Any());
  }

  [Fact]
  public async void If_NoWords_NorMatrix_Then_EmptyResult()
  {
    string[] words = [];
    List<string> matrix = [];

    var fixture = new Fixture()
            .Customize(new AutoMoqCustomization());
    var requestMatrixService = fixture.Freeze<Mock<IRequestMatrixService>>();
    requestMatrixService.Setup(self => self.GetMatrix(64)).ReturnsAsync(matrix);
    WordFinder sut = new(requestMatrixService.Object, new Trie());

    var actual = await sut.Find(words);

    Assert.False(actual.Any());
  }

  [Fact]
  public async void If_NullWords_NorMatrix_Then_EmptyResult()
  {
    string[]? words = null;
    List<string> matrix = [];

    var fixture = new Fixture()
            .Customize(new AutoMoqCustomization());
    var requestMatrixService = fixture.Freeze<Mock<IRequestMatrixService>>();
    requestMatrixService.Setup(self => self.GetMatrix(64)).ReturnsAsync(matrix);
    WordFinder sut = new(requestMatrixService.Object, new Trie());

    var actual = await sut.Find(words);

    Assert.False(actual.Any());
  }

  [Fact]
  public void If_NullMatrix_Then_NullReferenceException()
  {
    string[]? words = ["a", "b"];
    List<string> matrix = null;

    var fixture = new Fixture()
            .Customize(new AutoMoqCustomization());
    var requestMatrixService = fixture.Freeze<Mock<IRequestMatrixService>>();
    requestMatrixService.Setup(self => self.GetMatrix(64)).ReturnsAsync(matrix);
    WordFinder sut = new(requestMatrixService.Object, new Trie());

    Assert.ThrowsAsync<NullReferenceException>(async () => await sut.Find(words));
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