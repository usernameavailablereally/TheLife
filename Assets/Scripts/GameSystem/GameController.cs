using System;
using GridCore;
using LifeStrategies;
using UnityEngine;
using VContainer.Unity;

namespace GameSystem
{
    public enum LifeTimeStates
    {
        Play,
        Stop
    }

    public class GameController : IStartable, IDisposable
    {
        private readonly GridController _gridController;
        private readonly GridSettingsObject _gridSettings;
        private readonly UIManager _uiManager;
        
        public GameController(GridController gridController, GridSettingsObject gridSettings, UIManager uiManager)
        {
            _gridController = gridController;
            _gridSettings = gridSettings;
            _uiManager = uiManager;
        } 

        public void Start()
        {
            // Generating default grid. Playing life is in LifePlayer.cs 
            var targetStrategy = new StrategyPicker().GetTargetStrategy();
            
            _gridController.InitGrid(GridGenerator.CreateRandomGrid(_gridSettings.Height, _gridSettings.Width));
            _gridController.InitStrategy(targetStrategy);
            _gridController.DrawGrid();
            Debug.Log("Random grid generated");
            
            _uiManager.NewGameButton.onClick.AddListener(Restart);
        }

        private void Restart()
        {
            Debug.Log("Empty grid generated");
            _gridController.InitGrid(new CellUnit[_gridSettings.Height, _gridSettings.Width]);
            _gridController.DrawGrid();
        }

        public void Dispose()
        {
            _uiManager.NewGameButton.onClick.RemoveAllListeners();
        }
    }
}