using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;

namespace BookProcessor.Implementation
{
    public class BookProcessor
    {
        public void ProcessBooks(Stream stream)
        {
            var lines = new List<string>();
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            var books = new List<Book>();
            foreach (var line in lines)
            {
                var fields = line.Split('|');

                if (fields.Length != 2)
                {
                    Console.WriteLine($"WARN: Line malformed. Only {fields.Length} field(s) found.");
                    continue;
                }

                if (!decimal.TryParse(fields[1], out var price))
                {
                    Console.WriteLine($"WARN: Book price is not a valid decimal: '{fields[1]}'");
                    continue;
                }

                var title = fields[0];

                var book = new Book
                {
                    Price = price,
                    Title = title
                };

                books.Add(book);
            }

            using (var db = new LiteDatabase(@"Books.db"))
            {
                var dbBooks = db.GetCollection<Book>("books");

                dbBooks.Insert(books);
            }
            
            Console.WriteLine($"INFO: {books.Count} books processed");
        }
    }
}
