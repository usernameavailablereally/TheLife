using GridCore;

namespace LifeStrategies
{
    public class ThreadSafeStrategyDecorator : ILifeStrategy
    {
        private readonly ILifeStrategy _strategy;
        private readonly object _lock = new object();

        public ThreadSafeStrategyDecorator(ILifeStrategy strategy)
        {
            _strategy = strategy;
        }

        public void ProcessNextGeneration(ref GridData gridData)
        {
            lock (_lock)
            {
                _strategy.ProcessNextGeneration(ref gridData);
            }
        }
    }
}