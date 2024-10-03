namespace WordFinder.Tests;

public class Trie_Tests
{
    [Fact]
    public void CountOcurrences_Test1()
    {
        Trie sut = new();
        var input = "adgoodadjiidgoodgioagood";
        string[] words = ["good", "go", "and"];

        foreach (var w in words)
        {
            sut.Add(w);
        }

        var wordsCounter = sut.DeepSearch(input);

        Assert.Equal(3, wordsCounter["go"]);
        Assert.Equal(3, wordsCounter["good"]);
        Assert.DoesNotContain("and", wordsCounter);
    }

    [Fact]
    public void If_WordsAreNull_Should_ReturnEmtpy()
    {
        Trie sut = new();
        var input = "adgoodadjiidgoodgioagood";
        string[] words = [];

        foreach (var w in words)
        {
            sut.Add(w);
        }

        var wordsCounter = sut.DeepSearch(input);

        Assert.Empty(wordsCounter);
    }

    [Fact]
    public void If_InputIsNullOrEmpty_Should_ReturnEmtpy()
    {
        Trie sut = new();
        var input = "";
        string[] words = ["tree", "animal"];

        foreach (var w in words)
        {
            sut.Add(w);
        }

        var wordsCounter = sut.DeepSearch(input);

        Assert.Empty(wordsCounter);
    }

    [Theory]
    [ClassData(typeof(TrieTestData))]
    public void CountOcurrences_Test2(TrieTestData testData)
    {
        Trie sut = new();
        var input = testData.StreamInput;
        string[] words = testData.InputData;

        foreach (var w in words)
        {
            sut.Add(w);
        }

        var actualWordResult = sut.DeepSearch(input);
        var actual = actualWordResult.Keys.ToArray();
        Assert.True(testData.StringCounterResult.Count == actual.Length);
        foreach (var expected in testData.StringCounterResult.Keys)
        {
            Assert.Contains(expected, actual);
        }
    }

    [Fact]
    public void Add_Test1()
    {
        Trie sut = new();
        string[] words = ["good", "go", "and"];
        int expectedNumberOfAddedWords = words.Length;

        foreach (var w in words)
        {
            sut.Add(w);
        }

        int actualNumberOfAddedWords = 0;
        foreach (var w in words)
        {
            if (sut.Search(w))
            {
                actualNumberOfAddedWords++;
            }
        }

        Assert.Equal(actualNumberOfAddedWords, expectedNumberOfAddedWords);
    }
}