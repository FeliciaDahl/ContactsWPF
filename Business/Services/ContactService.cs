﻿

using Business.Entities;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Business.Services;

public class ContactService : IContactService

{
    private readonly IFileService _fileService;
    private readonly JsonSerializerOptions _jsonOptions;
    private List<ContactEntity> _contacts = [];

    public ContactService(IFileService fileService)
    {
        _fileService = fileService;
        _jsonOptions = new JsonSerializerOptions { WriteIndented = true };
       
    }

    public bool AddContact(ContactModel contact)
    {
        try
        {
            var contactEntity = ContactEntityFactory.Create(contact);
            _contacts.Add(contactEntity);

            var json = JsonSerializer.Serialize(_contacts, _jsonOptions);
            _fileService.SaveListToFile(json);

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public IEnumerable<Contact> GetAll()
    {
        try
        {
            var json = _fileService.LoadListFromFile();

            _contacts = JsonSerializer.Deserialize<List<ContactEntity>>(json, _jsonOptions) ?? [];

            return _contacts.Select(contact => ContactEntityFactory.Create(contact));
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return [];
        }

    }

    public bool UpdateContact(Contact contactToUpdate)
    {
        try
        {
            var listContact = _contacts.FirstOrDefault(contact => string.Equals(contact.Id, contactToUpdate.Id));

            if (listContact == null)
            {
                return false;
            }
               
            listContact.FirstName = contactToUpdate.FirstName;
            listContact.LastName = contactToUpdate.LastName;
            listContact.Email = contactToUpdate.Email;
            listContact.Phone = contactToUpdate.Phone;
            listContact.Address = contactToUpdate.Address;
            listContact.PostalCode = contactToUpdate.PostalCode;
            listContact.City = contactToUpdate.City;

            var json = JsonSerializer.Serialize(_contacts, _jsonOptions);
            _fileService.SaveListToFile(json);

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false; 
        }
    }

    public bool DeleteContact(Contact contactToDelete)
    {
        try
        {
            var listContact = _contacts.FirstOrDefault(contact => contact.Id == contactToDelete.Id);
           
            if (listContact == null)
            {
                return false;
            }
                
            _contacts.Remove(listContact);

            var json = JsonSerializer.Serialize(_contacts, _jsonOptions);
            _fileService.SaveListToFile(json);

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }

    }
}


