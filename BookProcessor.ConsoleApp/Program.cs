using System;
using BookProcessor.Implementation;
using BookProcessor.Persistence;

namespace BookProcessor.ConsoleApp
{
    internal class Program
    {
        private static void Main()
        {
            //var bookProcessor = new Implementation.BookProcessor();
            //bookProcessor.ProcessBooks();
            
            var bookProcessor = ComposeDependecies();

            bookProcessor.ProcessBooks();

            Console.ReadKey();
        }

        private static Implementation.BookProcessor ComposeDependecies()
        {
            // Composition using poor's man dependency injection.
            var logger = new ConsoleLogger();
            var bookValidator = new SimpleBookValidator(logger);
            var bookMapper = new SimpleBookMapper();
            var bookParser = new SimpleBookParser(bookValidator, bookMapper);
            var bookDataProvider = new StreamBookDataProvider();
            var bookStorage = new LiteDbBookStorage(logger);

            var bookProcessor = new Implementation.BookProcessor(
                bookDataProvider,
                bookParser,
                bookStorage);

            return bookProcessor;
        }
    }
}
