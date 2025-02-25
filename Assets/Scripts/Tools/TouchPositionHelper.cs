using UnityEngine;

namespace Tools
{
    public static class TouchPositionHelper
    { 
        // TODO MAKE EXTENSION METHODS
        public static Vector3 GetMousePosition(Camera camera)
        {
            return camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -camera.transform.position.z));
        }
    }
}