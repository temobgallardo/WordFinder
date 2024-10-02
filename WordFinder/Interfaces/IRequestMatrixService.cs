namespace WordFinder.Interfaces;

public interface IRequestMatrixService
{
  Task<IEnumerable<string>> GetMatrix(int count = 64);
}