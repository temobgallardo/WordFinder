using System.Collections;

namespace WordFinder.Tests;

public class TrieTestData : IEnumerable<object[]>
{
  public required string[] InputData { get; set; }
  public required string StreamInput { get; set; }
  public required Dictionary<string, int> StringCounterResult { get; set; }

  public IEnumerator<object[]> GetEnumerator()
  {
    yield return new object[] {
      new TrieTestData() {
        InputData = ["call", "cause", "ria", "re", "return", "bought"],
        StreamInput = "jijcallcallgiuhqriareturnjijjreriaari",
        StringCounterResult = new Dictionary<string, int> {
          {"call", 2 },
          { "ria", 2 },
          { "re", 2 },
          { "return", 1 },
        }
      } };
    yield return new TrieTestData[] {new()
      {
        InputData = ["call", "cause", "ria", "re", "return", "bought",  "house"],
        StreamInput = "jiboughtjcallc1allgiuhqriareturnjijjreriaari",
        StringCounterResult = new Dictionary<string, int> {
          {"call", 1 },
          { "ria", 2 },
          { "re", 2 },
          { "return", 1 },
          { "bought", 1 },
        }
      } };
    yield return new TrieTestData[]{
      new(){
        InputData = ["a"],
        StreamInput = "aaaaaaaaaaaaaaaaaaaaab",
        StringCounterResult = new Dictionary<string, int> {
          {"a", 21 }
        }
      } };
    yield return new TrieTestData[]{
      new(){
        InputData = ["a", "b", "c"],
        StreamInput = "zaaaaaaaaaaaaaaaaaaaaab",
        StringCounterResult = new Dictionary<string, int> {
          {"a", 21 },
          {"b", 1 },
        }
      } };
    yield return new TrieTestData[]{
      new(){
        InputData = ["a", "b", "cat"],
        StreamInput = "zcataaaaaaaaaaaaaaaaaaaab",
        StringCounterResult = new Dictionary<string, int> {
          {"a", 20 },
          {"b", 1 },
          {"cat", 1 },
        }
      } };
  }

  IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}