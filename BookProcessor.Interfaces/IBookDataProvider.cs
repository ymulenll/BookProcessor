
using System.Collections.Generic;

namespace BookProcessor.Interfaces
{
    public interface IBookDataProvider
    {
        IEnumerable<string> GetBookData();
    }
}
