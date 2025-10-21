using ContactCatalog.Repositories;
using ContactCatalog.Services;
using Microsoft.Extensions.Logging;

namespace ContactCatalog
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            var logger = loggerFactory.CreateLogger<ContactService>();
            var service = new ContactService(logger);
            var menu = new Menu(service);
            menu.StartMenu();
        }
    }
}
