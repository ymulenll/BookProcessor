using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace BookProcessor.Implementation
{
    public class StreamBookDataProvider : IBookDataProvider
    {
        public IEnumerable<string> GetBookData()
        {
            var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("BookProcessor.Implementation.NewBooks.txt");

            var lines = new List<string>();
            if (stream == null) return lines;

            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines;
        }
    }
}
