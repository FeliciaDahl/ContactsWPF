
using Business.Interfaces;
using Business.Models;
using MainApp.ViewModels;
using Moq;

namespace MainApp.Tests;

public class EditContactViewModel_Tests
{

    private readonly Mock<IContactService> _contactServiceMock;
    private readonly Mock<IServiceProvider> _serviceProviderMock;
    private readonly EditContactViewModel _editContactViewModel;

    public EditContactViewModel_Tests()
    {
        _contactServiceMock = new Mock<IContactService>();
        _serviceProviderMock = new Mock<IServiceProvider>();

        _editContactViewModel = new EditContactViewModel(_serviceProviderMock.Object, _contactServiceMock.Object);
    }

    [Fact]
    public void Save_ShouldCallUpdateContactService_WithCorrectContact()
    {
        //arrange
        var contact = new Contact
        {
            FirstName = "TestFirstName",
            LastName = "TestLastName",
            Email = "test@example.com",
            Phone = "123456789",
            Address = "Street",
            PostalCode = "12345",
            City = "GBG"
        };

        _editContactViewModel.Contact = contact;


        //act

        _editContactViewModel.SaveCommand.Execute(null);

        //assert
        
        _contactServiceMock.Verify(service => service.UpdateContact(It.Is<Contact>(contact =>
            contact.FirstName == "TestFirstName" &&
            contact.LastName == "TestLastName" &&
            contact.Email == "test@example.com" &&
            contact.Phone == "123456789" &&
            contact.Address == "Street" &&
            contact.PostalCode == "12345" &&
            contact.City == "GBG")), Times.Once);

    }


}
