using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour, IGameStartListener
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private LevelBounds _levelBounds;
        [Space]
        [SerializeField] private BulletPool _pool;

        public void OnStartGame()
        {
            _pool.Construct(_gameManager);
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