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
        void AddContact(Contact contact);
        IEnumerable<Contact> GetAll();
        IEnumerable<Contact> SearchByName(string name);
        IEnumerable<Contact> FilterByTag(string tag);

        IEnumerable<Contact> GetByEmail(string email);

        IEnumerable<Contact> SearchByNameOrEmail(string nameOrEmail);
    }
}
