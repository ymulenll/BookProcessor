using System.Collections.Generic;
using System.Linq;
using LiteDB;

namespace BookProcessor.Implementation
{
    public class LiteDbBookStorage : IBookStorage
    {
        private readonly ILogger _logger;

        public LiteDbBookStorage(ILogger logger)
        {
            _logger = logger;
        }

        public void Store(IEnumerable<Book> books)
        {
            var bookList = books.ToList();
            using (var db = new LiteDatabase(@"Books.db"))
            {
                var dbBooks = db.GetCollection<Book>("books");

                dbBooks.Insert(bookList);
            }

            // logging
            _logger.LogInfo($"{bookList.Count} books processed");
        }
    }
}
