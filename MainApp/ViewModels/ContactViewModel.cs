
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace MainApp.ViewModels;

public partial class ContactViewModel : ObservableObject
{

    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private string _title = "Contacts";

  

    [RelayCommand]
    private void GoToAddContact()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<AddContactViewModel>();
    }

    public ContactViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

}
