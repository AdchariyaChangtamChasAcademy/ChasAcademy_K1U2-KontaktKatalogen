using ContactCatalog.Exceptions;
using ContactCatalog.Models;
using ContactCatalog.Repositories;
using ContactCatalog.Services;
using ContactCatalog.Validators;
using Microsoft.Extensions.Logging;
using Moq;

namespace ContactCatalog.Tests
{
    public class EmailValidatorTests
    {
        [Fact]
        public void GivenValidEmail_WhenValidated_ThenReturnTrue()
        {
            bool result = EmailValidator.IsValidEmail("validEmail@example.com");
            Assert.True(result);
        }

        [Fact]
        public void GivenInvalidEmail_WhenValidated_ThenReturnFalse()
        {
            bool result = EmailValidator.IsValidEmail("invalidEmailExample");
            Assert.False(result);
        }

        [Fact]
        public void GivenExistingEmail_WhenAddingContact_ThenThrowDuplicateEmailException()
        {
            // Given
            var logger = Mock.Of<ILogger<ContactService>>();
            var service = new ContactService(logger);

            // When
            service.Add(new Contact { Name = "Alice", Email = "a@a.com" });

            // Then
            Assert.Throws<DuplicateEmailException>(() => service.Add(new Contact { Name = "Bob", Email = "a@a.com" }));
        }
    }
}