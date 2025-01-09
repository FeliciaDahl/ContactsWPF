
using Business.Entities;
using Business.Factories;
using Business.Models;
namespace Business.Tests.Factories;
public class ContactEntityFactory_Tests
{
    [Fact]
    public void Create_ShouldReturnContactEntity_WhenContactModelIsProvided()
    {
        // arrange
        var firstName = "TestFirstName";
        var lastName = "TestLastName";
        var email = "test@example.com";
        var phone = "123456789";
        var address = "Street";
        var postalCode = "12345";
        var city = "GBG";

        var contactModel = new ContactModel
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Phone = phone,
            Address = address,
            PostalCode = postalCode,
            City = city
        };

        // act
        var contactEntity = ContactEntityFactory.Create(contactModel);

        // assert
        Assert.Equal(firstName, contactEntity.FirstName);
        Assert.Equal(lastName, contactEntity.LastName);
        Assert.Equal(email, contactEntity.Email);
        Assert.Equal(phone, contactEntity.Phone);
        Assert.Equal(address, contactEntity.Address);
        Assert.Equal(postalCode, contactEntity.PostalCode);
        Assert.Equal(city, contactEntity.City);
        Assert.NotNull(contactEntity.Id);
    }

    [Fact]
    public void Create_ShouldReturnContact_WhenContactEntityIsProvided()
    {
        // arrange
        var id = "Test-ID";
        var firstName = "TestFirstName";
        var lastName = "TestLastName";
        var email = "test@example.com";
        var phone = "123456789";
        var address = "Street";
        var postalCode = "12345";
        var city = "GBG";

        var contactEntity = new ContactEntity
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Phone = phone,
            Address = address,
            PostalCode = postalCode,
            City = city
        };

        // act
        var contact = ContactEntityFactory.Create(contactEntity);

        // assert
        Assert.Equal(id, contact.Id);
        Assert.Equal(firstName, contact.FirstName);
        Assert.Equal(lastName, contact.LastName);
        Assert.Equal(email, contact.Email);
        Assert.Equal(phone, contact.Phone);
        Assert.Equal(address, contact.Address);
        Assert.Equal(postalCode, contact.PostalCode);
        Assert.Equal(city, contact.City);
    }
}


