using System;
using System.Reflection;

namespace BookProcessor.ConsoleApp
{
    internal class Program
    {
        private static void Main()
        {
            var bookStream = Assembly.GetAssembly(typeof(Implementation.BookProcessor))
                .GetManifestResourceStream("BookProcessor.Implementation.NewBooks.txt");

            var bookProcessor = new Implementation.BookProcessor();
            bookProcessor.ProcessBooks(bookStream);

            Console.ReadKey();
        }
    }
}
