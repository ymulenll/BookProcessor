using System;
using BookProcessor.Interfaces;

namespace BookProcessor.Implementation.Decorators
{
    public class LoggingTimerDecorator : ITimer
    {
        private readonly ILogger _logger;
        private readonly ITimer _decoratedTimmer;
        public LoggingTimerDecorator(ITimer decoratedTimmer, ILogger logger)
        {
            _logger = logger;
            _decoratedTimmer = decoratedTimmer;
        }

        public void Start()
        {
            _decoratedTimmer.Start();
        }

        public TimeSpan Stop()
        {
            var elapsedTime = _decoratedTimmer.Stop();
            _logger.LogInfo($"The method took {elapsedTime.TotalSeconds} secounds to complete");
            return elapsedTime;
        }
    }
}
