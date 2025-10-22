using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog.Exceptions
{
    public class InvalidEmailException : Exception
    {
        public string Email { get; }
        public InvalidEmailException(string email) : base($"The email {email} is invalid.")
        {
            Email = email;
        }
    }
}
