using UnityEngine;

namespace GridCore
{
    public static class GridHelper
    { 
        // TODO: MAKE EXTENSION METHODS
        public static Vector3Int ConvertToGridPosition(Grid sceneGrid, Vector3 position)
        {
            return sceneGrid.WorldToCell(position);
        }
    }
}