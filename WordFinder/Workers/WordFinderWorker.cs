using WordFinder.Interfaces;

namespace WordFinder.Workers;

public class WordFinderWorker(ILogger<WordFinderWorker> logger, IWordFinder _wordFinderServiec, IRequestWordsToSearchService _requestWordsService) : WorkerBase(logger)
{
  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    this._logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

    IEnumerable<string> wordsToFind = await _requestWordsService.GetWords();
    var wordsFound = await _wordFinderServiec.Find(wordsToFind);

    this._logger.LogInformation("Proocessed - Words Found: {@WordsFound}", wordsFound);

    this._logger.LogInformation("Work completed at: {time}", DateTime.Now);
  }
}
