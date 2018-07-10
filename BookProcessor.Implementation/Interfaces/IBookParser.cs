using System.Collections.Generic;

namespace BookProcessor.Implementation
{
    public interface IBookParser
    {
        IEnumerable<Book> Parse(IEnumerable<string> lines);
    }
}
