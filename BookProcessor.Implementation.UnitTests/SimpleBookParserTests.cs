using System.Collections.Generic;
using BookProcessor.Interfaces;
using FakeItEasy;
using NUnit.Framework;

namespace BookProcessor.Implementation.UnitTests
{
    [TestFixture]
    class SimpleBookParserTests
    {
        private IBookParser _booksParser;

        private IBookValidator _booksValidatorFake;

        private IBookMapper _booksMapperFake;

        [SetUp]
        public void Setup()
        {
            _booksValidatorFake = A.Fake<IBookValidator>();
            _booksMapperFake = A.Fake<IBookMapper>();

            _booksParser = new SimpleBookParser(_booksValidatorFake, _booksMapperFake);
        }

        [Test]
        public void BasicParsingWorksFine()
        {
            IEnumerable<string> lines = new[] { "book1|10.1", "book2|20.1" };

            var expected = new[]
            {
                new Book { Title = "book1", Price = 10.1M },
                new Book { Title = "book2", Price = 20.1M }
            };

            A.CallTo(() => _booksValidatorFake.Validate(A<string[]>._)).Returns(true);
            A.CallTo(() => _booksMapperFake.Map(A<string[]>._)).ReturnsNextFromSequence(expected);

            var result = _booksParser.Parse(lines);

            CollectionAssert.AreEqual(expected, result);
        }
    }
}
