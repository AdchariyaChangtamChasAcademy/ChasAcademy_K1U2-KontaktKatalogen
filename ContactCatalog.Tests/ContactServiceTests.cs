using ContactCatalog.Exceptions;
using ContactCatalog.Models;
using ContactCatalog.Repositories;
using ContactCatalog.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace ContactCatalog.Tests
{
    public class ContactServiceTests
    {
        [Fact]
        public void GivenValidContact_WhenAdded_ThenLogsInformation()
        {
            // Given
            var mockLogger = new Mock<ILogger<ContactService>>();
            var service = new ContactService(mockLogger.Object);

            var contact = new Contact
            {
                Id = 1,
                Name = "Alice",
                Email = "alice@mail.com",
                Tags = new List<string> { "friend" }
            };

            // When
            service.AddContact(contact);

            // Then
            /*
             * 1. log.Log(...)                                                             1. Moq intercepts calls to the Log method of ILogger.
             * 2.  It.Is<LogLevel>(lvl => lvl == LogLevel.Information)                     2. Only match calls at Information level.
             * 3.  It.IsAny<EventId>()                                                     3. Ignore the EventId parameter.
             * 4.  It.Is<It.IsAnyType>((obj,t)=>obj.ToString().Contains("Added contact"))  4. Check the message contains “Added contact”.
             * 5.  It.IsAny<Exception>()                                                   5. Ignore the exception parameter.
             * 6.  (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()              6. Ignore the formatter delegate parameter.
             * 7.  Times.Once                                                              7. Assert that the log was called exactly once.
             */
            mockLogger.Verify(
                log => log.Log(
                    It.Is<LogLevel>(lvl => lvl == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((obj, t) => obj.ToString().Contains("Added contact")),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
                Times.Once
            );
        }

        [Fact]
        public void GivenContactsExist_WhenExportToCsvIsCalled_ThenHeaderAndAllContactsAreWritten()
        {
            // Given
            // Mock logger and service
            var logger = Mock.Of<ILogger<ContactService>>();
            var service = new ContactService(logger);

            // Add sample contacts
            service.AddContact(new Contact { Name = "Alice", Email = "alice@mail.com", Tags = new List<string> { "Work", "Friend" } });
            service.AddContact(new Contact { Name = "Bob", Email = "bob@mail.com", Tags = new List<string> { "Club", "Friend" } });

            // Mock file writer
            var writerMock = new Mock<IFileWriter>();
            var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\.."));

            // When
            service.ExportToCsv(writerMock.Object, path);

            // Then
            // Header + 2 contacts = 3 WriteLine calls
            writerMock.Verify(w => w.WriteLine(It.IsAny<string>()), Times.Exactly(3));

            // (Verify expected header written once)
            writerMock.Verify(w => w.WriteLine("Id,Name,Email,Tags"), Times.Once);

            // (Verify specific contact lines with expected values)
            writerMock.Verify(w => w.WriteLine("1,Alice,alice@mail.com,Work;Friend"), Times.Once);
            writerMock.Verify(w => w.WriteLine("2,Bob,bob@mail.com,Club;Friend"), Times.Once);
        }
    }
}
