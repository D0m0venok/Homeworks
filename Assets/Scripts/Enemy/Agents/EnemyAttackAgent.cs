using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(WeaponComponent), typeof(EnemyMoveAgent))]
    public sealed class EnemyAttackAgent : MonoBehaviour, IGameFixedUpdateListener
    {
        [SerializeField] private float _countdown = 1;
        
        [SerializeField] private WeaponComponent _weaponComponent;
        [SerializeField] private EnemyMoveAgent _moveAgent;
        private Player _target;
        private float _currentTime;
        
        public event Action<Vector2, Vector2> OnFired;

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
                _currentTime += _countdown;
            }
        }
        public void SetTarget(Player target)
        {
            _target = target;
            _currentTime = _countdown;
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