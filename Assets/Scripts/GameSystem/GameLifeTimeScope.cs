using GridCore.Raw;
using GridCore.Scene;
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
        
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_gridSettings);
            
            builder.RegisterComponent(_tilemap);
            builder.RegisterComponent(_sceneGrid); 
            builder.RegisterComponent(_assetsLoader);
            builder.RegisterComponent(_uiManager);
            
            builder.Register<ILifeStrategy, DefaultStrategy>(Lifetime.Singleton);
            builder.Register<GridController>(Lifetime.Singleton);
            builder.RegisterEntryPoint<GameController>();
            builder.Register<InputController>(Lifetime.Singleton).As<ITickable>();
        }
    }
}