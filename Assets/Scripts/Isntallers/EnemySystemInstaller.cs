using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class EnemySystemInstaller : MonoInstaller
    {
        [SerializeField] private Transform _enemiesPoolContainer;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private Transform[] _spawnPositions;
        [SerializeField] private Transform[] _attackPositions;

        public override void InstallBindings()
        {
            Container.Bind<EnemyPool>().To<EnemyPool>().AsSingle().WithArguments(_enemiesPoolContainer);
            Container.Bind<EnemyManager>().AsSingle().WithArguments(_worldTransform);
            Container.BindInterfacesAndSelfTo<EnemyPeriodSpawner>().AsCached().NonLazy();
            Container.Bind<EnemyPositions>().AsSingle().WithArguments(_spawnPositions, _attackPositions);
            //Container.Bind<Enemy>().AsSingle();
        }
    }
}