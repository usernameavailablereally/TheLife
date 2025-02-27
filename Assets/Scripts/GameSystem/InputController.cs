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
        private readonly IGridClickHandler _gridClickHandler; 
        private bool IsLeftClick() => Input.GetMouseButton(0);
        private bool IsRightClick() => Input.GetMouseButton(1);

        [Inject] // explicit constructor injection
        public InputController(GridController gridController, IGridClickHandler gridClickHandler)
        {
            _gridController = gridController;
            _gridClickHandler = gridClickHandler;
        }

        public void Tick()
        {
            CheckForMouseInput();
        }

        private void CheckForMouseInput()
        {
            if (IsLeftClick())
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }

                _gridController.OnGridLeftClick();
            }

            if (IsRightClick())
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }

                _gridClickHandler.OnGridRightClick();
            }
        }
    }
}