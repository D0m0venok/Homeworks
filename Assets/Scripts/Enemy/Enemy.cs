using System;
using VG.Utilites;

namespace ShootEmUp
{
    [InstallMono(InstallType.PoolFactory, 0, 7), InjectTo]
    public sealed class Enemy : Unit
    {
        private float _positionInaccuracy;
        private float _shootDelay;

        [Inject]
        public void Construct(EnemySettings enemySettings)
        {
            _isPlayer = enemySettings.IsPlayer;
            _speed = enemySettings.Speed;
            _hitPoint = enemySettings.HitPoint;
            _positionInaccuracy = enemySettings.PositionInaccuracy;
            _shootDelay = enemySettings.ShootDelay;
            
            Add(new EnemyMoveAgent(_positionInaccuracy));
            Add(new EnemyAttackAgent(_shootDelay));
            
            base.Construct();
        }
        
        [Serializable]
        public class EnemySettings : UnitSettings
        {
            public float PositionInaccuracy = 0.25f;
            public float ShootDelay = 1;
        }
    }
}