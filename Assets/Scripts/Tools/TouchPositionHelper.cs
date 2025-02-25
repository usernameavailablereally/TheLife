using UnityEngine;

namespace Tools
{
    public static class TouchPositionHelper
    { 
        public static Vector3 GetMousePosition(this Camera camera)
        {
            return camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -camera.transform.position.z));
        }
    }
}