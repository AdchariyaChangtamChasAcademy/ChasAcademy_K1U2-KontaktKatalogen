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
                Email = "alice@example.com",
                Tags = ["friend"]
            };

            // When
            service.Add(contact);

            // Then
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
    }
}
