using System;
using System.Collections.Generic;
using BookProcessor.Interfaces;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace BookProcessor.Implementation.Interceptors
{
    public class LoggingInterceptionBehavior : IInterceptionBehavior
    {
        private readonly ILogger _logger;
        public LoggingInterceptionBehavior(ILogger logger)
        {
            _logger = logger;
        }
        public bool WillExecute => true;

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            // Before invoking the method on the original target.
            _logger.LogInfo($"Method {input.MethodBase.Name} started");

            // Invoke the next behavior in the chain.
            var result = getNext()(input, getNext);

            // After invoking the method on the original target.
            _logger.LogInfo($"Method {input.MethodBase.Name} Finished");

            return result;
        }
    }
}
