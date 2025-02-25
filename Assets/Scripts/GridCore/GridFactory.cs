namespace GridCore.Raw
{
    public static class GridFactory
    {
        // TODO REMOVE STATIC
        public static CellUnit[,] CreateGrid(int sizeX, int sizeY)
        {
            var grid = new CellUnit[sizeX, sizeY];

            for (var x = 0; x < sizeX; x++)
            {
                for (var y = 0; y < sizeY; y++)
                {
                    grid[x, y] = new CellUnit(RandomiseCellState(), x, y);
                }
            }

            return grid;
        }

        private static bool RandomiseCellState()
        {
            var random = UnityEngine.Random.Range(0, 20);
            return random == 0;
        }
    }
}