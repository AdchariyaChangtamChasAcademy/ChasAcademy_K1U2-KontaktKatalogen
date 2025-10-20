using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactCatalog.Models;

namespace ContactCatalog.Repositories
{
    public interface IContactRepository
    {
        void Add(Contact contact);
        IEnumerable<Contact> ListAll();
        IEnumerable<Contact> SearchByName(string name);
        IEnumerable<Contact> FilterByTag(string tag);
    }
}
