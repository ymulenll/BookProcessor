using System;

namespace BookProcessor.Interfaces
{
    public interface ITimer
    {
        void Start();

        TimeSpan Stop();
    }
}
