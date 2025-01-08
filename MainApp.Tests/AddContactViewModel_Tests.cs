using Business.Interfaces;
using Business.Models;
using MainApp.ViewModels;
using MainApp.Views;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace MainApp.Tests;

public class AddContactViewModel_Tests
{
    private readonly Mock<IContactService> _contactServiceMock;
    private readonly Mock<IServiceProvider> _serviceProviderMock;
    private readonly AddContactViewModel _addContactViewModel;

    //Denna kod är genererad med hjälp av GPT-4o mini.
    //I konstruktorn lägger vi in allt som behöver mockas. Koden ska ge returnera en mockad version av mainViewModel och contactViewModel 
    //när dessa blir kallade på via serviceProvider.GetRequiredService som körs för att navigera mellan olika views.
    //
    public AddContactViewModel_Tests()
    {
        _contactServiceMock = new Mock<IContactService>();
        _serviceProviderMock = new Mock<IServiceProvider>();

        var mainViewModel = new MainViewModel(_serviceProviderMock.Object);
        _serviceProviderMock.Setup(sp => sp.GetRequiredService<MainViewModel>())
        .Returns(mainViewModel);

        var contactViewModelMock = new Mock<ContactViewModel>(_serviceProviderMock.Object, _contactServiceMock.Object);
        _serviceProviderMock
            .Setup(sp => sp.GetRequiredService<ContactViewModel>())
            .Returns(contactViewModelMock.Object);

        //Här skapar jag en instans av AddContactViewModel och skickar med beroendena. Denna behövs för att kunna
        _addContactViewModel = new AddContactViewModel(_serviceProviderMock.Object, _contactServiceMock.Object);
    }

    [Fact]
    public void Save_ShouldNavigateToContactViewModel_WhenAddContactIsSuccessful()
    {

        // arrange
        //skapar en ny instans av ContactModel som vi AddContactView.Contact
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

        _addContactViewModel.Contact = contactModel;

        //När metoden AddContact körs ska den returnera true.
        _contactServiceMock
            .Setup(service => service.AddContact(contactModel))
            .Returns(true);

        //Då vi inte har tillgång till CurrentViewModel så måste vi mocka så vi kan verifiera i assert
        var mainViewModelMock = new Mock<MainViewModel>();
        mainViewModelMock.SetupProperty(m => m.CurrentViewModel);

        //Om Addcontact returnerar true så aktiveras navigeringen
        _serviceProviderMock
            .Setup(sp => sp.GetService(typeof(MainViewModel)))
            .Returns(mainViewModelMock.Object);

        //act
        //anropar SaveCommand i AddContactViewModel och sätter null då execute kräver parameter.
        _addContactViewModel.SaveCommand.Execute(null);


        //assert

        //Kontrollerar att AddContact anroppas en gång.
        _contactServiceMock.Verify(service => service.AddContact(It.IsAny<ContactModel>()), Times.Once);

        //Kontrollerar att rätt navigeringen sker.
        _serviceProviderMock.Verify(sp => sp.GetRequiredService<ContactViewModel>(), Times.Once);
        mainViewModelMock.VerifySet(m => m.CurrentViewModel = It.IsAny<ContactViewModel>(), Times.Once);
    }
}



