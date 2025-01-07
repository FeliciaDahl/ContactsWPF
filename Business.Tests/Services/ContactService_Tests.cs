
using Business.Entities;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;
using System.Text.Json;

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

        //arrage
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
        bool result = _contactService.AddContact(contactModel);

        //assert
        Assert.True(result);
        _fileServiceMock.Verify(fs => fs.SaveListToFile(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void GetAll_ShouldReturnListOfContacts()
    {

        //arrange 
        var contacts = new List<Contact>()
        {
            new()
            {
                Id = "123",
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Email = "test@example.com",
                Phone = "123456789",
                Address = "Street",
                PostalCode = "12345",
                City = "GBG"
                },

            new() 
            {
                Id = "456",
                FirstName = "TestFirstName2",
                LastName = "TestLastName2",
                Email = "test2@example.com",
                Phone = "123456789",
                Address = "Street 2",
                PostalCode = "123",
                City = "GBG",
            }
        };
        var json = JsonSerializer.Serialize(contacts);

        _fileServiceMock
            .Setup(fs => fs.LoadListFromFile())
            .Returns(json);

        //act 
        var result = _contactService.GetAll();

        //assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());

    }
    [Fact]

    public void UpdateContact_ShouldReturnTrue_WhenContactIsUpdated()
    {
        // arrange

        var originalContact = new ContactEntity

        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "TestFirstName",
            LastName = "TestLastName",
            Email = "test@example.com",
            Phone = "123456789",
            Address = "Street",
            PostalCode = "12345",
            City = "GBG"

        };

         var updatedContact = new Contact

        {
            Id = originalContact.Id,
            FirstName = "TestFirstName2",
            LastName = "TestLastName2",
            Email = "test2@example.com",
            Phone = "123456789",
            Address = "Street 2",
            PostalCode = "123",
            City = "GBG",
        };


        _fileServiceMock
           .Setup(fs => fs.LoadListFromFile())
            .Returns(JsonSerializer.Serialize(new List<ContactEntity> { originalContact })); 
        _fileServiceMock
           .Setup(fs => fs.SaveListToFile(It.IsAny<string>()))
            .Returns(true);

        var contactService = new ContactService(_fileServiceMock.Object);
        contactService.GetAll();

        // act
        bool result = contactService.UpdateContact(updatedContact);

        // assert
        Assert.True(result);
    }

    [Fact]
    public void DeleteContact_ShouldReturnTrue_WhenContactIsDeleted()
    {
        // arrange
        var contactToDelete = new ContactEntity
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "TestFirstName",
            LastName = "TestLastName",
            Email = "test@example.com",
            Phone = "123456789",
            Address = "Street",
            PostalCode = "12345",
            City = "GBG"
        };

        _fileServiceMock
            .Setup(fs => fs.LoadListFromFile())
            .Returns(JsonSerializer.Serialize(new List<ContactEntity> { contactToDelete }));

     
        _fileServiceMock
            .Setup(fs => fs.SaveListToFile(It.IsAny<string>()))
            .Verifiable();

        _contactService.GetAll();

        var contactModelToDelete = new Contact
        {
            Id = contactToDelete.Id 
        };

        // act
        var result = _contactService.DeleteContact(contactModelToDelete);

        // assert
        Assert.True(result);
        _fileServiceMock.Verify(fs => fs.SaveListToFile(It.IsAny<string>()), Times.Once);
    }

}
