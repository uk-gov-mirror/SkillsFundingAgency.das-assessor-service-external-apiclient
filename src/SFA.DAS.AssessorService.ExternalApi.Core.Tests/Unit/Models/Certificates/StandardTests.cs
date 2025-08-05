namespace SFA.DAS.AssessorService.ExternalApi.Core.Tests.Unit.Models.Certificates
{
    using FizzWare.NBuilder;
    using NUnit.Framework;
    using SFA.DAS.AssessorService.ExternalApi.Core.Models.Certificates;
    using System.Linq;

    [TestFixture(Category = "Models")]
    public class StandardTests
    {
        [Test]
        public void InvalidStandardCode()
        {
            // arrange
            var standard = Builder<Standard>.CreateNew().With(s => s.StandardCode = -1).Build();

            // act
            bool isValid = standard.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults.First().ErrorMessage, Is.EqualTo("A standard should be selected").IgnoreCase);
        }

        [Test]
        public void NoStandardSpecified()
        {
            // arrange
            var standard = Builder<Standard>.CreateNew().With(s => s.StandardCode = null).With(s => s.StandardReference = null).Build();

            // act
            bool isValid = standard.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.False);
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults.First().ErrorMessage, Is.EqualTo("A standard should be selected").IgnoreCase);
        }

        [Test]
        public void WhenValid()
        {
            // arrange
            var standard = Builder<Standard>.CreateNew().Build();

            // act
            bool isValid = standard.IsValid(out var validationResults);

            // assert
            Assert.That(isValid, Is.True);
            Assert.That(validationResults, Has.Count.EqualTo(0));
        }

        [Test]
        [Category("IEquatable")]
        public void WhenEqual()
        {
            // arrange
            var standard1 = Builder<Standard>.CreateNew().With(s => s.StandardCode = 1).Build();
            var standard2 = Builder<Standard>.CreateNew().With(s => s.StandardCode = 1).Build();

            // act
            bool areEqual = standard1 == standard2;

            // assert
             Assert.That(areEqual, Is.True);
        }

        [Test]
        [Category("IEquatable")]
        public void WhenNotEqual()
        {
            // arrange
            var standard1 = Builder<Standard>.CreateNew().With(s => s.StandardCode = 1).Build();
            var standard2 = Builder<Standard>.CreateNew().With(s => s.StandardCode = 2).Build();

            // act
            bool areNotEqual = standard1 != standard2;

            // assert
            Assert.That(areNotEqual, Is.True);
        }
    }
}
