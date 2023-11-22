using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(CircleCollider2D))]
    public sealed class Bullet : RigidbodyStateController, 
        IGameFixedUpdateListener, 
        IGameDetachListener
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private int _damage;
        private bool _isPlayer;
        private Action<Bullet> _removeAction;
        private Func<Vector3, bool> _checkBoundsAction;
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            DealDamage(collision.gameObject);
            _removeAction?.Invoke(this);
        }
        
        public void OnFixedUpdate(float fixedDeltaTime)
        {
            if(!_checkBoundsAction.Invoke(transform.position))
                _removeAction?.Invoke(this);
        }
        public void Detach()
        {
            _removeAction = null;
            _checkBoundsAction = null;
        }
        public void SetBullet(Args args, Func<Vector3, bool> checkBoundsAction, Action<Bullet> removeAction)
        {
            _rigidbody2D.velocity = args.Velocity;
            gameObject.layer = args.PhysicsLayer;
            transform.position = args.Position;
            _spriteRenderer.color = args.Color;
            _damage = args.Damage;
            _isPlayer = args.IsPlayer;

            _checkBoundsAction = checkBoundsAction;
            _removeAction = removeAction;
        }
        
        private void DealDamage(GameObject other)
        {
            if (!other.TryGetComponent(out Unit unit))
                return;

            if (_isPlayer == unit.TeamComponent.IsPlayer)
                return;

            unit.HitPointsComponent.TakeDamage(_damage);
        }
    }
}