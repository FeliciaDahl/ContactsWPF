
using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Windows;

namespace MainApp.ViewModels;

public partial class AddContactViewModel(IServiceProvider serviceProvider, IContactService contactService) : ObservableObject
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    private readonly IContactService _contactService = contactService;

    [ObservableProperty]
    private ContactModel _contact = new();
 

    [RelayCommand]
    /*Delar av denna kod är genererad med hjälp av Chat GPT-40 mini. 
     Validerar så alla fält är ifyllda innan kontakten sparas. */
    private void Save()
    { 
        if (!string.IsNullOrWhiteSpace(Contact.FirstName)
            && !string.IsNullOrWhiteSpace(Contact.LastName)
            && !string.IsNullOrWhiteSpace(Contact.Email)
            && !string.IsNullOrWhiteSpace(Contact.Phone)
            && !string.IsNullOrWhiteSpace(Contact.Address)
            && !string.IsNullOrWhiteSpace(Contact.PostalCode)
            && !string.IsNullOrWhiteSpace(Contact.City)
            )
        {
            var result = _contactService.AddContact(Contact);

            
            if(result)
            {
                var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
                mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactViewModel>();
            }
           
        }
      else
            //skickar ut felmeddelande till användaren om ett fält skulle saknas.
        { 
            MessageBox.Show("All fields must be filled out. Try again", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

            
    }


    [RelayCommand]
    private void GoToContacts()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactViewModel>();
    }

}
