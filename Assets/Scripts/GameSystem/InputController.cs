using GridCore;
using GridCore.Scene;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystem
{
    public class InputController : MonoBehaviour
    {
        // TODO INJECT PROPERLY
        [SerializeField] GridController _gridController;
        //[SerializeField] GameController _gameController;

        private void Update()
        {
            // CheckForKeyboardInputs();

            // if (_gameController.LifeTimeState == LifeTimeStates.Play)
            // {
            //     return;
            // }

            CheckForMouseInput();
        }


        private void CheckForMouseInput()
        { 
            if (Input.GetMouseButton(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }

                _gridController.SetAlive();
            }
 
            if (!Input.GetMouseButton(1)) return;
            
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            _gridController.SetDead();
            _gridController.DrawGrid();
        }

        // private void CheckForKeyboardInputs()
        // {
        //     if (Input.GetKeyDown(KeyCode.Space))
        //     {
        //         if (_gameController.LifeTimeState == LifeTimeStates.Play)
        //         {
        //             _gameController.StopGame();
        //         }
        //         else
        //         {
        //             _gameController.StartGame();
        //         }
        //     }
        // }
    }
}