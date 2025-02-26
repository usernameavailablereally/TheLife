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

            // Hierarchy references
            builder.RegisterComponent(_mainCamera);
            builder.RegisterComponent(_tilemap);
            builder.RegisterComponent(_sceneGrid);
            builder.RegisterComponent(_assetsLoader);
            builder.RegisterComponent(_uiManager);

            // Non-mono classes
            // here could be a StrategyPicker as MonoBehaviour with different strategies as ScriptableObjects, for switching strategies in runtime
            builder.Register<GridController>(Lifetime.Singleton);
            builder.Register<SaveGameController>(Lifetime.Singleton).As<IInitializable>().As<IDisposable>(); 
            builder.Register<LifePlayerController>(Lifetime.Singleton).As<IStartable>().As<IDisposable>();
            builder.RegisterEntryPoint<GameController>();
            builder.Register<InputController>(Lifetime.Singleton).As<ITickable>();
        }
    }
}