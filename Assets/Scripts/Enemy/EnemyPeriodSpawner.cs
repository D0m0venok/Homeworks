using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPeriodSpawner : IGameUpdateListener
    {
        private readonly float _delayBetweenSpawnsTime = 1f;
        private readonly EnemyManager _enemyManager;
        private float _lastSpawnTime;
        
        public EnemyPeriodSpawner(EnemyManager enemyManager)
        {
            _enemyManager = enemyManager;
        }
        public void OnUpdate(float deltaTime)
        {
            if (Time.realtimeSinceStartup - _lastSpawnTime <= _delayBetweenSpawnsTime) 
                return;
            
            if(_enemyManager.TrySpawnEnemy())
                _lastSpawnTime = Time.realtimeSinceStartup;
        }
    }
}