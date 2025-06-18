namespace SFA.DAS.AssessorService.ExternalApi.Core.Tests.Unit.Messages.Requests.Certificates
{
    using FizzWare.NBuilder;
    using NUnit.Framework;
    using SFA.DAS.AssessorService.ExternalApi.Core.Messages.Request.Certificates;
    using System.Linq;

    [TestFixture(Category = "Requests")]
    public class DeleteCertificateTests
    {
        [Test]
        public void UlnInvalid()
        {
            // arrange
            var certificate = Builder<DeleteCertificateRequest>.CreateNew().With(sc => sc.Uln = 12435).Build();

            // act
            bool isValid = certificate.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults.First().ErrorMessage, Is.EqualTo("The apprentice's ULN should contain exactly 10 numbers").IgnoreCase);
        }

        [Test]
        public void NoStandardSpecified()
        {
            // arrange
            var certificate = Builder<DeleteCertificateRequest>.CreateNew().With(sc => sc.Uln = 1243567890).With(sc => sc.Standard = null).Build();

            // act
            bool isValid = certificate.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults.First().ErrorMessage, Is.EqualTo("A standard should be selected").IgnoreCase);
        }

        [Test]
        public void FamilyNameMissing()
        {
            // arrange
            var certificate = Builder<DeleteCertificateRequest>.CreateNew().With(sc => sc.Uln = 1243567890).With(l => l.FamilyName = null).Build();

            // act
            bool isValid = certificate.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults.First().ErrorMessage, Is.EqualTo("Enter the apprentice's last name").IgnoreCase);
        }

        [Test]
        public void CertificateReferenceMissing()
        {
            // arrange
            var certificate = Builder<DeleteCertificateRequest>.CreateNew().With(sc => sc.Uln = 1243567890).With(sc => sc.CertificateReference = null).Build();

            // act
            bool isValid = certificate.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults.First().ErrorMessage, Is.EqualTo("Enter the certificate reference").IgnoreCase);
        }

        [Test]
        public void WhenValid()
        {
            // arrange
            var certificate = Builder<DeleteCertificateRequest>.CreateNew().With(sc => sc.Uln = 1243567890).Build();

            // act
            bool isValid = certificate.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.True);
            Assert.That(validationResults, Has.Count.EqualTo(0));
        }

        [Test]
        [Category("IEquatable")]
        public void WhenEqual()
        {
            // arrange
            var certificate1 = Builder<DeleteCertificateRequest>.CreateNew().With(sc => sc.Standard = "1").Build();
            var certificate2 = Builder<DeleteCertificateRequest>.CreateNew().With(sc => sc.Standard = "1").Build();

            // act
            bool areEqual = certificate1 == certificate2;

            // assert
            Assert.That(areEqual, Is.True);
        }

        [Test]
        [Category("IEquatable")]
        public void WhenNotEqual()
        {
            // arrange
            var certificate1 = Builder<DeleteCertificateRequest>.CreateNew().With(sc => sc.Standard = "1").Build();
            var certificate2 = Builder<DeleteCertificateRequest>.CreateNew().With(sc => sc.Standard = "9").Build();

            // act
            bool areNotEqual = certificate1 != certificate2;

            // assert
            Assert.That(areNotEqual, Is.True);
        }
    }
}
