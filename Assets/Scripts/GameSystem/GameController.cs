using System;
using Cysharp.Threading.Tasks;
using GridCore.Raw;
using GridCore.Scene;
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
        private readonly UIManager _uiManager;
        
        private const float Delay = 0.15f;
        private LifeTimeStates _lifeTimeState = LifeTimeStates.Stop;
        private ILifeStrategy _targetStrategy;
        private readonly GridSettingsObject _gridSettings;
        
        public GameController(GridController gridController, ILifeStrategy targetStrategy, GridSettingsObject gridSettings, UIManager uiManager)
        {
            _gridController = gridController;
            _targetStrategy = targetStrategy;
            _gridSettings = gridSettings;
            _uiManager = uiManager;
        } 

        public void Start()
        {
            Debug.Log("GameController.Initialize");

            _targetStrategy = StrategyPicker.GetTargetStrategy();
            _lifeTimeState = LifeTimeStates.Stop;
            _gridController.InitGrid(_gridSettings.SizeX, _gridSettings.SizeY, _targetStrategy);
            _gridController.DrawGrid();
            
            _uiManager.StartButton.onClick.AddListener(StartGame);
            _uiManager.StopButton.onClick.AddListener(StopGame);
        } 
        
        // TODO assign to button in script
        public void StartGame()
        {
            if (_lifeTimeState == LifeTimeStates.Play)
            {
                return;
            }
        
            _lifeTimeState = LifeTimeStates.Play;
        
            LifeTimeLoop().Forget();
        }
        
        public void StopGame()
        {
            _lifeTimeState = LifeTimeStates.Stop;
        } 
        // TODO Extract to LifePlayer (IAsyncStartable)
        // TODO CANCELLATION TOKEN
        private async UniTask LifeTimeLoop()
        {
            _gridController.DrawGrid();
            while (_lifeTimeState == LifeTimeStates.Play)
            {
                Debug.Log("Processing next iteration");
                _gridController.ProcessNextGeneration();
                _gridController.DrawGrid();

                await UniTask.Delay((int)(Delay * 1000));
            }

            _lifeTimeState = LifeTimeStates.Stop;
        }

        public void Dispose()
        { 
            _uiManager.StartButton.onClick.RemoveListener(StartGame);
            _uiManager.StopButton.onClick.RemoveListener(StopGame);
        } 
    }
}