using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private Player _player;
        [SerializeField] private Transform _levelBackgrount;
        [SerializeField] private LevelBackground.Params _levelBackgrountParametrs;
        [SerializeField] private Transform[] _levelBounds;

        public override void InstallBindings()
        {
            Container.Bind<GameManager>().FromInstance(_gameManager).AsSingle();
            Container.Bind<Player>().FromInstance(_player).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle();
            Container.BindInterfacesTo<InputSystemManager>().AsSingle();
            Container.BindInterfacesTo<LevelBackground>().AsSingle().WithArguments(_levelBackgrount, _levelBackgrountParametrs);
            Container.Bind<LevelBounds>().AsSingle().WithArguments(_levelBounds);
        }
    }
}
