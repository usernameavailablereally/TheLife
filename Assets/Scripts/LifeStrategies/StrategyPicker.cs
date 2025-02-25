namespace LifeStrategies
{
    public class StrategyPicker
    {
        public ILifeStrategy GetTargetStrategy()
        {
            return new DefaultStrategy();
        }
    }
}