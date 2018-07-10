using BookProcessor.Interfaces;

namespace BookProcessor.Implementation
{
    public class BookProcessor
    {
        private readonly IBookDataProvider _bookDataProvider;
        private readonly IBookParser _bookParser;
        private readonly IBookStorage _bookStorage;

        public BookProcessor(IBookDataProvider bookDataProvider,
            IBookParser bookParser, 
            IBookStorage bookStorage)
        {
            _bookDataProvider = bookDataProvider;
            _bookParser = bookParser;
            _bookStorage = bookStorage;
        }

        public void ProcessBooks()
        {
            var bookData = _bookDataProvider.GetBookData();

            var books = _bookParser.Parse(bookData);

            _bookStorage.Persist(books);
        }
    }
}