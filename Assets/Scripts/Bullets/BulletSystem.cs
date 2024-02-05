using UnityEngine;
using VG.Utilites;

namespace ShootEmUp
{
    [InjectTo]
    public sealed class BulletSystem : Listener, IGameStartListener
    {
        [Inject] private readonly PoolFactory<Bullet> _pool;
        
        private readonly Transform _active;
        private readonly Transform _disable;

        public BulletSystem(Transform active, Transform disable)
        {
            _active = active;
            _disable = disable;
        }

        public void OnStartGame()
        {
            _pool.ActiveContainer = _active;
            _pool.DisableContainer = _disable;
        }
        public void FlyBulletByArgs(Args args)
        {
            _pool.Get().SetBullet(args);
        }
    }
}