using System;
using UnityEngine;
using VG.Utilites;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : EntityComponent, IFixedUpdate
    {
        private readonly float _shootDelay;
        private Player _target;
        private float _currentTime;
        
        public event Action<Vector2, Vector2> OnFired;

        public EnemyAttackAgent(float shootDelay)
        {
            _shootDelay = shootDelay;
        }
        void IFixedUpdate.OnEntityFixedUpdate()
        {
            if (!Entity.Get<EnemyMoveAgent>().IsReached)
                return;

            if (!_target.Get<HitPointsComponent>().IsHitPointsExists())
                return;

            _currentTime -= Time.fixedDeltaTime;
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
            var startPosition = Entity.Get<WeaponComponent>().Position;
            var vector = (Vector2) _target.transform.position - startPosition;
            var direction = vector.normalized;
            OnFired?.Invoke(startPosition, direction);
        }
    }
}