using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace GridCore
{
    public static class GridExtensions
    { 
        public static Vector3Int ConvertToGridPosition(this Grid sceneGrid, Camera camera)
        { 
            return sceneGrid.WorldToCell(camera.GetMousePosition());
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

        public static bool HasElement(this GridData gridData, int gridPositionX, int gridPositionY)
        {
            return gridPositionX >= 0 && gridPositionX < gridData.SizeX && gridPositionY >= 0 && gridPositionY < gridData.SizeY;
        }
    }
}