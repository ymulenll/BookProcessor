using System.Collections.Generic;

namespace BookProcessor.Interfaces
{
    public interface IBookStorage
    {
        void Store(IEnumerable<Book> books);
    }
}
