using System.Linq;
using GridCore;

namespace LifeStrategies
{
    public class DefaultStrategy : ILifeStrategy
    {
        public void ProcessNextGeneration(ref GridData gridData)
        { 
            var sizeX = gridData.SizeX;
            var sizeY = gridData.SizeY;
            
            var newGrid = new CellUnit[sizeX, sizeY];
    
            for (var x = 0; x < sizeX; x++)
            {
                for (var y = 0; y < sizeY; y++)
                { 
                    var neighbours = gridData.CalculateNeighbours(gridData.Grid[x, y]);
    
                    var neighboursAlive = neighbours.Count(neighbour => neighbour.IsAlive());

                    if (gridData.Grid[x, y].IsAlive())
                    {
                        // Conway's s23 rule (keep alive if 2 or 3 neighbours are alive)
                        newGrid[x, y].UpdateCellUnit(neighboursAlive is 2 or 3, x, y);
                    }
                    else
                    {
                        // Conway's s3 rule (revive if 3 neighbours are alive)
                        newGrid[x, y].UpdateCellUnit(neighboursAlive == 3, x, y);
                    }
                }
            }
            
            gridData.Grid = newGrid;
        }
    }
}