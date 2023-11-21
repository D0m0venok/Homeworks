using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [Space]
        [SerializeField] private int _initialCount = 50;
        [SerializeField] private Bullet _prefab;
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _worldTransform;
        [Space]
        [SerializeField] private LevelBounds _levelBounds;

        private BulletPool _pool;

        private void Awake()
        {
            _pool = new BulletPool(_prefab, _container, _initialCount, _gameManager);
        }
        public void FlyBulletByArgs(Args args)
        {
            _pool.Get(_worldTransform).SetBullet(args, _levelBounds.InBounds, RemoveBullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            _pool.Put(bullet);
        }
    }
}