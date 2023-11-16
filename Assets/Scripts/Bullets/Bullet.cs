using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(CircleCollider2D))]
    public sealed class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Action<Bullet> _onCollisionCallback;
        
        public bool IsPlayer { get; set; }
        public int Damage { get; set; }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            BulletUtils.DealDamage(this, collision.gameObject);
            _onCollisionCallback?.Invoke(this);
        }

        public void SetCollisionCallback(Action<Bullet> action)
        {
            _onCollisionCallback = action;
        }
        public void SetVelocity(Vector2 velocity)
        {
            _rigidbody2D.velocity = velocity;
        }
        public void SetPhysicsLayer(int physicsLayer)
        {
            gameObject.layer = physicsLayer;
        }
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
        public void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }
    }
}