
using System;
using System.Diagnostics;
using BookProcessor.Interfaces;

namespace BookProcessor.Implementation.Adapters
{
    public class StopwatchTimerAdapter : ITimer
    {
        private readonly Stopwatch _stopwatch;
        public StopwatchTimerAdapter()
        {
            _stopwatch = new Stopwatch();
        }

        public void Start()
        {
            _stopwatch.Start();
        }

        public TimeSpan Stop()
        {
            _stopwatch.Stop();
            var elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
            _stopwatch.Reset();
            return TimeSpan.FromMilliseconds(elapsedMilliseconds);
        }
    }
}
