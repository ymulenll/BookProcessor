using System;
using BookProcessor.Implementation;
using BookProcessor.Implementation.Adapters;
using BookProcessor.Implementation.Decorators;
using BookProcessor.Interfaces;
using BookProcessor.Persistence;
using Unity;
using Unity.Injection;

namespace BookProcessor.ConsoleApp
{
    internal class Program
    {
        private static void Main()
        {
            //var bookProcessor = new Implementation.BookProcessor();
            //bookProcessor.ProcessBooks();
            
            //ComposeUsingPoorManDependencyInjection();

            ComposeDependeciesUsingDiContainer();

            Console.ReadKey();
        }

        private static void ComposeUsingPoorManDependencyInjection()
        {
            // Composition using poor's man dependency injection.
            var logger = new ConsoleLogger();
            var bookValidator = new SimpleBookValidator(logger);
            var bookMapper = new SimpleBookMapper();
            var bookParser = new SimpleBookParser(bookValidator, bookMapper);
            var bookDataProvider = new StreamBookDataProvider();
            var bookStorage = new LiteDbBookStorage(logger);

            // pipeline
            var timer = new StopwatchTimerAdapter();
            var loggerTimerDecorator = new LoggingTimerDecorator(timer, logger);

            var bookProcessor = new Implementation.BookProcessor(
                bookDataProvider,
                bookParser,
                bookStorage);

            // pipeline
            var bookProcessorLoggerDecorator = new BookProcessorLoggerDecorator(bookProcessor, logger);
            var booksProcessorLoggerTimer = new BookProcessorTimerDecorator(bookProcessorLoggerDecorator, loggerTimerDecorator);

            booksProcessorLoggerTimer.ProcessBooks();
        }

        private static void ComposeDependeciesUsingDiContainer()
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

                // timer pipeline
                container.RegisterType<ITimer, StopwatchTimerAdapter>();
                container.RegisterType<ITimer, LoggingTimerDecorator>("LoggingTimerDecorator");

                container.RegisterType<IBookProcessor, Implementation.BookProcessor>();

                // pipeline
                container.RegisterType<IBookProcessor, BookProcessorLoggerDecorator>("BookProcessorLoggerDecorator");
                container.RegisterType<IBookProcessor, BookProcessorTimerDecorator>("BookProcessorTimerDecorator",
                    new InjectionConstructor(
                        new ResolvedParameter<IBookProcessor>("BookProcessorLoggerDecorator"),
                        new ResolvedParameter<ITimer>("LoggingTimerDecorator")));

                var bookProcessor = container.Resolve<IBookProcessor>("BookProcessorTimerDecorator");
                bookProcessor.ProcessBooks();
            }
        }

        //using Unity.RegistrationByConvention;
        //private static Implementation.BookProcessor CompositionByConvention()
        //{
        //    // Composition using unity DI container.
        //    using (var container = new UnityContainer())
        //    {
        //        container.RegisterTypes(
        //            AllClasses.FromAssembliesInBasePath(),
        //            WithMappings.FromAllInterfaces,
        //            WithName.Default,
        //            WithLifetime.Transient);

        //        return container.Resolve<Implementation.BookProcessor>();
        //    }
        //}
    }
}
