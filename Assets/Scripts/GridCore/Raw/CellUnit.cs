namespace GridCore.Raw
{
    public class CellUnit
    {
        public int PositionX { get; }
        public int PositionY { get; }
    
        private bool _isAlive;

        public CellUnit(bool isAlive, int positionX = 0, int positionY = 0)
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
    }
}