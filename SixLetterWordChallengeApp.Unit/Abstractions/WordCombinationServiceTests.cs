using Microsoft.Extensions.Options;
using SixLetterWordChallengeApp.Abstractions;
using SixLetterWordChallengeApp.Models;

namespace SixLetterWordChallengeApp.Unit.Abstractions;

/// <summary>
/// Tests for the <see cref="WordCombinationService"/> class.
/// </summary>
public class WordCombinationServiceTests
{
    private readonly WordCombinationService _sut;

    /// <summary>
    /// Initializes a new instance of the <see cref="WordCombinationServiceTests"/> class.
    /// </summary>
    public WordCombinationServiceTests()
    {
        var combinationSettings = Options.Create(new CombinationSettings { MaxCombinationLength = 6 });
        _sut = new WordCombinationService(combinationSettings);
    }

    /// <summary>
    /// Tests that the <see cref="WordCombinationService.FindCombinations"/> method returns combinations only when the combination is present in the input words.
    /// </summary>
    [Fact]
    public void FindCombinations_WithValidCombinations_ShouldReturnOnlyPresentCombinations()
    {
        // Arrange
        var words = new List<string> { "foobar", "foo", "bar", "hello", "world", "foobaz", "baz", "kenze", "g", "kenzeg", "louis", "g", "louisg" };
        var expectedCombinations = new List<string>
        {
            "foo+bar=foobar",
            "foo+baz=foobaz",
            "kenze+g=kenzeg",
            "louis+g=louisg"
        };

        // Act
        var result = _sut.FindCombinations(words).ToList();

        // Assert
        Assert.Equal(expectedCombinations.Count, result.Count);
        foreach (var combination in expectedCombinations)
        {
            Assert.Contains(combination, result);
        }
    }

    /// <summary>
    /// Tests that the <see cref="WordCombinationService.FindCombinations"/> method returns an empty list when no combinations are found.
    /// </summary>
    [Fact]
    public void FindCombinations_ShouldReturnEmptyList_WhenNoCombinationsFound()
    {
        // Arrange
        var words = new List<string> { "foobar", "foo", "baz" };

        // Act
        var result = _sut.FindCombinations(words);

        // Assert
        Assert.Empty(result);
    }

    /// <summary>
    /// Tests that the <see cref="WordCombinationService.FindCombinations"/> method returns an empty list when the input is empty.
    /// </summary>
    [Fact]
    public void FindCombinations_ShouldReturnEmptyList_WhenInputIsEmpty()
    {
        // Arrange
        var words = new List<string>();

        // Act
        var result = _sut.FindCombinations(words);

        // Assert
        Assert.Empty(result);
    }
}