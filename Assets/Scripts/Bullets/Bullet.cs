using UnityEngine;
using VG.Utilites;

namespace ShootEmUp
{
    [InstallMono(InstallType.PoolFactory, 20)]
    [RequireComponent(typeof(Collider2D))]
    public sealed class Bullet : Entity, IAwake,
        IFixedUpdate, ICollisionEnter2D
    {
        [InjectLocal] private SpriteRenderer _spriteRenderer;
        [InjectLocal] private Rigidbody2D _rigidbody2D;
        [Inject] private LevelBounds _levelBounds;
        [Inject] private PoolFactory<Bullet> _pool;

        private int _damage;
        private bool _isPlayer;
        private readonly RigidbodyStateController _rigidbodyStateController = new ();

        void IAwake.OnEntityAwake()
        {
            _rigidbodyStateController.Init(_rigidbody2D);
        }
        void ICollisionEnter2D.OnEntityCollisionEnter2D(Collision2D other)
        {
            _pool?.Put(this);
            DealDamage(other.gameObject);
        }
        void IFixedUpdate.OnEntityFixedUpdate()
        {
            if(!_levelBounds.InBounds(transform.position))
                _pool?.Put(this);
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

            if (_isPlayer == unit.TeamComponent.IsPlayer)
                return;

            unit.HitPointsComponent.TakeDamage(_damage);
        }
    }
}