using AutoFixture.Kernel;

namespace WordFinder.Tests.Generators;

public class CustomStringGenerator(string allowedChars, int size) : ISpecimenBuilder
{
  private readonly int _size = size;
  private readonly string _allowedChars = allowedChars;

  public object Create(object request, ISpecimenContext context)
  {
    if (request is Type type && type == typeof(string))
    {
      var random = new Random();

      return new string(Enumerable.Repeat(_allowedChars, _size).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    return new NoSpecimen();
  }
}
