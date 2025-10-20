using ContactCatalog.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog
{
    public class Menu
    {
        public void StartMenu()
        {
            IContactRepository repo = new ContactRepository();

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

        public void Add()
        {

        }

        public void ListAll()
        {

        }

        public void SearchByName()
        {

        }

        public void FilterByTag()
        {

        }

        public void ExportCSV()
        {

        }
    }
}
