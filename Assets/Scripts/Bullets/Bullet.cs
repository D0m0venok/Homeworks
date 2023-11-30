using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(CircleCollider2D))]
    public sealed class Bullet : MonoBehaviour, 
        IGameStartListener, IGameFixedUpdateListener, 
        IGameDetachListener, IGameListenerProvider
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private int _damage;
        private bool _isPlayer;
        private BulletPool _pool;
        private LevelBounds _levelBounds;
        private RigidbodyStateController _rigidbodyStateController;
        
        public void OnStartGame()
        {
            _rigidbodyStateController = new RigidbodyStateController(_rigidbody2D);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            _pool?.Put(this);
            DealDamage(collision.gameObject);
        }
        
        public void OnFixedUpdate(float fixedDeltaTime)
        {
            if(!_levelBounds.InBounds(transform.position))
                _pool?.Put(this);
        }
        public void Detach()
        {
            _pool = null;
        }
        public IEnumerable<IGameListener> ProvideListeners()
        {
            yield return _rigidbodyStateController;
            yield return this;
        }
        public void SetBullet(BulletPool pool, LevelBounds levelBounds, Args args)
        {
            _pool = pool;
            _levelBounds = levelBounds;
            
            transform.position = args.Position;
            _rigidbody2D.velocity = args.Velocity;
            gameObject.layer = args.PhysicsLayer;
            _spriteRenderer.color = args.Color;
            _damage = args.Damage;
            _isPlayer = args.IsPlayer;
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