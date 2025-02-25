using System.Collections.Generic;
using GridCore.Raw;
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
        public static List<CellUnit> CalculateNeighbours(this GridData gridData, CellUnit cellUnit)
        {
            var neighbours = new List<CellUnit>();

            for (var x = -1; x <= 1; x++)
            {
                for (var y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }

                    var nextX = cellUnit.PositionX + x;
                    var nextY = cellUnit.PositionY + y;

                    if (nextX >= 0 && nextX < gridData.SizeX && nextY >= 0 && nextY < gridData.SizeY)
                    {
                        neighbours.Add(gridData.Grid[nextX, nextY]);
                    }
                }
            }

            return neighbours;
        }
    }
}