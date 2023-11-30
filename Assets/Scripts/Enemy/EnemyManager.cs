using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager
    {
        private readonly BulletSystem _bulletSystem;
        private readonly BulletSettings _bulletSettings;
        private readonly Player _player;
        private readonly EnemyPositions _enemyPositions;
        private readonly EnemyPool _pool;
        private readonly Transform _worldTransform;
        
        public EnemyManager(BulletSystem bulletSystem, Settings settings, Player player, 
            EnemyPool pool, EnemyPositions enemyPositions, Transform worldTransform)
        {
            _bulletSystem = bulletSystem;
            _bulletSettings = settings.BulletSettings;
            _player = player;
            _enemyPositions = enemyPositions;
            _pool = pool;
            _worldTransform = worldTransform;
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
                (position, direction * _bulletSettings.Speed, _bulletSettings.Color, _bulletSettings.PhysicsLayer, _bulletSettings.Damage, false));
        }
        
        [Serializable]
        public class Settings
        {
             public BulletSettings BulletSettings;
        }
    }
}