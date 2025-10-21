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

namespace ContactCatalog
{
    public class Menu
    {
        private readonly ContactService _service;
        public Menu(ContactService service)
        {
            _service = service;
        }
        public void StartMenu()
        {
            while (true)
            {
                Console.WriteLine("=== Contact Catalog ===");
                Console.WriteLine("1) Add");
                Console.WriteLine("2) List");
                Console.WriteLine("3) Search (Name contains)");
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
                    default: Console.WriteLine("Invalid selection"); break;
                }
            }
        }

        public void Add()
        {
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
            catch (DuplicateEmailException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void ListAll()
        {
            foreach (var c in _service.GetAll())
                Console.WriteLine($"{c.Id}: {c.Name} ({c.Email}) - Tags: {string.Join(", ", c.Tags)}");
        }

        public void SearchByName()
        {
            Console.Write("Search term: ");
            var term = Console.ReadLine()!;
            foreach (var c in _service.SearchByName(term))
                Console.WriteLine($"{c.Id}: {c.Name}");
        }

        public void FilterByTag()
        {
            Console.Write("Tag: ");
            var tag = Console.ReadLine()!;
            foreach (var c in _service.FilterByTag(tag))
                Console.WriteLine($"{c.Id}: {c.Name}");
        }

        public void ExportCSV()
        {

        }
    }
}
