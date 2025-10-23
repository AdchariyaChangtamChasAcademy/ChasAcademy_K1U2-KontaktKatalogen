using ContactCatalog.Exceptions;
using ContactCatalog.Models;
using ContactCatalog.Repositories;
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
        private int _idCount = 1;
        public ContactService(ILogger<ContactService> logger)
        {
            _logger = logger;
        }

        public void AddContact(Contact contact)
        {
            // If _emails is not a valid email, throw an InvalidEmailException
            if (!EmailValidator.IsValidEmail(contact.Email))
                throw new InvalidEmailException(contact.Email); 

            // If _emails already contains given contacts email, throw a DuplicateEmailException
            if (_emails.Contains(contact.Email))
                throw new DuplicateEmailException(contact.Email);

            // Adds new contact to service            
            contact.Id = _idCount++;
            _contacts[contact.Id] = contact;
            _emails.Add(contact.Email);
            _logger.LogInformation("Added contact ({Id}) {Name} <{Email}> [{Tags}]\n", contact.Id, contact.Name, contact.Email, contact.Tags);
        }

        // Returns an IEnumerable<Contact> containing all contacts.
        // Throws InvalidOperationException if the contacts list is empty.
        public IEnumerable<Contact> GetAll() => _contacts.Any() ? _contacts.Values : throw new InvalidOperationException("Contacts list is empty.");

        public IEnumerable<Contact> SearchByName(string name)
        {
            // If there are no contacts, throw InvalidOperationException
            if (_contacts == null || !_contacts.Any()) 
                throw new InvalidOperationException("Contacts list is empty");

            // Filter contacts by name (case-insensitive)
            var foundContacts = _contacts.Values
                                         .Where(c => c.Name
                                         .Contains(name, StringComparison.OrdinalIgnoreCase));

            // If contact with name could not be found, throw InvalidOperationException
            if (!foundContacts.Any()) throw new InvalidOperationException($"Could not find '{name}' in list");

            // Log information
            _logger.LogInformation("'{Count}' Contacts found with given name: '{Name}'\n", foundContacts.Count(), name);
            return foundContacts;
        }

        public IEnumerable<Contact> GetByEmail(string email)
        {
            // If there are no contacts, throw InvalidOperationException
            if (_contacts == null || !_contacts.Any())
                throw new InvalidOperationException("Contacts list is empty");

            // Filter contacts by name (case-insensitive)
            var foundContacts = _contacts.Values
                                         .Where(c => c.Email
                                         .Contains(email, StringComparison.OrdinalIgnoreCase));

            // If contact with name could not be found, throw InvalidOperationException
            if (!foundContacts.Any()) throw new InvalidOperationException($"Could not find '{email}' in list");

            // Log information
            _logger.LogInformation("'{Count}' Contacts found with given name: '{Name}'\n", foundContacts.Count(), email);
            return foundContacts;
        }

        public IEnumerable<Contact> SearchByNameOrEmail(string nameOrEmail)
        {
            if (_contacts == null || !_contacts.Any())
                throw new InvalidOperationException("Contacts list is empty");

            var foundContacts = _contacts.Values
                .Where(c => c.Name.Contains(nameOrEmail, StringComparison.OrdinalIgnoreCase)
                         || c.Email.Contains(nameOrEmail, StringComparison.OrdinalIgnoreCase));

            if (!foundContacts.Any())
                throw new InvalidOperationException($"Could not find contact by '{nameOrEmail}' in contacts");

            _logger.LogInformation("'{Count}' Contacts found with given name or email: '{NameOrEmail}'\n", foundContacts.Count(), nameOrEmail);
            return foundContacts;
        }

        public IEnumerable<Contact> FilterByTag(string tag)
        {
            // If _contacts is empty, throw an InvalidOperationException
            if (_contacts == null || !_contacts.Any()) 
                throw new InvalidOperationException(tag);

            // Filter contacts by tags that contains the given tag. (Case-insensitive and ordered alphabetically by names)
            var foundContacts = _contacts.Values
                                         .Where(c => c.Tags.Any(t => t.Equals(tag, StringComparison.OrdinalIgnoreCase)))
                                         .OrderBy(c => c.Name);

            // If contact with tag could not be found, throw InvalidOperationException
            if (!foundContacts.Any()) throw new InvalidOperationException(tag);

            return foundContacts;
        }

        public void ExportToCsv(IFileWriter fileWriter)
        {
            // Check for empty contacts
            if (_contacts == null || !_contacts.Any())
                throw new InvalidOperationException("Contacts list is empty");

            // Write CSV header
            fileWriter.WriteLine("Id,Name,Email,Tags");

            // Write each contactd
            foreach (var contact in _contacts.Values)
            {
                var tags = string.Join(";", contact.Tags); // semicolon to avoid conflicts
                fileWriter.WriteLine($"{contact.Id},{EscapeCsv(contact.Name)},{EscapeCsv(contact.Email)},{EscapeCsv(tags)}");
            }
            // Dispose to complete the writing to file
            fileWriter.Dispose();

            // Log information
            _logger.LogInformation("Contacts exported to '{FilePath}'\n", Environment.CurrentDirectory);
        }

        private string EscapeCsv(string value)
        {
            // Check for empty string
            if (string.IsNullOrEmpty(value)) return "";

            // Check for special characters
            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
                // Wrap in quotes and escape inner quotes if needed, csv formatting
                return $"\"{value.Replace("\"", "\"\"")}\"";

            // If there are no special characters, return value
            return value;
        }
    }
}
