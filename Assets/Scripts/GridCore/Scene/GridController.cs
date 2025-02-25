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
        [SerializeField] private int _sizeX;
        [SerializeField] private int _sizeY;

        private CellUnit[,] _grid;

        private Vector3Int _gridPosition;

        private void Awake()
        {
            InitGrid();
            DrawGrid();
        }

        public void InitGrid()
        {
            _grid = GridFactory.CreateGrid(_sizeX, _sizeY); 
        }
    
        public void DrawGrid()
        {
            for (var i = 0; i < _sizeY; i++)
            {
                for (var j = 0; j < _sizeX; j++)
                {
                    var p = new Vector3Int(i, j, 0);
                    _tilemap.SetTile(p, _grid[j, i].IsAlive() ? _aliveTile : _deadTile);
                }
            }
        }
    
        // TODO fix CAMERA getter
        public void SetAlive()
        {
            _gridPosition = GridHelper.ConvertToGridPosition(_sceneGrid, TouchPositionHelper.GetMousePosition(Camera.main));
            _grid[_gridPosition.y, _gridPosition.x].SetState(true); 
            DrawGrid();
        }

        public void SetDead()
        {
            _gridPosition = GridHelper.ConvertToGridPosition(_sceneGrid, TouchPositionHelper.GetMousePosition(Camera.main));
            _grid[_gridPosition.y, _gridPosition.x].SetState(false);
        } 

    }
}