using BookProcessor.Interfaces;
using FakeItEasy;
using NUnit.Framework;

namespace BookProcessor.Implementation.UnitTests
{
    [TestFixture]
    public class SimpleBooksValidatorTests
    {
        private IBookValidator _bookValidator;
        private ILogger _loggerFake;

        [SetUp]
        public void Setup()
        {
            _loggerFake = A.Fake<ILogger>();
            _bookValidator = new SimpleBookValidator(_loggerFake);
        }

        [Test]
        public void Validate_ValidInput_ReturnsTrue()
        {
            string[] fields = { "hello", "25.36" };
            var result = _bookValidator.Validate(fields);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void Validate_WrongPrice_RetursFalse()
        {
            string[] fields = { "hello", "wrong price" };
            var result = _bookValidator.Validate(fields);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void Validate_WrongPrice_LogsWarning()
        {
            string[] fields = { "hello", "wrong price" };
            _bookValidator.Validate(fields);

            A.CallTo(() => _loggerFake.LogWarning(A<string>._)).MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
