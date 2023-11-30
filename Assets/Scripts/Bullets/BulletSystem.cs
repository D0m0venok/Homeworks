using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem
    {
        private readonly BulletPool _pool;
        private readonly LevelBounds _levelBounds;
        private readonly Transform _worldTransform;

        public BulletSystem(BulletPool pool, LevelBounds levelBounds, Transform worldTransform)
        {
            _pool = pool;
            _levelBounds = levelBounds;
            _worldTransform = worldTransform;
        }
        
        public void FlyBulletByArgs(Args args)
        {
            _pool.Get(_worldTransform).SetBullet(_pool, _levelBounds, args);
        }
    }
}