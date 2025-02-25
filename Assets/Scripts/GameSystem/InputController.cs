using GridCore;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using VContainer.Unity;

namespace GameSystem
{
    public class InputController : ITickable
    {
        private readonly GridController _gridController;
        
        [Inject]  // explicit constructor injection
        public InputController(GridController gridController)
        {
            _gridController = gridController;
        }
        public void Tick()
        {
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
    }
}