namespace GridCore.Raw
{
    public interface ILifeStrategy
    {
        void ProcessNextGeneration(ref GridData gridData);
    }
}