using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog.Repositories
{
    public interface IFileWriter
    {
        void WriteLine(string line);
        void Dispose();
    }
}
