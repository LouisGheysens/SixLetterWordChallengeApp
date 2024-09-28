namespace SixLetterWordChallengeApp.Abstractions;

/// <summary>
/// Interface for finding valid word combinations.
/// </summary>
public interface IWordCombinationService
{
    /// <summary>
    /// Finds all valid word combinations that form a 6-character word.
    /// </summary>
    /// <param name="words">A collection of words to search for combinations.</param>
    /// <returns>A collection of valid word combinations.</returns>
    IEnumerable<string> FindCombinations(IEnumerable<string> words);
}