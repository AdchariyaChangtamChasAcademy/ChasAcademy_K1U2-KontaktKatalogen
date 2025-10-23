using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog.Exceptions
{
    public class InvalidEmailException : Exception
    {
        public string InvalidEmail { get; }
        public InvalidEmailException(string email) : base($"'{email}' is not in a valid email form.")
        {
            InvalidEmail = email;
        }
    }
}
