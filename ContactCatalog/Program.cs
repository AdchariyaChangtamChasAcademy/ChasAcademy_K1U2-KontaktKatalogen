using ContactCatalog.Models;
using ContactCatalog.Repositories;
using ContactCatalog.Services;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;

namespace ContactCatalog
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<ContactService>();
            var service = new ContactService(logger);
            var menu = new Menu(service, logger);

            /* For testing purposes 
             * 
             * service.AddContact(new Contact { Name = "Alice", Email = "alice@mail.com", Tags = new string[] { "Work", "Friend" }.ToList() });
             * service.AddContact(new Contact { Name = "Bob", Email = "bob@mail.com", Tags = new string[] { "Club", "Friend", "Besty" }.ToList() });
             * service.AddContact(new Contact { Name = "Charlie", Email = "charlie@mail.com", Tags = new string[] { "Work", "Rival" }.ToList() });
             * Console.Clear();
            */

            menu.Run();
        }
    }
}
