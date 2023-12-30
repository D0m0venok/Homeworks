using System;
using System.Collections.Generic;
using VG.Utilites;

namespace ShootEmUp
{
    [InstallMono(InstallType.PoolFactory, 7, 7)]
    public sealed class Enemy : Unit
    {
        [Inject] private EnemySettings _enemySettings;
        private float _positionInaccuracy;
        private float _shootDelay;

        public EnemyAttackAgent AttackAgent { get; private set; }
        public EnemyMoveAgent MoveAgent { get; private set; }

        public override void OnEntityAwake()
        {
            _isPlayer = _enemySettings.IsPlayer;
            _speed = _enemySettings.Speed;
            _hitPoint = _enemySettings.HitPoint;
            _positionInaccuracy = _enemySettings.PositionInaccuracy;
            _shootDelay = _enemySettings.ShootDelay;

            MoveAgent = new EnemyMoveAgent(this, _positionInaccuracy);
            AttackAgent = new EnemyAttackAgent(this, _shootDelay);
            
            base.OnEntityAwake();
        }
        
        [Serializable]
        public class EnemySettings : UnitSettings
        {
            public float PositionInaccuracy = 0.25f;
            public float ShootDelay = 1;
        }
    }
}