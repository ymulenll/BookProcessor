using System.Collections.Generic;

namespace BookProcessor.Interfaces
{
    public interface IBookStorage
    {
        void Persist(IEnumerable<Book> books);
    }
}
