using BookProcessor.Interfaces;

namespace BookProcessor.Implementation
{
    public class BookProcessorLoggerDecorator : IBookProcessor
    {
        private readonly ILogger _logger;
        private readonly IBookProcessor _decoratedBookProcessor;

        public BookProcessorLoggerDecorator(IBookProcessor decoratedBookProcessor, ILogger logger)
        {
            _logger = logger;
            _decoratedBookProcessor = decoratedBookProcessor;
        }

        public void ProcessBooks()
        {
            _logger.LogInfo("start process");
            _decoratedBookProcessor.ProcessBooks();
            _logger.LogInfo("end process");
        }
    }
}
