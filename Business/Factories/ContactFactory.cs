using Business.Models;

namespace Business.Factories;

public static class ContactFactory
{
    public static ContactModel Create()
    {
        return new ContactModel();
    }
}