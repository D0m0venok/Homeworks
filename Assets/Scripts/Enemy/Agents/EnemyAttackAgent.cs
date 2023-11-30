using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : IGameFixedUpdateListener
    {
        private readonly WeaponComponent _weaponComponent;
        private readonly EnemyMoveAgent _moveAgent;
        private readonly float _shootDelay;
        private Player _target;
        private float _currentTime;
        
        public event Action<Vector2, Vector2> OnFired;

        public EnemyAttackAgent(Enemy enemy, float shootDelay)
        {
            _weaponComponent = enemy.WeaponComponent;
            _moveAgent = enemy.MoveAgent;
            _shootDelay = shootDelay;
        }
        public void OnFixedUpdate(float fixedDeltaTime)
        {
            if (!_moveAgent.IsReached)
                return;

            if (!_target.HitPointsComponent.IsHitPointsExists())
                return;

            _currentTime -= fixedDeltaTime;
            if (_currentTime <= 0)
            {
                Fire();
                _currentTime += _shootDelay;
            }
        }
        public void SetTarget(Player target)
        {
            _target = target;
            _currentTime = _shootDelay;
        }
        
        private void Fire()
        {
            var startPosition = _weaponComponent.Position;
            var vector = (Vector2) _target.transform.position - startPosition;
            var direction = vector.normalized;
            OnFired?.Invoke(startPosition, direction);
        }
    }
}