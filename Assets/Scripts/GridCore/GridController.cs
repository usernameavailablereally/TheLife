using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using VContainer;

namespace GridCore
{
    public class GridController : IGridClickHandler
    {
        private readonly Tilemap _tilemap;
        private readonly Grid _sceneGrid;
        private readonly Tile _aliveTile;
        private readonly Tile _deadTile;
        private readonly Camera _mainCamera;

        private GridData _gridData;
        private Vector3Int _gridPosition;

        [Inject]
        public GridController(Tilemap tilemap, Grid sceneGrid, AssetsLoader assetsLoader, Camera mainCamera)
        {
            _tilemap = tilemap ?? throw new ArgumentNullException(nameof(tilemap));
            _sceneGrid = sceneGrid ?? throw new ArgumentNullException(nameof(sceneGrid));
            _mainCamera = mainCamera ?? throw new ArgumentNullException(nameof(mainCamera));
        
            if (assetsLoader == null) throw new ArgumentNullException(nameof(assetsLoader));
            _aliveTile = assetsLoader.AliveTile ?? throw new ArgumentNullException(nameof(assetsLoader.AliveTile));
            _deadTile = assetsLoader.DeadTile ?? throw new ArgumentNullException(nameof(assetsLoader.DeadTile));
        }

        public void InitGrid(CellUnit[,] grid)
        {
            if (grid == null) throw new ArgumentNullException(nameof(grid));
        
            var sizeX = grid.GetLength(0);
            var sizeY = grid.GetLength(1);
            _gridData = new GridData(sizeX, sizeY)
            {
                Grid = grid
            };
        }

        public void InitGrid(GridData gridData)
        {
            _gridData = gridData;
        }

        public GridData GetGridData()
        {
            return _gridData;
        }

        public void DrawGrid()
        {
            if (_gridData.Grid == null) throw new InvalidOperationException("Grid is not initialized");
    
            var p = new Vector3Int();
            for (var i = 0; i < _gridData.SizeX; i++)
            {
                for (var j = 0; j < _gridData.SizeY; j++)
                {
                    p.Set(i, j, 0);
                    _tilemap.SetTile(p, _gridData.Grid[i, j].IsAlive() ? _aliveTile : _deadTile);
                }
            }
        }

        public void OnGridLeftClick()
        {
            SetCellState(true);
            DrawGrid();
        }

        public void OnGridRightClick()
        {
            SetCellState(false);
            DrawGrid();
        }

        private void SetCellState(bool isAlive)
        {
            if (_gridData.Grid == null) throw new InvalidOperationException("GridData is not initialized");
        
            _gridPosition = _sceneGrid.ConvertToGridPosition(_mainCamera);
            if (!_gridData.HasElement(_gridPosition.x, _gridPosition.y)) return;

            _gridData.Grid[_gridPosition.x, _gridPosition.y].SetState(isAlive);
        }
    }
}