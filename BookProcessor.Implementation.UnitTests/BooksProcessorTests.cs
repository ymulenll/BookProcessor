using BookProcessor.Interfaces;
using FakeItEasy;
using NUnit.Framework;

namespace BookProcessor.Implementation.UnitTests
{
    [TestFixture]
    class BookProcessorTests
    {
        private BookProcessor _bookProcessor;

        private IBookDataProvider _bookDataProviderFake;

        private IBookParser _bookParserFake;

        private IBookStorage _bookStorageFake;

        [SetUp]
        public void Setup()
        {
            _bookDataProviderFake = A.Fake<IBookDataProvider>(options => options.Strict());

            _bookParserFake = A.Fake<IBookParser>(options => options.Strict());

            _bookStorageFake = A.Fake<IBookStorage>(options => options.Strict());

            _bookProcessor = new BookProcessor(_bookDataProviderFake, _bookParserFake, _bookStorageFake);
        }

        [Test]
        public void BasicProcessingOrchestratesFine()
        {
            var lines = new[] { "book1|1.5" };
            var books = new[] { new Book { Title = "book1", Price = 1.5M } };
            A.CallTo(() => _bookDataProviderFake.GetBookData()).Returns(lines);
            A.CallTo(() => _bookParserFake.Parse(lines)).Returns(books);
            A.CallTo(() => _bookStorageFake.Persist(books)).DoesNothing();

            _bookProcessor.ProcessBooks();

            A.CallTo(() => _bookDataProviderFake.GetBookData()).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => _bookParserFake.Parse(lines)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => _bookStorageFake.Persist(books)).MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
