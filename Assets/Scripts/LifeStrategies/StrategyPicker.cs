using GridCore.Raw;

namespace LifeStrategies
{
    public class StrategyPicker
    {
        public static ILifeStrategy GetTargetStrategy()
        {
            return new DefaultStrategy();
        }
    }
}