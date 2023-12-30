using System;
using UnityEngine;
using VG.Utilites;

namespace ShootEmUp
{
    [InjectTo]
    public sealed class EnemyManager
    {
        [Inject] private readonly PoolFactory<Enemy> _pool;
        [Inject] private readonly BulletSystem _bulletSystem;
        [Inject] private readonly Settings _settings;
        [Inject] private readonly Player _player;
        [Inject] private readonly EnemyPositions _enemyPositions;
        
        public bool TrySpawnEnemy()
        {
            if(!_pool.TryGet(out var enemy))
                return false;
            
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
            var bulletSettings = _settings.BulletSettings;
            _bulletSystem.FlyBulletByArgs(new Args
                (position, direction * bulletSettings.Speed, bulletSettings.Color, bulletSettings.PhysicsLayer, bulletSettings.Damage, false));
        }
        
        [Serializable]
        public class Settings
        {
             public BulletSettings BulletSettings;
        }
    }
}