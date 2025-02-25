using GridCore;

namespace LifeStrategies
{
    public interface ILifeStrategy
    {
        void ProcessNextGeneration(ref GridData gridData);
    }
}