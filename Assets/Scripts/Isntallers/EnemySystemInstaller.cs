using UnityEngine;
using VG.Utilites;

namespace ShootEmUp
{
    public class EnemySystemInstaller : MonoInstaller
    {
        [SerializeField] private Transform _enemiesPoolContainer;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private Transform[] _spawnPositions;
        [SerializeField] private Transform[] _attackPositions;
        
        public override void Install(DIContainer container)
        {
            container.Install(new EnemyManager(_worldTransform, _enemiesPoolContainer));
            container.Install<EnemyPeriodSpawner>();
            container.Install(new EnemyPositions(_spawnPositions, _attackPositions));
        }
    }
}