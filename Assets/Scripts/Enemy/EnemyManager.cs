using System;
using UnityEngine;
using VG.Utilites;

namespace ShootEmUp
{
    [InjectTo]
    public sealed class EnemyManager : Listener, IGameStartListener
    {
        [Inject] private readonly PoolFactory<Enemy> _pool;
        [Inject] private readonly BulletSystem _bulletSystem;
        [Inject] private readonly Settings _settings;
        [Inject] private readonly Player _player;
        [Inject] private readonly EnemyPositions _enemyPositions;
        
        private readonly Transform _active;
        private readonly Transform _disable;

        public EnemyManager(Transform active, Transform disable)
        {
            _active = active;
            _disable = disable;
        }
        
        public void OnStartGame()
        {
            _pool.ActiveContainer = _active;
            _pool.DisableContainer = _disable;
        }
        public bool TrySpawnEnemy()
        {
            if(!_pool.TryGet(out var enemy))
                return false;
            
            var spawnPosition = _enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;
                    
            var attackPosition = _enemyPositions.RandomAttackPosition();
            enemy.Get<EnemyMoveAgent>().SetDestination(attackPosition.position);

            enemy.Get<EnemyAttackAgent>().SetTarget(_player);
                    
            enemy.Get<HitPointsComponent>().OnDeath += OnDeath;
            enemy.Get<EnemyAttackAgent>().OnFired += OnFired;

            return true;
        }
        
        private void OnDeath(Unit unit)
        {
            if (unit is Enemy enemy)
            {
                enemy.Get<HitPointsComponent>().OnDeath -= OnDeath;
                enemy.Get<EnemyAttackAgent>().OnFired -= OnFired;

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