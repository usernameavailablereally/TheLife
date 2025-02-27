using System;
using GridCore;
using UnityEngine;
using UnityEngine.Tilemaps;
using VContainer;
using VContainer.Unity;

namespace GameSystem
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private GridSettingsObject _gridSettings;
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private Grid _sceneGrid;
        [SerializeField] private AssetsLoader _assetsLoader;
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private Camera _mainCamera;

        protected override void Configure(IContainerBuilder builder)
        {
            // ScriptableObject asset instance
            builder.RegisterInstance(_gridSettings);

            // Scene references
            builder.RegisterComponent(_mainCamera);
            builder.RegisterComponent(_tilemap);
            builder.RegisterComponent(_sceneGrid);
            builder.RegisterComponent(_assetsLoader);
            builder.RegisterComponent(_uiManager);
            // here could be a StrategyPicker as MonoBehaviour with different strategies as ScriptableObjects, for switching strategies in runtime by Designer

            // Non-mono classes
            builder.Register<GridController>(Lifetime.Singleton).AsSelf().As<IGridClickHandler>();
            builder.Register<SaveGameController>(Lifetime.Singleton).As<ISaveLoadClickHandler>(); 
            builder.Register<LifeClickPlayerController>(Lifetime.Singleton).As<IDisposable>().As<IStartStopLifeClickHandler>();
            builder.RegisterEntryPoint<GameController>().As<IRestartClickHandler>();
            builder.Register<InputController>(Lifetime.Singleton).As<ITickable>();
        }
    }
}