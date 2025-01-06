
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;
using System.Net;
using System.Numerics;

namespace Business.Tests.Services;

public class ContactService_Tests
{

    private readonly Mock<IFileService> _fileServiceMock;
    private readonly IContactService _contactService;

    public ContactService_Tests()
    {
        _fileServiceMock = new Mock<IFileService>();
        _contactService = new ContactService(_fileServiceMock.Object);
    }


    [Fact]

    public void AddContact_ShouldSaveToFile_AndReturnTrue_WhenContactIsCreated()
    {
          var contactModel = new ContactModel
        {
            FirstName = "TestFirstName",
            LastName = "TestLastName",
            Email = "test@example.com",
            Phone = "123456789",
            Address = "Street",
            PostalCode = "12345",
            City = "GBG"
        };

        _fileServiceMock
          .Setup(fs => fs.SaveListToFile(It.IsAny<string>()))
          .Returns(true);

        //act
        var result = _contactService.AddContact(contactModel);

        //assert
        Assert.True(result);
        _fileServiceMock.Verify(fs => fs.SaveListToFile(It.IsAny<string>()), Times.Once);
    }



}
