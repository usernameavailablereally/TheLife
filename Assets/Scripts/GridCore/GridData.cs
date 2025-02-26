using System;

namespace GridCore
{
    // TODO: Don't mix up Scriptable and Serializable terms :D
    [Serializable]
    public struct GridData
    {
        public CellUnit[,] Grid;
        public readonly int SizeX;
        public readonly int SizeY;
        
        public GridData(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            Grid = new CellUnit[sizeX, sizeY]; 
        }
    }
}