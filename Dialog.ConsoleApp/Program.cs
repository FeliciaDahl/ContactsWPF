using Business.Interfaces;
using Business.Services;
using Dialog.ConsoleApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {

        services.AddSingleton<IFileService>(new FileService("Data", "contactList.json"));
        services.AddSingleton<IContactService, ContactService>();
        services.AddTransient<IMenuDialog, MenuDialog>();
    }) .Build();

using var scope = host.Services.CreateScope();
var mainMenu = scope.ServiceProvider.GetService<IMenuDialog>()!;
mainMenu.Run();