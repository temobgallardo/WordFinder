namespace WordFinder.Workers;

public abstract class WorkerBase(ILogger<WorkerBase> logger) : BackgroundService
{
  protected readonly ILogger<WorkerBase> _logger = logger;
}