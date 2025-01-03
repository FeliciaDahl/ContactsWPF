
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using System.ComponentModel.DataAnnotations;

namespace Dialog.ConsoleApp;

public class MenuDialog(IContactService contactService) : IMenuDialog
{
    private readonly IContactService _contactService = contactService;

    public void Run()
    {
        var isRunning = true;

        do
        {
            Console.Clear();
            Console.WriteLine("-----MAIN MENU-----");
            Console.WriteLine("1. Add contact");
            Console.WriteLine("2. View contacts");
            Console.WriteLine("q. Quit application");
            Console.WriteLine("-------------------");

            Console.Write("Please choose one option from the menu: ");
            var option = Console.ReadLine()!;

            switch (option.ToLower())
            {
                case "1":
                    AddContact();
                    break;

                case "2":
                    ShowContactList();
                    break;

                case "q":
                    QuitOption();
                    break;

                default:
                    Console.Clear();
                    OutputDialog("Please enter a valid key");
                    Console.ReadKey();
                    break;
            }

        } while (isRunning);
    }

    public void AddContact()
    {
        var contact = ContactFactory.Create();

        Console.Clear();
        contact.FirstName = GetValidatedInput("Enter first name :", nameof(contact.FirstName));


        contact.LastName = GetValidatedInput("Enter last name :", nameof(contact.LastName));


        contact.Email = GetValidatedInput("Enter email :", nameof(contact.Email));


        contact.Phone = GetValidatedInput("Enter phone number :", nameof(contact.Phone));


        contact.Address = GetValidatedInput("Enter address :", nameof(contact.Address));


        contact.PostalCode = GetValidatedInput("Enter postal code :", nameof(contact.PostalCode));


        contact.City = GetValidatedInput("Enter city :", nameof(contact.City));

        bool result = _contactService.AddContact(contact);

        if (result)
            OutputDialog("Contact was created");
        else
            OutputDialog("Could not create a contact, please try again");

        Console.ReadKey();
    }

    public string GetValidatedInput(string prompt, string properyName)
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine(prompt);
            var input = Console.ReadLine() ?? string.Empty;

            var results = new List<ValidationResult>();
            var context = new ValidationContext(new ContactModel()) { MemberName = properyName };

            if (Validator.TryValidateProperty(input, context, results))
                return input;
            Console.WriteLine($"{results[0].ErrorMessage}");
        }
    }

    public void ShowContactList()
    {
        var contacts = _contactService.GetAll();

        Console.Clear();
        if (!contacts.Any())
        {
            OutputDialog("No users were found in the list");
            return;
        }
        foreach (var user in contacts)
        {
            Console.WriteLine($"{"ID:",-15}{user.Id}");
            Console.WriteLine($"{"Name:",-15}{user.FullName}");
            Console.WriteLine($"{"Email:",-15}{user.Email}");
            Console.WriteLine($"{"Phone:",-15}{user.Phone}");
            Console.WriteLine($"{"Adress:",-15}{user.Address}");
            Console.WriteLine($"{"PostalCode:",-15}{user.PostalCode}");
            Console.WriteLine($"{"City:",-15}{user.City}");
            Console.WriteLine();
        }
        Console.ReadKey();
    }


    public void QuitOption()
    {
        Console.Clear();
        Console.WriteLine("Do you want to exit? y/n?");
        var option = Console.ReadLine()!;

        if (option.Equals("y", StringComparison.CurrentCultureIgnoreCase))
        {
            Environment.Exit(0);
        }
    }

    public void OutputDialog(string message)
    {
        Console.Clear();
        Console.WriteLine(message);
        Console.ReadKey();

    }
}




