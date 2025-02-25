using LifeStrategies;
using UnityEngine;
using UnityEngine.Tilemaps;
using VContainer;

namespace GridCore
{
    public class GridController
    {
        private readonly Tilemap _tilemap;
        private readonly Grid _sceneGrid;
        private readonly Tile _aliveTile;
        private readonly Tile _deadTile;
        private readonly Camera _mainCamera;

        private GridData _gridData;
        private Vector3Int _gridPosition;
        private ILifeStrategy _currentLifeStrategy;

        [Inject] // explicit constructor injection
        public GridController(Tilemap tilemap, Grid sceneGrid, AssetsLoader assetsLoader, Camera mainCamera)
        {
            _tilemap = tilemap;
            _sceneGrid = sceneGrid;
            _aliveTile = assetsLoader.AliveTile;
            _deadTile = assetsLoader.DeadTile;
            _mainCamera = mainCamera;
        }

        public void InitGrid(int sizeX, int sizeY, ILifeStrategy targetStrategy)
        {
            _currentLifeStrategy = targetStrategy;
            _gridData = new GridData(sizeX, sizeY)
            {
                Grid = GridGenerator.CreateGrid(sizeX, sizeY)
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

        public void SetAlive()
        {
            _gridPosition = _sceneGrid.ConvertToGridPosition(_mainCamera);
            _gridData.Grid[_gridPosition.y, _gridPosition.x].SetState(true);
            DrawGrid();
        }

        public void SetDead()
        {
            _gridPosition = _sceneGrid.ConvertToGridPosition(_mainCamera);
            _gridData.Grid[_gridPosition.y, _gridPosition.x].SetState(false);
        }

        public void ProcessNextGeneration()
        {
            _currentLifeStrategy.ProcessNextGeneration(ref _gridData);
        }
    }
}