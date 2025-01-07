
using Business.Helpers;

namespace Business.Tests.Helpers;

public class GenerateUniqeId_Tests
{
    [Fact]
    public void GenerateUniqeId_ShouldReturnStringOfTypeGuid()
    {
        //arrange
        var genarator = new GenerateUniqeId();

        //
        string id = genarator.GenerateId();

        //assert
        Assert.False(string.IsNullOrWhiteSpace(id));
        Assert.True(Guid.TryParse(id, out _));
    }
    
}
