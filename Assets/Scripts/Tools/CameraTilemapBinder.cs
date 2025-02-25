using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tools
{
    public class CameraTilemapBinder : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Tilemap _tilemap;  
        [SerializeField] private  float _padding = 1f;  

        private void Start()
        {
            if (_tilemap == null) return;

            BoundsInt bounds = _tilemap.cellBounds;
            Vector3Int min = bounds.min;
            Vector3Int max = bounds.max;

            float gridWidth = (max.x - min.x);
            float gridHeight = (max.y - min.y); 
        
            if (_camera == null) return;
 
            _camera.orthographicSize = (gridHeight / 2f) + _padding;
 
            float aspectRatio = _camera.aspect;
            float camWidth = _camera.orthographicSize * aspectRatio;
 
            if (gridWidth > camWidth * 2)
            {
                _camera.orthographicSize = (gridWidth / (2 * aspectRatio)) + _padding;
            }
 
            Vector3 center = _tilemap.cellBounds.center;
            _camera.transform.position = new Vector3(center.x, center.y, _camera.transform.position.z);
        }
    }
}