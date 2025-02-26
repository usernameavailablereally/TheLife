using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GridCore;
using UnityEngine;
using VContainer;

namespace GameSystem
{
    public enum LifePlayerStates
    {
        Play,
        Stop
    }
    public class LifeClickPlayerController : IDisposable, IStartStopLifeClickHandler
    { 
        private const int Delay = 150;
        private readonly GridController _gridController; 
        private LifePlayerStates _lifePlayerState;
        private CancellationTokenSource _cancellationTokenSource;

        [Inject] // explicit constructor injection
        public LifeClickPlayerController(GridController gridController)
        {
            _gridController = gridController; 
            _lifePlayerState = LifePlayerStates.Stop;
        } 

        public void OnStartLifeClicked()
        {
           StartLife();
        }

        public void OnStopLifeClicked()
        {
            StopLifeTask();
        }

        private void StartLife()
        {
            if (_lifePlayerState == LifePlayerStates.Play)
            {
                Debug.Log("Double click forbidden");
                return;
            }

            _lifePlayerState = LifePlayerStates.Play;
            _cancellationTokenSource = new CancellationTokenSource();
            StartLifeTask(_cancellationTokenSource.Token).Forget();
        }

        private async UniTask StartLifeTask(CancellationToken cancellation)
        {
            // as we expect Strategies (ProcessNextGeneration) may vary, not the GridControllers, we can assume DrawGrid is safe
            // discussable, but all the DrawGrid calls might also be caught potentially
            
            _gridController.DrawGrid();
            try
            {
                while (_lifePlayerState == LifePlayerStates.Play)
                {
                    if (cancellation.IsCancellationRequested)
                    {
                        break;
                    }

                    try
                    {
                        _gridController.ProcessNextGeneration();
                        _gridController.DrawGrid();
                        await UniTask.Delay(Delay, cancellationToken: cancellation);
                    }
                    catch (Exception ex) when (ex is not OperationCanceledException)
                    {
                        Debug.LogError($"Exception during StartLifeTask : {ex.Message}");
                        StopLifeTask();
                        break;
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("LifeTask was canceled");
            }
            finally
            {
                _lifePlayerState = LifePlayerStates.Stop;
            }
        }

        private void StopLifeTask()
        {
            _lifePlayerState = LifePlayerStates.Stop;
            _cancellationTokenSource?.Cancel();
        }

        public void Dispose()
        {
            StopLifeTask();
        }
    }
}