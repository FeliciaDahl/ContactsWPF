
using Business.Interfaces;
using Business.Models;
using MainApp.ViewModels;
using Moq;

namespace MainApp.Tests;

public class ContactViewModel_Tests
{

    private readonly Mock<IContactService> _contactServiceMock;
    private readonly Mock<IServiceProvider> _serviceProviderMock;

    private readonly ContactViewModel _contactViewModel;
    public ContactViewModel_Tests()
    {
        _contactServiceMock = new Mock<IContactService>();
        _serviceProviderMock = new Mock<IServiceProvider>();

        _contactViewModel = new ContactViewModel(_serviceProviderMock.Object, _contactServiceMock.Object);

    }

    [Fact] 
    public void DeleteContactInList_ShouldDeleteContact_WhenMethodDeleteContact_ReturnsTrue()
    {
        //arrange

        var contactToDelete = new Contact
        {
            FirstName = "TestFirstName",
            LastName = "TestLastName",
            Email = "test@example.com",
            Phone = "123456789",
            Address = "Street",
            PostalCode = "12345",
            City = "GBG"
        };

        _contactViewModel.Contacts.Add(contactToDelete);
        
           _contactServiceMock
            .Setup(cs => cs.DeleteContact(contactToDelete))
            .Returns(true);

        //act

        _contactViewModel.DeleteContactInListCommand.Execute(contactToDelete);

        //assert
       
        Assert.DoesNotContain(contactToDelete, _contactViewModel.Contacts);
        _contactServiceMock.Verify(service => service.DeleteContact(contactToDelete), Times.Once);

    }

    [Fact]
    public void DeleteContactInList_ShouldNotDeleteContact_WhenMethodDeleteContact_ReturnsFalse()
    {
        //arrange

        var contactToDelete = new Contact
        {
            FirstName = "TestFirstName",
            LastName = "TestLastName",
            Email = "test@example.com",
            Phone = "123456789",
            Address = "Street",
            PostalCode = "12345",
            City = "GBG"
        };
        _contactViewModel.Contacts.Add(contactToDelete);

        _contactServiceMock
         .Setup(cs => cs.DeleteContact(contactToDelete))
         .Returns(false);

        //act
        _contactViewModel.DeleteContactInListCommand.Execute(contactToDelete);

        //assert
        Assert.Contains(contactToDelete, _contactViewModel.Contacts);

    }
}
