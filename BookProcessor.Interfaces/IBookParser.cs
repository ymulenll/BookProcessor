using System.Collections.Generic;

namespace BookProcessor.Interfaces
{
    public interface IBookParser
    {
        IEnumerable<Book> Parse(IEnumerable<string> lines);
    }
}
