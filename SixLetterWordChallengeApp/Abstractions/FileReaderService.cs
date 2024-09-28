using Microsoft.Extensions.Options;
using SixLetterWordChallengeApp.Models;

namespace SixLetterWordChallengeApp.Abstractions;

/// <inheritdoc />
public class FileReaderService : IFileReaderService
{
    private readonly string _filePath;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileReaderService"/> class.
    /// </summary>
    /// <param name="settings">The settings</param>
    public FileReaderService(IOptions<FileSettings> settings)
    {
        _filePath = Path.Combine(Directory.GetCurrentDirectory(), settings.Value.FilePath);
    }

    /// <inheritdoc />
    public IEnumerable<string> ReadFile()
    {
        if (!File.Exists(_filePath))
        {
            throw new FileNotFoundException("Input file not found", _filePath);
        }

        return File.ReadAllLines(_filePath).ToList();
    }
}