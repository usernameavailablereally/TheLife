using System;
using GridCore;
using LifeStrategies;
using UnityEngine;

namespace GameSystem
{
    public class NewGameInitializer : INewGameInitializer
    {
        private readonly IStrategyContainer _strategyContainer;
        private readonly GridController _gridController;
        private readonly GridSettingsObject _gridSettings;

        private NewGameInitializer(GridController gridController, GridSettingsObject gridSettings, IStrategyContainer strategyContainer)
        {
            _gridController = gridController;
            _gridSettings = gridSettings;
            _strategyContainer = strategyContainer;
        }
        
        public void InitializeStrategy()
        {
            try 
            {
                var strategy = new StrategyPicker().GetTargetStrategy();
                _strategyContainer.SetStrategy(strategy);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Strategy initialization failed: {ex.Message}");
                throw;
            }
        }

        public void InitialRandomGrid()
        {    
            _gridController.InitGrid(GridGenerator.CreateRandomGrid(_gridSettings.Width, _gridSettings.Height));
            _gridController.DrawGrid();
            Debug.Log("Random grid generated");
        }

        public void InitialEmptyGrid()
        {
            _gridController.InitGrid(new CellUnit[_gridSettings.Width, _gridSettings.Height]);
            _gridController.DrawGrid();
            Debug.Log("Empty grid generated");
        }
    }
    
    public interface INewGameInitializer
    {
        void InitializeStrategy();
        void InitialRandomGrid();
        void InitialEmptyGrid();
    }
}