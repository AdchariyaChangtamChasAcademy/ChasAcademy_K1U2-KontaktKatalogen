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
        [Theory]
        [InlineData("validEmail@Example.com")]
        [InlineData("SecondValidEmail@Example.com")]
        public void GivenValidEmail_WhenValidated_ThenReturnTrue(string email)
        {
            // Given, email from inlinedata
            // When, validating email with EmailValidator.IsValidEmail()
            bool result = EmailValidator.IsValidEmail(email);

            // Then, verify if true
            Assert.True(result);
        }

        [Fact]
        public void GivenInvalidEmail_WhenValidated_ThenReturnFalse()
        {
            // Given, email "invalidEmailExample"
            // When, validating email with EmailValidator.IsValidEmail()
            bool result = EmailValidator.IsValidEmail("invalidEmailExample");

            // Then, verify if false
            Assert.False(result);
        }

        [Fact]
        public void GivenExistingEmail_WhenAddingContact_ThenThrowDuplicateEmailException()
        {
            // Given, a contact service with one contact already added
            var logger = Mock.Of<ILogger<ContactService>>();
            var service = new ContactService(logger);
            var existingContact = new Contact { Name = "Alice", Email = "testDuplicate@mail.com" };
            service.AddContact(existingContact);

            // When, we try to add another contact with the same email
            var newContact = new Contact { Name = "Bob", Email = "testDuplicate@mail.com" };

            // Then, a DuplicateEmailException is thrown
            var exception = Assert.Throws<DuplicateEmailException>(() => service.AddContact(newContact));

            // (Verify if exception message is as expected)
            Assert.Equal("'testDuplicate@mail.com' already exists.", exception.Message);
        }
    }
}