using System;

namespace GridCore
{
    [Serializable]
    public struct CellUnit
    {
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }

        // true = alive, false = dead
        private bool _isAlive;

        public CellUnit(bool isAlive, int positionX = 0, int positionY = 0) : this()
        {
            PositionX = positionX;
            PositionY = positionY;

            SetState(isAlive);
        }

        public bool IsAlive()
        {
            return _isAlive;
        }

        public void SetState(bool isAlive)
        {
            _isAlive = isAlive;
        }

        public void UpdateCellUnit(bool isAlive, int positionX, int positionY)
        {
            _isAlive = isAlive;
            PositionX = positionX;
            PositionY = positionY;
        }
    }
}