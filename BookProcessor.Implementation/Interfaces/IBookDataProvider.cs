
using System.Collections.Generic;

namespace BookProcessor.Implementation
{
    public interface IBookDataProvider
    {
        IEnumerable<string> GetBookData();
    }
}
