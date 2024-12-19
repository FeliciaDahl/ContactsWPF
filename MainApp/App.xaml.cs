
using Business.Helpers;
using Business.Interfaces;
using Business.Services;
using MainApp.ViewModels;
using MainApp.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace MainApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                 .ConfigureServices(services =>
                 {
                     services.AddSingleton<IFileService>(new FileService("Data", "contactList.json"));
                     services.AddSingleton<IContactService, ContactService>();
                 

                     services.AddSingleton<MainViewModel>();
                     services.AddSingleton<MainWindow>();

                     services.AddTransient<ContactViewModel>();
                     services.AddTransient<ContactsView>();

                     services.AddTransient<ContactDetailsViewModel>();
                     services.AddTransient<ContactDetailsView>();

                     services.AddTransient<AddContactViewModel>();
                     services.AddTransient<AddContactView>();

                     services.AddTransient<EditContactViewModel>();
                     services.AddTransient<EditContactView>();

                    

                 })
                 .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainViewModel = _host.Services.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel= _host.Services.GetRequiredService<ContactViewModel>();

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }

}
