using ContactCatalog.Exceptions;
using ContactCatalog.Models;
using ContactCatalog.Repositories;
using ContactCatalog.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ContactCatalog
{
    public class Menu
    {
        private readonly ContactService _service;
        private readonly ILogger<ContactService> _logger;
        public Menu(ContactService service, ILogger<ContactService> logger)
        {
            _service = service;
            _logger = logger;
        }
        public void StartMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Contact Catalog ===");
                Console.WriteLine("1) Add");
                Console.WriteLine("2) List");
                Console.WriteLine("3) Search by name");
                Console.WriteLine("4) Filter by Tag");
                Console.WriteLine("5) Export CSV");
                Console.WriteLine("0) Exit");

                switch (Console.ReadLine())
                {
                    case "1": Add(); break;
                    case "2": ListAll(); break;
                    case "3": SearchByName(); break;
                    case "4": FilterByTag(); break;
                    case "5": ExportCSV(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid selection"); break;
                }
            }
        }

        public void Add()
        {
            Console.Clear();
            Console.WriteLine("=== Contact Catalog : Add ===");

            Console.Write("Name: ");
            var name = Console.ReadLine()!;

            Console.Write("Email: ");
            var email = Console.ReadLine()!;

            Console.Write("Tags (comma separated): ");
            var tags = Console.ReadLine()!.Split(',', StringSplitOptions.TrimEntries);

            try
            {
                _service.Add(new Contact { Name = name, Email = email, Tags = tags.ToList() });
            }
            catch (InvalidEmailException ex)
            {
                _logger.LogWarning("Validation error trying to add email: {Reason}", ex.Message);
            }
            catch (DuplicateEmailException ex)
            {
                _logger.LogWarning("Duplication error trying to add email: {Reason}", ex.Message);
            }
            finally
            {
                Pause();
            }
        }

        public void ListAll()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== Contact Catalog : List ===");

                foreach (var c in _service.GetAll())
                    Console.WriteLine($"{c.Id}: {c.Name} | {c.Email} | {string.Join(", ", c.Tags)}");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("Operation error trying to list all contacts: {Reason}", ex.Message);
            }

            Pause();
        }

        public void SearchByName()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== Contact Catalog : Search by name ===");

                Console.Write("Name to seach: ");
                var name = Console.ReadLine()!;

                var foundContacts = _service.SearchByName(name).ToList();

                Console.WriteLine("\n== Found contacts ==");
                foreach (var c in _service.SearchByName(name))
                    Console.WriteLine($"{c.Id}: {c.Name}");
                
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("Contacts with tag could not be found: {Reason}", ex.Message);
            }

            Pause();
        }

        public void FilterByTag()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== Contact Catalog : Filter by tag ===");

                Console.Write("Tag: ");
                var tag = Console.ReadLine()!;

                Console.WriteLine("\n== Found contacts ==");
                foreach (var c in _service.FilterByTag(tag))
                    Console.WriteLine($"{c.Id}: {c.Name}");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("Contacts list is empty could not find tag: {Reason}", ex.Message);
            }

            Pause();
        }

        public void ExportCSV()
        {

        }

        public void Pause()
        {
            Console.WriteLine("\nPress any key to continue...\n");
            Console.ReadLine();
        }
    }
}
