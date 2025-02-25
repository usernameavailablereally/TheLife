using GridCore.Scene;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystem
{
    public class InputController : MonoBehaviour
    {
        // TODO INJECT PROPERLY
        [SerializeField] GridController _gridController;

        private void Update()
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