using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;
        [Space]
        [SerializeField] private int _initialCount = 7;
        [SerializeField] private Enemy _prefab;
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _worldTransform;
        [Space]
        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private Player _player;
        [SerializeField] private float _delayBetweenSpawnsTime = 1f;

        private EnemyPool _enemyPool;
        private float _lastSpawnTime;

        private void Awake()
        {
            _enemyPool = new EnemyPool(_prefab, _container, _initialCount, _initialCount);
        }
        private void Update()
        {
            if (Time.realtimeSinceStartup - _lastSpawnTime <= _delayBetweenSpawnsTime) 
                return;
            
            if (!_enemyPool.TryGet(out var enemy, _worldTransform))
                return;
                
            SetEnemy(enemy);
        }

        private void SetEnemy(Enemy enemy)
        {
            var spawnPosition = _enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;
                    
            var attackPosition = _enemyPositions.RandomAttackPosition();
            enemy.MoveAgent.SetDestination(attackPosition.position);
                    
            enemy.AttackAgent.SetTarget(_player);
                    
            enemy.HitPointsComponent.OnDeath += OnDeath;
            enemy.AttackAgent.OnFired += OnFired;
                
            _lastSpawnTime = Time.realtimeSinceStartup;
        }
        private void OnDeath(Unit unit)
        {
            if (unit is Enemy enemy)
            {   
                enemy.HitPointsComponent.OnDeath -= OnDeath;
                enemy.AttackAgent.OnFired -= OnFired;

                _enemyPool.Put(enemy);
            }
        }
        private void OnFired(Vector2 position, Vector2 direction)
        {
            _bulletSystem.FlyBulletByArgs(new Args
                (position, direction * _bulletConfig.Speed, _bulletConfig.Color, _bulletConfig.Layer, _bulletConfig.Damage, false));
        }
    }
}