
using Business.Factories;
using Business.Models;

namespace Business.Tests.Factories;

public class ContactFactory_Tests
{
    [Fact]
    public void Create_ShouldReturnContactModel()
    {
        //act
        ContactModel reslut = ContactFactory.Create();

        //assert
        Assert.IsType<ContactModel>(reslut);
    }

  
}
