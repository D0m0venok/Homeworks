using UnityEngine;

namespace ShootEmUp
{
    public class BootstrapInstaller
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private Player _player;
        [SerializeField] private Transform _levelBackground;
        [SerializeField] private LevelBackground.Params _levelBackgroundParameters;
        [SerializeField] private Transform[] _levelBounds;

        public void InstallBindings()
        {
            // Container.Bind<GameManager>().FromInstance(_gameManager).AsSingle();
            // Container.Bind<Player>().FromInstance(_player).AsSingle();
            // Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle();
            // Container.BindInterfacesTo<InputSystemManager>().AsSingle();
            // Container.BindInterfacesTo<LevelBackground>().AsSingle().WithArguments(_levelBackground, _levelBackgroundParameters);
            // Container.Bind<LevelBounds>().AsSingle().WithArguments(_levelBounds);
        }
    }
}
