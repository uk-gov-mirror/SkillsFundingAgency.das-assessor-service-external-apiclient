namespace SFA.DAS.AssessorService.ExternalApi.Core.Tests.Unit.Models.Epa
{
    using FizzWare.NBuilder;
    using NUnit.Framework;
    using SFA.DAS.AssessorService.ExternalApi.Core.Models.Epa;
    using System;
    using System.Linq;

    [TestFixture(Category = "Models")]
    public class EpaRecordTests
    {
        [Test]
        public void EpaDateInFuture()
        {
            // arrange
            var epaRecord = Builder<EpaRecord>.CreateNew().With(er => er.EpaDate = DateTime.UtcNow.AddDays(1)).With(er => er.EpaOutcome = "Pass").Build();

            // act
            bool isValid = epaRecord.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults.First().ErrorMessage, Is.EqualTo("An Epa date cannot be in the future").IgnoreCase);
        }

        [Test]
        public void EpaOutcomeMissing()
        {
            // arrange
            var epaRecord = Builder<EpaRecord>.CreateNew().With(er => er.EpaDate = DateTime.UtcNow).With(er => er.EpaOutcome = null).Build();

            // act
            bool isValid = epaRecord.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults.First().ErrorMessage, Is.EqualTo("Select the outcome the apprentice achieved").IgnoreCase);
        }

        [Test]
        public void EpaOutcomeInvalid()
        {
            // arrange
            var epaRecord = Builder<EpaRecord>.CreateNew().With(er => er.EpaDate = DateTime.UtcNow).With(er => er.EpaOutcome = "INVALID").Build();

            // act
            bool isValid = epaRecord.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults.First().ErrorMessage, Does.StartWith("Invalid outcome. Must one of the following:"));
        }

        [Test]
        public void WhenValid()
        {
            // arrange
            var epaRecord = Builder<EpaRecord>.CreateNew().With(er => er.EpaDate = DateTime.UtcNow).With(er => er.EpaOutcome = "Pass").Build();

            // act
            bool isValid = epaRecord.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.True);
            Assert.That(validationResults, Has.Count.EqualTo(0));
        }

        [Test]
        [Category("IEquatable")]
        public void WhenEqual()
        {
            // arrange
            var epaRecord1 = Builder<EpaRecord>.CreateNew().Build();
            var epaRecord2 = Builder<EpaRecord>.CreateNew().Build();

            // act
            bool areEqual = epaRecord1 == epaRecord2;

            // assert
             Assert.That(areEqual, Is.True);
        }

        [Test]
        [Category("IEquatable")]
        public void WhenNotEqual()
        {
            // arrange
            var epaRecord1 = Builder<EpaRecord>.CreateNew().With(er => er.EpaOutcome = "Pass").Build();
            var epaRecord2 = Builder<EpaRecord>.CreateNew().With(er => er.EpaOutcome = "Merit").Build();

            // act
            bool areNotEqual = epaRecord1 != epaRecord2;

            // assert
            Assert.That(areNotEqual, Is.True);
        }
    }
}
