using System.Collections.Generic;

namespace BookProcessor.Implementation
{
    public interface IBookStorage
    {
        void Store(IEnumerable<Book> books);
    }
}
