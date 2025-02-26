namespace LifeStrategies
{
    public class StrategyPicker
    {
        // can be scaled for many strategies
        public ILifeStrategy GetTargetStrategy()
        {
            return new DefaultStrategy();
        }
    }
}