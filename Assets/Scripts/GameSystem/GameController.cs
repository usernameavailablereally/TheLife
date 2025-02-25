using GridCore;
using LifeStrategies;
using VContainer.Unity;

namespace GameSystem
{
    public enum LifeTimeStates
    {
        Play,
        Stop
    }

    public class GameController : IStartable
    {
        private readonly GridController _gridController;
        private readonly GridSettingsObject _gridSettings;
        
        public GameController(GridController gridController, GridSettingsObject gridSettings, UIManager uiManager)
        {
            _gridController = gridController;
            _gridSettings = gridSettings;
        } 

        public void Start()
        {
            // Generating default grid. Playing life is in LifePlayer.cs 
            var targetStrategy = new StrategyPicker().GetTargetStrategy();
            
            _gridController.InitGrid(_gridSettings.SizeX, _gridSettings.SizeY, targetStrategy);
            _gridController.DrawGrid();
        }
    }
}