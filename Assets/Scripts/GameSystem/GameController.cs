using GridCore;
using LifeStrategies;
using UnityEngine;
using VContainer.Unity;

namespace GameSystem
{
    // Generates initial grid. Playing life generations is in LifePlayer.cs 
    public class GameController : IStartable, IRestartClickHandler
    {
        private readonly GridController _gridController;
        private readonly GridSettingsObject _gridSettings;
        
        public GameController(GridController gridController, GridSettingsObject gridSettings)
        {
            _gridController = gridController;
            _gridSettings = gridSettings;
        } 

        public void Start()
        {
            var targetStrategy = new StrategyPicker().GetTargetStrategy();
            
            // Generating random grid
            _gridController.InitGrid(GridGenerator.CreateRandomGrid(_gridSettings.Width, _gridSettings.Height));
            _gridController.InitStrategy(targetStrategy);
            _gridController.DrawGrid();
            Debug.Log("Random grid generated");
        }

        public void OnRestartClicked()
        {
            Restart();
        }

        private void Restart()
        {
            Debug.Log("Empty grid generated");
            // Generating empty gri
            _gridController.InitGrid(new CellUnit[_gridSettings.Width, _gridSettings.Height]);
            _gridController.DrawGrid();
        }
    }
}