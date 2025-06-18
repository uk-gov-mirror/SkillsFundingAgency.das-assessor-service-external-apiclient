namespace SFA.DAS.AssessorService.ExternalApi.Core.Tests.Unit.Messages.Requests.Certificates
{
    using FizzWare.NBuilder;
    using NUnit.Framework;
    using SFA.DAS.AssessorService.ExternalApi.Core.Messages.Request.Certificates;
    using SFA.DAS.AssessorService.ExternalApi.Core.Models.Certificates;
    using System.Linq;

    [TestFixture(Category = "Requests")]
    public class CreateCertificateTests
    {
        [Test]
        public void LearnerMissing()
        {
            // arrange
            var learningDetails = Builder<LearningDetails>.CreateNew().With(l => l.OverallGrade = "Pass").Build();
            var standard = Builder<Standard>.CreateNew().Build();
            var postalContact = Builder<PostalContact>.CreateNew().With(l => l.PostCode = "ZY9 9ZY").Build();

            var certificate = Builder<CreateCertificateRequest>.CreateNew().With(cd => cd.Learner = null)
                                                                        .With(cd => cd.Standard = standard)
                                                                        .With(cd => cd.LearningDetails = learningDetails)
                                                                        .With(cd => cd.PostalContact = postalContact).Build();

            // act
            bool isValid = certificate.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults.First().ErrorMessage, Is.EqualTo("Learner is required").IgnoreCase);

        }

        [Test]
        public void StandardMissing()
        {
            // arrange
            var learner = Builder<Learner>.CreateNew().With(l => l.Uln = 1243567890).Build();
            var learningDetails = Builder<LearningDetails>.CreateNew().With(l => l.OverallGrade = "Pass").Build();
            var postalContact = Builder<PostalContact>.CreateNew().With(l => l.PostCode = "ZY9 9ZY").Build();

            var certificate = Builder<CreateCertificateRequest>.CreateNew().With(cd => cd.Learner = learner)
                                                                        .With(cd => cd.Standard = null)
                                                                        .With(cd => cd.LearningDetails = learningDetails)
                                                                        .With(cd => cd.PostalContact = postalContact).Build();

            // act
            bool isValid = certificate.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults.First().ErrorMessage, Is.EqualTo("Standard is required").IgnoreCase);
        }

        [Test]
        public void LearningDetailsMissing()
        {
            // arrange
            var learner = Builder<Learner>.CreateNew().With(l => l.Uln = 1243567890).Build();
            var standard = Builder<Standard>.CreateNew().Build();
            var postalContact = Builder<PostalContact>.CreateNew().With(l => l.PostCode = "ZY9 9ZY").Build();

            var certificate = Builder<CreateCertificateRequest>.CreateNew().With(cd => cd.Learner = learner)
                                                                        .With(cd => cd.Standard = standard)
                                                                        .With(cd => cd.LearningDetails = null)
                                                                        .With(cd => cd.PostalContact = postalContact).Build();

            // act
            bool isValid = certificate.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults.First().ErrorMessage, Is.EqualTo("LearningDetails is required").IgnoreCase);
        }

        [Test]
        public void PostalContactMissing()
        {
            // arrange
            var learner = Builder<Learner>.CreateNew().With(l => l.Uln = 1243567890).Build();
            var standard = Builder<Standard>.CreateNew().Build();
            var learningDetails = Builder<LearningDetails>.CreateNew().With(l => l.OverallGrade = "Pass").Build();

            var certificate = Builder<CreateCertificateRequest>.CreateNew().With(cd => cd.Learner = learner)
                                                                        .With(cd => cd.Standard = standard)
                                                                        .With(cd => cd.LearningDetails = learningDetails)
                                                                        .With(cd => cd.PostalContact = null).Build();

            // act
            bool isValid = certificate.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults.First().ErrorMessage, Is.EqualTo("PostalContact is required").IgnoreCase);
        }

        [Test]
        public void WhenValid()
        {
            // arrange
            var learner = Builder<Learner>.CreateNew().With(l => l.Uln = 1243567890).Build();
            var standard = Builder<Standard>.CreateNew().Build();
            var learningDetails = Builder<LearningDetails>.CreateNew().With(l => l.OverallGrade = "Pass").Build();
            var postalContact = Builder<PostalContact>.CreateNew().With(l => l.PostCode = "ZY9 9ZY").Build();

            var certificate = Builder<CreateCertificateRequest>.CreateNew().With(cd => cd.Learner = learner)
                                                                        .With(cd => cd.Standard = standard)
                                                                        .With(cd => cd.LearningDetails = learningDetails)
                                                                        .With(cd => cd.PostalContact = postalContact).Build();

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
            var learner = Builder<Learner>.CreateNew().Build();
            var standard = Builder<Standard>.CreateNew().Build();
            var learningDetails = Builder<LearningDetails>.CreateNew().Build();
            var postalContact = Builder<PostalContact>.CreateNew().Build();

            var certificate1 = Builder<CreateCertificateRequest>.CreateNew().With(cd => cd.Learner = learner)
                                                                        .With(cd => cd.Standard = standard)
                                                                        .With(cd => cd.LearningDetails = learningDetails)
                                                                        .With(cd => cd.PostalContact = postalContact).Build();

            var certificate2 = Builder<CreateCertificateRequest>.CreateNew().With(cd => cd.Learner = learner)
                                                                        .With(cd => cd.Standard = standard)
                                                                        .With(cd => cd.LearningDetails = learningDetails)
                                                                        .With(cd => cd.PostalContact = postalContact).Build();

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
            var learningDetails = Builder<LearningDetails>.CreateNew().Build();
            var standard = Builder<Standard>.CreateNew().Build();
            var postalContact = Builder<PostalContact>.CreateNew().Build();

            var certificate1 = Builder<CreateCertificateRequest>.CreateNew().With(cd => cd.Learner = Builder<Learner>.CreateNew().Build())
                                                                        .With(cd => cd.Learner.FamilyName = "1")
                                                                        .With(cd => cd.Standard = standard)
                                                                        .With(cd => cd.LearningDetails = learningDetails)
                                                                        .With(cd => cd.PostalContact = postalContact).Build();

            var certificate2 = Builder<CreateCertificateRequest>.CreateNew().With(cd => cd.Learner = Builder<Learner>.CreateNew().Build())
                                                                        .With(cd => cd.Learner.FamilyName = "2")
                                                                        .With(cd => cd.Standard = standard)
                                                                        .With(cd => cd.LearningDetails = learningDetails)
                                                                        .With(cd => cd.PostalContact = postalContact).Build();

            // act
            bool areNotEqual = certificate1 != certificate2;

            // assert
            Assert.That(areNotEqual, Is.True);
        }
    }
}
