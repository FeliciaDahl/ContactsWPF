using Business.Interfaces;
using Business.Models;
using MainApp.ViewModels;
using Moq;


namespace MainApp.Tests;
  /* Denna kod är genererad med hjälp av GPT-4o mini. 
   * Testet kontrollerar att Save anropar AddContact och skickar med rätt värde. */
public class AddContactViewModel_SimpleSaveTest
{
    private readonly Mock<IContactService> _contactServiceMock;
    private readonly Mock<IServiceProvider> _serviceProviderMock;
    private readonly AddContactViewModel _addContactViewModel;

    //I konstruktorn lägger vi in det som behöver mockas. 
    public AddContactViewModel_SimpleSaveTest()
    {
        _contactServiceMock = new Mock<IContactService>();
        _serviceProviderMock = new Mock<IServiceProvider>();

        _addContactViewModel = new AddContactViewModel(_serviceProviderMock.Object, _contactServiceMock.Object);
    }

    [Fact]
    public void Save_ShouldCallAddContactService_WithCorrectContact()
    {
        // arrange
        //skapar ett contactModel objekt
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

        //tilldelar contactModel till Contact i AddContactViewModel
        _addContactViewModel.Contact = contactModel;

        // act
        //simulerar anropet av Save i AddContactViewModel som triggas när användaren tycker på knappen som är skapad i AddContactView.xaml(därav command)
        //skickar med null då Execute kräver parameter
        _addContactViewModel.SaveCommand.Execute(null);

        // assert

        //verifierar att AddContant anropas och att det skapade contactModel objektet skickas med.
        _contactServiceMock
            .Verify(service => service.AddContact(It.Is<ContactModel>(contactModel =>
            contactModel.FirstName == "TestFirstName" &&
            contactModel.LastName == "TestLastName" &&
            contactModel.Email == "test@example.com" &&
            contactModel.Phone == "123456789" &&
            contactModel.Address == "Street" &&
            contactModel.PostalCode == "12345" &&
            contactModel.City == "GBG")), Times.Once);
    }
}


