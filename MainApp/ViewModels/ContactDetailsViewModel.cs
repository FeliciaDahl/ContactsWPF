using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MainApp.ViewModels;

public partial class ContactDetailsViewModel(IServiceProvider serviceProvider) : ObservableObject
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
 

    [ObservableProperty]
    private Contact _contact = new();

    [RelayCommand]
    private void GoToEditContact()
    {
        var editContactViewModel = _serviceProvider.GetRequiredService<EditContactViewModel>();
        editContactViewModel.Contact = Contact;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = editContactViewModel;
    }

    [RelayCommand]
    private void GoToContacts()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactViewModel>();
    }
}
