using System.Collections.Generic;

namespace BookProcessor.Implementation
{
    public interface IBookStorage
    {
        void Persist(IEnumerable<Book> books);
    }
}
