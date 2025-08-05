namespace SFA.DAS.AssessorService.ExternalApi.Core.Tests.Unit.Models.Certificates
{
    using FizzWare.NBuilder;
    using NUnit.Framework;
    using SFA.DAS.AssessorService.ExternalApi.Core.Models.Certificates;
    using System.Linq;

    [TestFixture(Category = "Models")]
    public class PostalContactTests
    {
        [Test]
        public void PostCodeInvalid()
        {
            // arrange
            var postalContact = Builder<PostalContact>.CreateNew().With(l => l.PostCode = "INVALID").Build();

            // act
            bool isValid = postalContact.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults.First().ErrorMessage, Is.EqualTo("Enter a valid UK postcode").IgnoreCase);
        }

        [Test]
        public void PostCodeMissing()
        {
            // arrange
            var postalContact = Builder<PostalContact>.CreateNew().With(l => l.PostCode = null).Build();

            // act
            bool isValid = postalContact.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults.First().ErrorMessage, Is.EqualTo("Enter a postcode").IgnoreCase);
        }

        [Test]
        public void ContactNameMissing()
        {
            // arrange
            var postalContact = Builder<PostalContact>.CreateNew().With(l => l.PostCode = "ZY9 9ZY").With(l => l.ContactName = null).Build();

            // act
            bool isValid = postalContact.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults.First().ErrorMessage, Is.EqualTo("Enter a contact name").IgnoreCase);
        }

        [Test]
        public void AddressMissing()
        {
            // arrange
            var postalContact = Builder<PostalContact>.CreateNew().With(l => l.PostCode = "ZY9 9ZY").With(l => l.AddressLine1 = null).Build();

            // act
            bool isValid = postalContact.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults.First().ErrorMessage, Is.EqualTo("Enter an address").IgnoreCase);
        }

        [Test]
        public void CityMissing()
        {
            // arrange
            var postalContact = Builder<PostalContact>.CreateNew().With(l => l.PostCode = "ZY9 9ZY").With(l => l.City = null).Build();

            // act
            bool isValid = postalContact.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults.First().ErrorMessage, Is.EqualTo("Enter a city or town").IgnoreCase);
        }

        [Test]
        public void WhenValid()
        {
            // arrange
            var postalContact = Builder<PostalContact>.CreateNew().With(l => l.PostCode = "ZY9 9ZY").Build();

            // act
            bool isValid = postalContact.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.True);
            Assert.That(validationResults, Has.Count.EqualTo(0));
        }

        [Test]
        [Category("IEquatable")]
        public void WhenEqual()
        {
            // arrange
            var postalContact1 = Builder<PostalContact>.CreateNew().With(l => l.PostCode = "ZY9 9ZY").Build();
            var postalContact2 = Builder<PostalContact>.CreateNew().With(l => l.PostCode = "ZY9 9ZY").Build();

            // act
            bool areEqual = postalContact1 == postalContact2;

            // assert
             Assert.That(areEqual, Is.True);
        }

        [Test]
        [Category("IEquatable")]
        public void WhenNotEqual()
        {
            // arrange
            var postalContact1 = Builder<PostalContact>.CreateNew().With(l => l.PostCode = "ZY9 9ZY").Build();
            var postalContact2 = Builder<PostalContact>.CreateNew().With(l => l.PostCode = "AA1 1AA").Build();

            // act
            bool areNotEqual = postalContact1 != postalContact2;

            // assert
            Assert.That(areNotEqual, Is.True);
        }
    }
}
