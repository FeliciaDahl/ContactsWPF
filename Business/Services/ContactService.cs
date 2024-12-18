

using Business.Entities;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;

namespace Business.Services;

public class ContactService : IContactService

{
    private readonly IFileService _fileService;
    private readonly IGenerateUniqeId _generateId;
    private readonly JsonSerializerOptions _jsonOptions;
    private List<ContactEntity> _contacts = [];

    public ContactService(IFileService fileService, IGenerateUniqeId generateId, JsonSerializerOptions jsonOptions)
    {
        _fileService = fileService;
        _generateId = generateId;
        _jsonOptions = jsonOptions;
    }

    public bool AddContact(ContactModel contact)
    {
        try
        {
            var contactEntity = ContactEntityFactory.Create(contact, _generateId);
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
            return Enumerable.Empty<Contact>();
        }

    }
}


