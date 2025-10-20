using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactCatalog.Models;

namespace ContactCatalog.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly Dictionary<int, Contact> _contacts = new();
        private readonly HashSet<string> _emails = new();
        private int _idCount = 0;
        public void Add(Contact contact)
        {
            _contacts.Add(_idCount++, contact);
        }
        public IEnumerable<Contact> ListAll() 
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
