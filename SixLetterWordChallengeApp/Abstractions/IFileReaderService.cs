namespace SixLetterWordChallengeApp.Abstractions;

/// <summary>
/// Interface for reading content from a file.
/// </summary>
public interface IFileReaderService
{
    /// <summary>
    /// Reads content from the specified file.
    /// </summary>
    /// <returns>A collection of content lines from the file.</returns>
    IEnumerable<string> ReadFile();
}