using ContactCatalog.Exceptions;
using ContactCatalog.Models;
using ContactCatalog.Validators;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ContactCatalog.Services
{
    public class ContactService : ContactCatalog.Repositories.IContactRepository
    {
        private readonly ILogger<ContactService> _logger;
        private readonly Dictionary<int, Contact> _contacts = new();
        private readonly HashSet<string> _emails = new();
        private int _idCount = 0;
        public ContactService(ILogger<ContactService> logger)
        {
            _logger = logger;
        }

        public void Add(Contact contact)
        {
            if (_emails.Contains(contact.Email))
                throw new DuplicateEmailException(contact.Email);

            contact.Id = _idCount++;
            _contacts[contact.Id] = contact;
            _emails.Add(contact.Email);
            _logger.LogInformation("Added contact: {Name}", contact.Name);
        }
        public IEnumerable<Contact> GetAll() 
            => _contacts.Values;

        public IEnumerable<Contact> SearchByName(string name) 
            => _contacts.Values
                        .Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

        public IEnumerable<Contact> FilterByTag(string tag)
            => _contacts.Values
                        .Where(c => c.Tags.Contains(tag))
                        .OrderBy(c => c.Name);
    }
}
