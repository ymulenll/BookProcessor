using System;
using BookProcessor.Implementation;
using BookProcessor.Interfaces;
using BookProcessor.Persistence;
using Unity;
using Unity.RegistrationByConvention;

namespace BookProcessor.ConsoleApp
{
    internal class Program
    {
        private static void Main()
        {
            //var bookProcessor = new Implementation.BookProcessor();
            //bookProcessor.ProcessBooks();
            
            var bookProcessor = ComposeDependeciesUsingDiContainer();

            bookProcessor.ProcessBooks();

            Console.ReadKey();
        }

        //private static Implementation.BookProcessor ComposeDependecies()
        //{
        //    // Composition using poor's man dependency injection.
        //    var logger = new ConsoleLogger();
        //    var bookValidator = new SimpleBookValidator(logger);
        //    var bookMapper = new SimpleBookMapper();
        //    var bookParser = new SimpleBookParser(bookValidator, bookMapper);
        //    var bookDataProvider = new StreamBookDataProvider();
        //    var bookStorage = new LiteDbBookStorage(logger);

        //    var bookProcessor = new Implementation.BookProcessor(
        //        bookDataProvider,
        //        bookParser,
        //        bookStorage);

        //    return bookProcessor;
        //}

        private static Implementation.BookProcessor ComposeDependeciesUsingDiContainer()
        {
            // Composition using unity DI container.
            using (var container = new UnityContainer())
            {
                container.RegisterType<ILogger, ConsoleLogger>();
                container.RegisterType<IBookValidator, SimpleBookValidator>();
                container.RegisterType<IBookParser, SimpleBookParser>();
                container.RegisterType<IBookMapper, SimpleBookMapper>();
                container.RegisterType<IBookDataProvider, StreamBookDataProvider>();
                container.RegisterType<IBookStorage, LiteDbBookStorage>();

                container.RegisterType<Implementation.BookProcessor>();

                return container.Resolve<Implementation.BookProcessor>();
            }
        }

        private static Implementation.BookProcessor CompositionByConvention()
        {
            // Composition using unity DI container.
            using (var container = new UnityContainer())
            {
                container.RegisterTypes(
                    AllClasses.FromAssembliesInBasePath(),
                    WithMappings.FromAllInterfaces,
                    WithName.Default,
                    WithLifetime.Transient);

                return container.Resolve<Implementation.BookProcessor>();
            }
        }

        // container.RegisterType<Implementation.BookProcessor>(new InjectionConstructor(container.Resolve<IBookParser>(), container.Resolve<IBookStorage>()));
    }
}
