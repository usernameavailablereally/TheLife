using UnityEngine;

namespace GridCore
{
    [CreateAssetMenu(fileName = "GridSettings", menuName = "Settings/GridSettings")]
    public class GridSettingsObject : ScriptableObject
    {
        public int SizeX;
        public int SizeY;
    }
}