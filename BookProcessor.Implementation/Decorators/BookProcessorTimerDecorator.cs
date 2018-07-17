using BookProcessor.Interfaces;

namespace BookProcessor.Implementation
{
    public class BookProcessorTimerDecorator : IBookProcessor
    {
        private readonly IBookProcessor _decoratedBookProcessor;
        private readonly ITimer _timer;
        public BookProcessorTimerDecorator(IBookProcessor decoratedBookProcessor, ITimer timer)
        {
            _decoratedBookProcessor = decoratedBookProcessor;
            _timer = timer;
        }

        public void ProcessBooks()
        {
            _timer.Start();
            _decoratedBookProcessor.ProcessBooks();
            _timer.Stop();
        }
    }
}
