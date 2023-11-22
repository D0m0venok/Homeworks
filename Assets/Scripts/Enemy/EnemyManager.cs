using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour, IGameStartListener
    {
        [SerializeField] private GameManager _gameManager;
        [Space]
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

        private EnemyPool _enemyPool;

        public void OnStartGame()
        {
            _enemyPool = new EnemyPool(_prefab, _container, _initialCount, _initialCount, _gameManager);
        }
        
        public bool TrySpawnEnemy()
        {
            if(_initialCount <= _enemyPool.Count)
                return false;
            
            var enemy = _enemyPool.Get(_worldTransform);
            var spawnPosition = _enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;
                    
            var attackPosition = _enemyPositions.RandomAttackPosition();
            enemy.MoveAgent.SetDestination(attackPosition.position);
                    
            enemy.AttackAgent.SetTarget(_player);
                    
            enemy.HitPointsComponent.OnDeath += OnDeath;
            enemy.AttackAgent.OnFired += OnFired;

            return true;
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