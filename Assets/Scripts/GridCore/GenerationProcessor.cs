using System;
using LifeStrategies;
using VContainer;

namespace GridCore
{
    public interface IGenerationProcessor
    {
        void ProcessNextGeneration();
    }

    public class GenerationProcessor : IGenerationProcessor
    {
        private readonly GridController _gridController;
        private readonly IStrategyContainer _strategyContainer;

        [Inject]
        public GenerationProcessor(GridController gridController, IStrategyContainer strategyContainer)
        {
            _gridController = gridController;
            _strategyContainer = strategyContainer;
        }

        public void ProcessNextGeneration()
        {
            var gridData = _gridController.GetGridData();
            if (gridData.Grid == null) throw new InvalidOperationException("Grid is not initialized");

            var strategy = _strategyContainer.GetCurrentStrategy();
            strategy.ProcessNextGeneration(ref gridData);
            _gridController.InitGrid(gridData);
        }
    }
}