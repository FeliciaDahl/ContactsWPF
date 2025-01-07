using Business.Entities;
using Business.Helpers;
using Business.Interfaces;
using Business.Models;
namespace Business.Factories;

public static class ContactEntityFactory
{

    private static readonly IGenerateUniqeId _generateUniqeId = new GenerateUniqeId();

    public static ContactEntity Create(ContactModel contact)
    {

        return new ContactEntity
        {
            Id = _generateUniqeId.GenerateId(),
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            Email = contact.Email,
            Phone = contact.Phone,
            Address = contact.Address,
            PostalCode = contact.PostalCode,
            City = contact.City,
            CreatedDate = DateTime.Now
        };
    }

    public static Contact Create(ContactEntity entity)
    {
        return new Contact
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            Phone = entity.Phone,
            Address = entity.Address,
            PostalCode = entity.PostalCode,
            City = entity.City
        };
    }
}