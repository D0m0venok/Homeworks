using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPeriodSpawner : MonoBehaviour, IGameUpdateListener
    {
        [SerializeField] private float _delayBetweenSpawnsTime = 1f;
        [SerializeField] private EnemyManager _enemyManager;
        
        private float _lastSpawnTime;
        
        public void OnUpdate(float deltaTime)
        {
            if (Time.realtimeSinceStartup - _lastSpawnTime <= _delayBetweenSpawnsTime) 
                return;
            
            if(!_enemyManager.HasFreeEnemy)
                return;

            _enemyManager.SpawnEnemy();
            
            _lastSpawnTime = Time.realtimeSinceStartup;
        }
    }
}