using System;
using System.Collections.Generic;
using GridCore;
using LifeStrategies;
using UnityEngine;
using UnityEngine.Tilemaps;
using VContainer;
using VContainer.Unity;

namespace GameSystem
{
    public sealed class GameLifetimeScope : LifetimeScope, IDisposable
    {
        [SerializeField] private GridSettingsObject _gridSettings;
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private Grid _sceneGrid;
        [SerializeField] private AssetsLoader _assetsLoader;
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private Camera _mainCamera;

        protected override void Configure(IContainerBuilder builder)
        {
            ValidateSerializedFields();

            RegisterCoreComponents(builder);

            RegisterControllers(builder);
            
            RegisterEntryPoints(builder);
        }

        private void ValidateSerializedFields()
        {
            if (_gridSettings == null) throw new MissingReferenceException($"{nameof(_gridSettings)} is not assigned");
            if (_tilemap == null) throw new MissingReferenceException($"{nameof(_tilemap)} is not assigned");
            if (_sceneGrid == null) throw new MissingReferenceException($"{nameof(_sceneGrid)} is not assigned");
            if (_assetsLoader == null) throw new MissingReferenceException($"{nameof(_assetsLoader)} is not assigned");
            if (_uiManager == null) throw new MissingReferenceException($"{nameof(_uiManager)} is not assigned");
            if (_mainCamera == null) throw new MissingReferenceException($"{nameof(_mainCamera)} is not assigned");
        }

        private void RegisterCoreComponents(IContainerBuilder builder)
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
        }

        private void RegisterControllers(IContainerBuilder builder)
        {
            builder.Register<GridController>(Lifetime.Singleton).AsSelf().As<IGridClickHandler>();
            builder.Register<SaveGameController>(Lifetime.Singleton).As<ISaveLoadClickHandler>();
            builder.Register<LifePlayerController>(Lifetime.Singleton).As<IStartStopLifeClickHandler>();
            builder.Register<StrategyContainer>(Lifetime.Singleton).As<IStrategyContainer>();
            builder.Register<InputController>(Lifetime.Singleton).As<ITickable>();
            builder.Register<GenerationProcessor>(Lifetime.Singleton).As<IGenerationProcessor>();
            builder.Register<NewGameInitializer>(Lifetime.Singleton).As<INewGameInitializer>();
        }

        private void RegisterEntryPoints(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameControllerEntryPoint>().As<IRestartClickHandler>();
        }
    }
}