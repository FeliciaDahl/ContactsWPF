
using Business.Services;

namespace Business.Tests.Services;

public class FileService_Tests
{

    [Fact]
    public void SaveListToFile_ShouldReturnTrue_WhenSaved()
    {
        //arrange
        var directoryPath = "test/path";
        var fileName = "test.json";
        var content = "Test content";

        var fileService = new FileService(directoryPath, fileName);
        //act
        var result = fileService.SaveListToFile(content);

        //assert
        Assert.True(result);
        Assert.True(File.Exists(Path.Combine(directoryPath, fileName)));

    }

    [Fact]

    public void LoadListFromFile_ShouldReturnList()
    {
        //arrange
        var directoryPath = "test/path";
        var fileName = "test.json";
        var content = "Test content";
        var fileService = new FileService(directoryPath, fileName);
        File.WriteAllText(Path.Combine(directoryPath, fileName), content);

        //act

        var result = fileService.LoadListFromFile();

        //assert
        Assert.Equal(content, result);
    }
}
