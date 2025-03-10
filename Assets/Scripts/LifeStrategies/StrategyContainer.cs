using System;

namespace LifeStrategies
{
    public interface IStrategyContainer
    {
        void SetStrategy(ILifeStrategy strategy);
        ILifeStrategy GetCurrentStrategy();
    }

    public class StrategyContainer : IStrategyContainer 
    {
        private ILifeStrategy _currentStrategy;
        private readonly object _lock = new();

        public void SetStrategy(ILifeStrategy strategy)
        {
            lock (_lock)
            {
                _currentStrategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
            }
        }

        public ILifeStrategy GetCurrentStrategy()
        {
            lock (_lock)
            {
                return _currentStrategy ?? throw new InvalidOperationException("Strategy is not set");
            }
        }
    }
}