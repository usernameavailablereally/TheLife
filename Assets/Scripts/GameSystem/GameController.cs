using Cysharp.Threading.Tasks;
using GridCore.Raw;
using GridCore.Scene;
using LifeStrategies;
using UnityEngine;

namespace GameSystem
{
    public enum LifeTimeStates
    {
        Play,
        Stop
    }

    public class GameController : MonoBehaviour
    {
        [SerializeField] private int _sizeX;
        [SerializeField] private int _sizeY;
        [SerializeField] private GridController _gridController;
        
        private const float Delay = 0.15f;
        private LifeTimeStates _lifeTimeState = LifeTimeStates.Stop;
        private ILifeStrategy _targetStrategy;

        //TODO advanced game entry point 
        private void Awake()
        {
            _targetStrategy = StrategyPicker.GetTargetStrategy();
            _lifeTimeState = LifeTimeStates.Stop;
            _gridController.InitGrid(_sizeX, _sizeY, _targetStrategy);
            _gridController.DrawGrid();
        }
        
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
        // TODO Extract to LifePlayer
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
    }
}