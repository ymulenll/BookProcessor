using System;
using System.Collections.Generic;
using BookProcessor.Interfaces;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace BookProcessor.Implementation.Interceptors
{
    public class TimerInterceptionBehavior : IInterceptionBehavior
    {
        private readonly ITimer _timer;
        public TimerInterceptionBehavior(ITimer timer)
        {
            _timer = timer;
        }
        public bool WillExecute => true;

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            // Before invoking the method on the original target.
            _timer.Start();

            // Invoke the next behavior in the chain.
            var result = getNext()(input, getNext);

            // After invoking the method on the original target.
            _timer.Stop();

            return result;
        }
    }
}
