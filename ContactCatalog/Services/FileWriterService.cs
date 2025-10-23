using ContactCatalog.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog.Services
{
    public class FileWriterService : IFileWriter, IDisposable
    {
        private readonly StreamWriter _writer;

        public FileWriterService(string filePath)
        {
            _writer = new StreamWriter(filePath);
        }

        public void WriteLine(string line)
        {
            _writer.WriteLine(line);
        }

        public void Dispose()
        {
            _writer.Flush();
            _writer.Dispose();
        }
    }
}
