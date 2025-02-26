using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GridCore;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameSystem
{
    public class LifePlayerController : IStartable, IDisposable
    { 
        private const int Delay = 150;
        private readonly GridController _gridController; 
        private LifeTimeStates _lifeTimeState;
        private readonly UIManager _uiManager; 
        private CancellationTokenSource _cancellationTokenSource;

        [Inject] // explicit constructor injection
        public LifePlayerController(GridController gridController, UIManager uiManager)
        {
            _gridController = gridController; 
            _lifeTimeState = LifeTimeStates.Stop;
            _uiManager = uiManager;
        }

        public void Start()
        {
            _uiManager.StartButton.onClick.AddListener(StartLife);
            _uiManager.StopButton.onClick.AddListener(StopLife);
        }

        private void StartLife()
        {
            if (_lifeTimeState == LifeTimeStates.Play)
            {
                Debug.Log("Double click forbidden");
                return;
            }

            _lifeTimeState = LifeTimeStates.Play;
            _cancellationTokenSource = new CancellationTokenSource();
            StartLifeTask(_cancellationTokenSource.Token).Forget();
        }

        private async UniTask StartLifeTask(CancellationToken cancellation)
        {
            _gridController.DrawGrid();
            while (_lifeTimeState == LifeTimeStates.Play)
            {
                if (cancellation.IsCancellationRequested)
                {
                    break;
                }

                _gridController.ProcessNextGeneration();
                _gridController.DrawGrid();

                await UniTask.Delay(Delay, cancellationToken: cancellation);
            }

            _lifeTimeState = LifeTimeStates.Stop;
        }

        private void StopLife()
        {
            _lifeTimeState = LifeTimeStates.Stop;
            _cancellationTokenSource?.Cancel();
        }

        public void Dispose()
        {
            StopLife();
            _uiManager.StartButton.onClick.RemoveListener(StartLife);
            _uiManager.StopButton.onClick.RemoveListener(StopLife);
        }
    }
}