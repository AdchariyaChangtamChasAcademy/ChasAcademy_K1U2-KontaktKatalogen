using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog.Validators
{
    public static class EmailValidator
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Unexpected error validating email. {ex.Message}");
                return false;
            }
        }

    }
}
