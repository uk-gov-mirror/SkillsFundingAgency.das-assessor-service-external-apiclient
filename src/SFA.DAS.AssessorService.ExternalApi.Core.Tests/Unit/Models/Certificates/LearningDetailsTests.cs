namespace SFA.DAS.AssessorService.ExternalApi.Core.Tests.Unit.Models.Certificates
{
    using FizzWare.NBuilder;
    using NUnit.Framework;
    using SFA.DAS.AssessorService.ExternalApi.Core.Models.Certificates;
    using System;
    using System.Linq;

    [TestFixture(Category = "Models")]
    public class LearningDetailsTests
    {
        [Test]
        public void AchievementDateBeforeDigitalCertificates()
        {
            // arrange
            DateTime firstDigitalCertificate = new DateTime(2017, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var learningDetails = Builder<LearningDetails>.CreateNew().With(l => l.AchievementDate = firstDigitalCertificate.AddDays(-1)).With(l => l.OverallGrade = "Pass").Build();

            // act
            bool isValid = learningDetails.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults.First().ErrorMessage, Does.StartWith("An achievement date cannot be before 01 01 2017"));

        }

        [Test]
        public void AchievementDateInFuture()
        {
            // arrange
            var learningDetails = Builder<LearningDetails>.CreateNew().With(l => l.AchievementDate = DateTime.UtcNow.AddDays(1)).With(l => l.OverallGrade = "Pass").Build();

            // act
            bool isValid = learningDetails.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults.First().ErrorMessage, Is.EqualTo("An achievement date cannot be in the future").IgnoreCase);
        }

        [Test]
        public void AchievementDateMissing()
        {
            // arrange
            var learningDetails = Builder<LearningDetails>.CreateNew().With(l => l.AchievementDate = null).With(l => l.OverallGrade = "Pass").Build();

            // act
            bool isValid = learningDetails.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults.First().ErrorMessage, Is.EqualTo("Enter the achievement date").IgnoreCase);
        }

        [Test]
        public void OverallGradeMissing()
        {
            // arrange
            var learningDetails = Builder<LearningDetails>.CreateNew().With(l => l.AchievementDate = DateTime.UtcNow).With(l => l.OverallGrade = null).Build();

            // act
            bool isValid = learningDetails.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults.First().ErrorMessage, Is.EqualTo("Select the grade the apprentice achieved").IgnoreCase);
        }

        [Test]
        public void OverallGradeInvalid()
        {
            // arrange
            var learningDetails = Builder<LearningDetails>.CreateNew().With(l => l.AchievementDate = DateTime.UtcNow).With(l => l.OverallGrade = "INVALID").Build();

            // act
            bool isValid = learningDetails.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults.First().ErrorMessage, Does.StartWith("Invalid grade"));
        }

        [Test]
        public void WhenValid()
        {
            // arrange
            var learningDetails = Builder<LearningDetails>.CreateNew().With(l => l.AchievementDate = DateTime.UtcNow).With(l => l.OverallGrade = "Pass").Build();

            // act
            bool isValid = learningDetails.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.True);
            Assert.That(validationResults, Has.Count.EqualTo(0));
        }

        [Test]
        [Category("IEquatable")]
        public void WhenEqual()
        {
            // arrange
            var learningDetails1 = Builder<LearningDetails>.CreateNew().Build();
            var learningDetails2 = Builder<LearningDetails>.CreateNew().Build();

            // act
            bool areEqual = learningDetails1 == learningDetails2;

            // assert
             Assert.That(areEqual, Is.True);
        }

        [Test]
        [Category("IEquatable")]
        public void WhenNotEqual()
        {
            // arrange
            var learningDetails1 = Builder<LearningDetails>.CreateNew().With(l => l.OverallGrade = "Pass").Build();
            var learningDetails2 = Builder<LearningDetails>.CreateNew().With(l => l.OverallGrade = "Merit").Build();

            // act
            bool areNotEqual = learningDetails1 != learningDetails2;

            // assert
            Assert.That(areNotEqual, Is.True);
        }
    }
}
