using Microsoft.Extensions.Options;
using SixLetterWordChallengeApp.Abstractions;
using SixLetterWordChallengeApp.Models;

namespace SixLetterWordChallengeApp.Unit.Abstractions;

/// <summary>
/// Tests for the <see cref="FileReaderService"/> class.
/// </summary>
public class FileReaderServiceTests
{
    private readonly string _testFilePath;
    private readonly FileReaderService _sut;
    private const string TestDataDirectory = "TestDataSets";
    private const string TestFileName = "testfile.txt";

    /// <summary>
    /// Initializes a new instance of the <see cref="FileReaderServiceTests"/> class.
    /// </summary>
    public FileReaderServiceTests()
    {
        _testFilePath = Path.Combine(Directory.GetCurrentDirectory(), TestDataDirectory, TestFileName);
        var fileSettings = Options.Create(new FileSettings { FilePath = _testFilePath });
        _sut = new FileReaderService(fileSettings);
    }

    /// <summary>
    /// Tests that the <see cref="FileReaderService.ReadFile"/> method reads the lines from a file correctly.
    /// </summary>
    [Fact]
    public void ReadFile_ShouldReturnLines_WhenFileExists()
    {
        // Arrange
        var expectedLines = new List<string> { "line1", "line2", "line3", "foobar", "bazqux", "teststring" };

        // Act
        var result = _sut.ReadFile();

        // Assert
        Assert.Equal(expectedLines, result);
    }


    /// <summary>
    /// Tests that the <see cref="FileReaderService.ReadFile"/> method throws a FileNotFoundException when the file does not exist.
    /// </summary>
    [Fact]
    public void ReadFile_ShouldThrowFileNotFoundException_WhenFileDoesNotExist()
    {
        // Arrange
        if (File.Exists(_testFilePath))
        {
            File.Delete(_testFilePath);
        }

        // Act & Assert
        Assert.Throws<FileNotFoundException>(() => _sut.ReadFile());
    }
}