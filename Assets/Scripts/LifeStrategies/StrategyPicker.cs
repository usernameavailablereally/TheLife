namespace LifeStrategies
{
    public class StrategyPicker
    {
        // can be scaled for many strategies
        public ILifeStrategy GetTargetStrategy()
        {
            var baseStrategy = new DefaultStrategy();
            return new ThreadSafeStrategyDecorator(baseStrategy);
        }
    }
}