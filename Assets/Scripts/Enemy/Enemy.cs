using System;
using System.Collections.Generic;
using Zenject;

namespace ShootEmUp
{
    public sealed class Enemy : Unit, IGameListenerProvider
    {
        private float _positionInaccuracy;
        private float _shootDelay;
        
        private EnemyAttackAgent _attackAgent;
        private EnemyMoveAgent _moveAgent;

        public EnemyMoveAgent MoveAgent => _moveAgent;
        public EnemyAttackAgent AttackAgent => _attackAgent;
        
        [Inject]
        public void Construct(EnemySettings enemySettings)
        {
            _isPlayer = enemySettings.IsPlayer;
            _speed = enemySettings.Speed;
            _hitPoint = enemySettings.HitPoint;
            _positionInaccuracy = enemySettings.PositionInaccuracy;
            _shootDelay = enemySettings.ShootDelay;
        }
        public override void OnStartGame()
        {
            base.OnStartGame();
            _moveAgent = new EnemyMoveAgent(this, _positionInaccuracy);
            _attackAgent = new EnemyAttackAgent(this, _shootDelay);
        }
        
        public IEnumerable<IGameListener> ProvideListeners()
        {
            yield return MoveAgent;
            yield return AttackAgent;
            yield return MoveComponent;
        }
        
        [Serializable]
        public class EnemySettings : UnitSettings
        {
            public float PositionInaccuracy = 0.25f;
            public float ShootDelay = 1;
        }
    }
}