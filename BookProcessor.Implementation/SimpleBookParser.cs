using System.Collections.Generic;

namespace BookProcessor.Implementation
{
    public class SimpleBookParser : IBookParser
    {
        private readonly IBookValidator _bookValidator;
        private readonly IBookMapper _bookMapper;

        public SimpleBookParser(IBookValidator bookValidator, IBookMapper bookMapper)
        {
            _bookValidator = bookValidator;
            _bookMapper = bookMapper;
        }

        public IEnumerable<Book> Parse(IEnumerable<string> lines)
        {
            var books = new List<Book>();
            foreach (var line in lines)
            {
                var fields = line.Split('|');

                // validate
                if (!_bookValidator.Validate(fields))
                {
                    continue;
                }

                // map lines to books
                var book = _bookMapper.Map(fields);

                books.Add(book);
            }

            return books;
        }
    }
}
