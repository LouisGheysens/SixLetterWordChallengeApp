using Microsoft.Extensions.Options;
using SixLetterWordChallengeApp.Models;

namespace SixLetterWordChallengeApp.Abstractions;

/// <inheritdoc />
public class WordCombinationService : IWordCombinationService
{
    private readonly int _maxCombinationLength;

    /// <summary>
    /// Initializes a new instance of the <see cref="WordCombinationService"/> class.
    /// </summary>
    /// <param name="settings">The settings</param>
    public WordCombinationService(IOptions<CombinationSettings> settings)
    {
        _maxCombinationLength = settings.Value.MaxCombinationLength;
    }

    /// <inheritdoc />
    public IEnumerable<string> FindCombinations(IEnumerable<string> words)
    {
        var wordSet = new HashSet<string>(words);

        foreach (var word in wordSet.Where(w => w.Length == _maxCombinationLength))
        {
            for (int i = 1; i < word.Length; i++)
            {
                var firstPart = word.Substring(0, i);
                var secondPart = word.Substring(i);

                var partsToCheck = new[] { firstPart, secondPart, word };

                if (partsToCheck.All(part => wordSet.Contains(part)))
                {
                    yield return $"{firstPart}+{secondPart}={word}";
                }
            }
        }
    }
}