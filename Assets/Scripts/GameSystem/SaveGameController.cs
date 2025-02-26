using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GridCore;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameSystem
{
    public class SaveGameController : IInitializable, IDisposable
    {
        private const string FileName = "GAMESAVE";
        private readonly GridController _gridController;
        private readonly UIManager _uiManager;

        [Inject] // explicit constructor injection
        public SaveGameController(GridController gridController, UIManager uiManager)
        {
            _gridController = gridController;
            _uiManager = uiManager;
        }

        public void Initialize()
        {
            _uiManager.SaveButton.onClick.AddListener(SaveGame);
            _uiManager.LoadButton.onClick.AddListener(LoadGame);
        }

        private void SaveGame()
        {
            var filePath = Path.Combine(Application.persistentDataPath, FileName);
            var gridData = _gridController.GetGridData();
            var formatter = new BinaryFormatter();
            using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, gridData);
            Debug.LogFormat("Grid saved to file: {0}", filePath);
        }

        private void LoadGame()
        {
            var filePath = Path.Combine(Application.persistentDataPath, FileName);
            if (!File.Exists(filePath))
            {
                Debug.LogError("Save file not found");
                return;
            }

            var formatter = new BinaryFormatter();
            using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var gridData = (GridData)formatter.Deserialize(stream);
            _gridController.InitGrid(gridData);
            _gridController.DrawGrid();
            Debug.LogFormat("Grid loaded from file: {0}", filePath);
        }

        public void Dispose()
        {
            _uiManager.SaveButton.onClick.RemoveAllListeners();
            _uiManager.LoadButton.onClick.RemoveAllListeners();
        }
    }
}