namespace WordFinder.Tests;

public class Trie_Tests
{
    [Fact]
    public void CountOcurrences_Test1()
    {
        Trie sut = new();
        var input = "adgoodadjiidgoodgioagood";
        string[] words = ["good", "go", "and"];
        int actualCountGo = 3;
        int actualCountAnd = 0;

        foreach (var w in words)
        {
            sut.Add(w);
        }

        var wordsCounter = sut.DeepSearch(input);

        Assert.Equal(wordsCounter["go"], actualCountGo);
        Assert.Equal(wordsCounter["good"], actualCountGo);
        Assert.Equal(wordsCounter["and"], actualCountAnd);
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

        var expected = sut.DeepSearch(input);

        foreach (var w in words)
        {
            Assert.Equal(expected[w], testData.StringCounterResult[w]);
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