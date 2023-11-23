using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour, IGameStartListener
    {
        [SerializeField] private GameManager _gameManager;
        [Space]
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;
        [Space]
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private Player _player;
        [SerializeField] private EnemyPool _pool;

        public void OnStartGame()
        {
            _pool.Construct(_gameManager);
        }
        
        public bool TrySpawnEnemy()
        {
            if(!_pool.HasFreeObject)
                return false;
            
            var enemy = _pool.Get(_worldTransform);
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

                _pool.Put(enemy);
            }
        }
        private void OnFired(Vector2 position, Vector2 direction)
        {
            _bulletSystem.FlyBulletByArgs(new Args
                (position, direction * _bulletConfig.Speed, _bulletConfig.Color, _bulletConfig.Layer, _bulletConfig.Damage, false));
        }
    }
}