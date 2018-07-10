﻿
namespace BookProcessor.Implementation
{
    public class SimpleBookMapper : IBookMapper
    {
        public Book Map(string[] fields)
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
    }
}