using UnityEngine;
using VG.Utilites;

namespace ShootEmUp
{
    [InjectTo]
    public sealed class EnemyPeriodSpawner : IUpdate
    {
        [Inject] private readonly EnemyManager _enemyManager;
        private readonly float _delayBetweenSpawnsTime = 1f;
        private float _lastSpawnTime;

        public EnemyPeriodSpawner()
        {
            ListenersManager.Add(this);
        }
        
        void IUpdate.OnEntityUpdate()
        {
            if (Time.realtimeSinceStartup - _lastSpawnTime <= _delayBetweenSpawnsTime) 
                return;
            
            if(_enemyManager.TrySpawnEnemy())
                _lastSpawnTime = Time.realtimeSinceStartup;
        }
    }
}