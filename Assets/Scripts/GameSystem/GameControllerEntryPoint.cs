using System;
using GridCore;
using LifeStrategies;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameSystem
{
    // Generates initial grid. Playing life generations is in LifePlayer.cs 
    public class GameControllerEntryPoint : IStartable, IRestartClickHandler
    {
        private readonly INewGameInitializer _gameInitializer;
        
        [Inject]
        public GameControllerEntryPoint(INewGameInitializer gameInitializer)
        {
            _gameInitializer = gameInitializer;
        } 

        public void Start()
        {
            _gameInitializer.InitializeStrategy();
            _gameInitializer.InitialRandomGrid();
       
            Debug.Log("Random grid generated");
        }

        public void OnRestartClicked()
        {
            Restart();
        }

        private void Restart()
        {
            _gameInitializer.InitialEmptyGrid();
        }
    }
}