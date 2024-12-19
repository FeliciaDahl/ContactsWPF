
using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace MainApp.ViewModels;

public partial class ContactViewModel : ObservableObject
{

    private readonly IServiceProvider _serviceProvider;

    private readonly IContactService _contactService;

    [ObservableProperty]
    private ObservableCollection<Contact> _contacts = [];

    public ContactViewModel(IServiceProvider serviceProvider, IContactService contactService)
    {
        _serviceProvider = serviceProvider;
        _contactService = contactService;

        _contacts = new ObservableCollection<Contact>(_contactService.GetAll());
    }

    [RelayCommand]
    private void GoToAddContact()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<AddContactViewModel>();
    }

    [RelayCommand]
    private void GoToDetailsView(Contact contact)
    {
        {
            var contactDetailsViewModel = _serviceProvider.GetRequiredService<ContactDetailsViewModel>();
            contactDetailsViewModel.Contact = contact;

            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = contactDetailsViewModel;
        }

    }

}
