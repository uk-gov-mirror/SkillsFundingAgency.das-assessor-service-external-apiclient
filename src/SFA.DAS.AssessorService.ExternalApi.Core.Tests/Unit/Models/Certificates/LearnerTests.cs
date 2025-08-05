namespace SFA.DAS.AssessorService.ExternalApi.Core.Tests.Unit.Models.Certificates
{
    using FizzWare.NBuilder;
    using NUnit.Framework;
    using SFA.DAS.AssessorService.ExternalApi.Core.Models.Certificates;
    using System.Linq;

    [TestFixture(Category = "Models")]
    public class LearnerTests
    {
        [Test]
        public void UlnInvalid()
        {
            // arrange
            var learner = Builder<Learner>.CreateNew().With(l => l.Uln = 12435).Build();

            // act
            bool isValid = learner.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults.First().ErrorMessage, Is.EqualTo("The apprentice's ULN should contain exactly 10 numbers").IgnoreCase);
        }

        [Test]
        public void GivenNamesMissing()
        {
            // arrange
            var learner = Builder<Learner>.CreateNew().With(l => l.Uln = 1243567890).With(l => l.GivenNames = null).Build();

            // act
            bool isValid = learner.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults.First().ErrorMessage, Is.EqualTo("Enter the apprentice's first name").IgnoreCase);
        }

        [Test]
        public void FamilyNameMissing()
        {
            // arrange
            var learner = Builder<Learner>.CreateNew().With(l => l.Uln = 1243567890).With(l => l.FamilyName = null).Build();

            // act
            bool isValid = learner.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults.First().ErrorMessage, Is.EqualTo("Enter the apprentice's last name").IgnoreCase);
        }

        [Test]
        public void WhenValid()
        {
            // arrange
            var learner = Builder<Learner>.CreateNew().With(l => l.Uln = 1243567890).Build();

            // act
            bool isValid = learner.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.True);
            Assert.That(validationResults, Has.Count.EqualTo(0));
        }

        [Test]
        [Category("IEquatable")]
        public void WhenEqual()
        {
            // arrange
            var learner1 = Builder<Learner>.CreateNew().With(l => l.Uln = 1243567890).Build();
            var learner2 = Builder<Learner>.CreateNew().With(l => l.Uln = 1243567890).Build();

            // act
            bool areEqual = learner1 == learner2;

            // assert
             Assert.That(areEqual, Is.True);
        }

        [Test]
        [Category("IEquatable")]
        public void WhenNotEqual()
        {
            // arrange
            var learner1 = Builder<Learner>.CreateNew().With(l => l.Uln = 1243567890).Build();
            var learner2 = Builder<Learner>.CreateNew().With(l => l.Uln = 9876543210).Build();

            // act
            bool areNotEqual = learner1 != learner2;

            // assert
            Assert.That(areNotEqual, Is.True);
        }
    }
}
