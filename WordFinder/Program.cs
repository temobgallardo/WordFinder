using Serilog;
using WordFinder;
using WordFinder.Interfaces;
using WordFinder.MockServices;
using WordFinder.Workers;

Log.Logger = new LoggerConfiguration()
#if DEBUG
      .WriteTo.Console()
#else
     .WriteTo.File(
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nameof(WordFinderWorker) + "-service.log")
    )
#endif
    .CreateLogger();

var host = Host.CreateDefaultBuilder(args)
    .UseSerilog()
    .ConfigureServices(services =>
    {
      services.AddSingleton<IRequestMatrixService, RequestMatrixService>();
      services.AddSingleton<IRequestWordsToSearchService, RequestWordsToSearchService>();
      services.AddSingleton<ITrie<string>, Trie>();
      services.AddScoped<IWordFinder, WordFinder.WordFinder>();
      services.AddHostedService<WordFinderWorker>();
    })
    .Build();

await host.RunAsync();
