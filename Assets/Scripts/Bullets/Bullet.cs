using UnityEngine;
using VG.Utilites;

namespace ShootEmUp
{
    [InstallMono(InstallType.PoolFactory), InjectTo]
    [RequireComponent(typeof(Collider2D))]
    public sealed class Bullet : Entity, IStart,
        IFixedUpdate, ICollisionEnter2D
    {
        [InjectLocal] private SpriteRenderer _spriteRenderer;
        [InjectLocal] private Rigidbody2D _rigidbody2D;
        [Inject] private LevelBounds _levelBounds;
        [Inject] private PoolFactory<Bullet> _pool;

        private int _damage;
        private bool _isPlayer;

        void IStart.OnStart()
        {
            Add(new RigidbodyStateController(_rigidbody2D));
        }
        void ICollisionEnter2D.OnEntityCollisionEnter2D(Collision2D other)
        {
            DealDamage(other.gameObject);
            _pool.Put(this);
        }
        void IFixedUpdate.OnEntityFixedUpdate()
        {
            if(!_levelBounds.InBounds(transform.position))
                _pool.Put(this);
        }

        public void SetBullet(Args args)
        {
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
            
            if (_isPlayer == unit.Get<TeamComponent>().IsPlayer)
                return;

            unit.Get<HitPointsComponent>().TakeDamage(_damage);
        }
    }
}