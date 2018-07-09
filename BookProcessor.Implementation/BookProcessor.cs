using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookProcessor.Implementation
{
    public class BookProcessor
    {
        public void ProcessBooks(Stream stream)
        {
            var lines = ReadBookData(stream);

            var books = ParseBooks(lines);
            
            StoreBooks(books);
        }

        private static void StoreBooks(IEnumerable<Book> books)
        {
            var bookList = books.ToList();
            using (var db = new LiteDatabase(@"Books.db"))
            {
                var dbBooks = db.GetCollection<Book>("books");

                dbBooks.Insert(bookList);
            }

            // logging
            LogMessage($"INFO: {bookList.Count()} books processed");
        }

        private static IEnumerable<Book> ParseBooks(IEnumerable<string> lines)
        {
            var books = new List<Book>();
            foreach (var line in lines)
            {
                var fields = line.Split('|');

                // validate
                if (!ValidateBookData(fields))
                {
                    continue;
                }

                // map lines to books
                Book book = MapBookDataToBookEntity(fields);

                books.Add(book);
            }

            return books;
        }

        private static Book MapBookDataToBookEntity(string[] fields)
        {
            var title = fields[0];
            var price = decimal.Parse(fields[1]);

            var book = new Book
            {
                Price = price,
                Title = title
            };
            return book;
        }

        private static bool ValidateBookData(string[] fields)
        {
            if (fields.Length != 2)
            {
                LogMessage($"WARN: Line malformed. Only {fields.Length} field(s) found.");
                return false;
            }

            if (!decimal.TryParse(fields[1], out _))
            {
                LogMessage($"WARN: Book price is not a valid decimal: '{fields[1]}'");
                return false;
            }

            return true;
        }

        private static void LogMessage(string message)
        {
            Console.WriteLine(message);
        }

        private static IEnumerable<string> ReadBookData(Stream stream)
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

            return lines;
        }
    }
}
