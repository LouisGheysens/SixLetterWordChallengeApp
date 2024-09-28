using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SixLetterWordChallengeApp.Abstractions;
using SixLetterWordChallengeApp.Models;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureAppConfiguration((context, config) =>
{
    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
});

builder.ConfigureServices((context, services) =>
{
    services.Configure<FileSettings>(context.Configuration.GetSection(nameof(FileSettings)));
    services.Configure<CombinationSettings>(context.Configuration.GetSection(nameof(CombinationSettings)));

    services.AddTransient<IFileReaderService, FileReaderService>();
    services.AddTransient<IWordCombinationService, WordCombinationService>();
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var fileReaderService = services.GetRequiredService<IFileReaderService>();
    var wordCombinationService = services.GetRequiredService<IWordCombinationService>();

    Console.WriteLine("Hello! This application finds all combinations of words that together form a 6-letter word.");
    Console.WriteLine("It reads from 'input.txt' in the root directory and displays valid combinations");

    var lines = fileReaderService.ReadFile();

    var combinations = wordCombinationService.FindCombinations(lines);

    Console.WriteLine($"Found {combinations.ToList().Count} combinations");

    foreach (var combination in combinations)
    {
        Console.WriteLine(combination);
    }
}