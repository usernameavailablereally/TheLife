using GridCore.Raw;
using Tools;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GridCore.Scene
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private Grid _sceneGrid; 
        [SerializeField] private Tile _aliveTile;
        [SerializeField] private Tile _deadTile;

        private GridData _gridData; 

        private Vector3Int _gridPosition;
        ILifeStrategy _currentLifeStrategy;
 

        public void InitGrid(int sizeX, int sizeY, ILifeStrategy targetStrategy)
        {
            _currentLifeStrategy = targetStrategy;
            _gridData = new GridData(sizeX, sizeY)
            {
                Grid = GridFactory.CreateGrid(sizeX, sizeY)
            };
        }
    
        public void DrawGrid()
        {
            for (var i = 0; i < _gridData.SizeY; i++)
            {
                for (var j = 0; j < _gridData.SizeX; j++)
                {
                    var p = new Vector3Int(i, j, 0);
                    _tilemap.SetTile(p, _gridData.Grid[j, i].IsAlive() ? _aliveTile : _deadTile);
                }
            }
        }

        // TODO fix CAMERA getter
        public void SetAlive()
        {
            _gridPosition = GridHelper.ConvertToGridPosition(_sceneGrid, TouchPositionHelper.GetMousePosition(Camera.main));
            _gridData.Grid[_gridPosition.y, _gridPosition.x].SetState(true); 
            DrawGrid();
        }

        public void SetDead()
        {
            _gridPosition = GridHelper.ConvertToGridPosition(_sceneGrid, TouchPositionHelper.GetMousePosition(Camera.main));
            _gridData.Grid[_gridPosition.y, _gridPosition.x].SetState(false);
        }

        public void ProcessNextGeneration()
        {
            _currentLifeStrategy.ProcessNextGeneration(ref _gridData);
        }
    }
}