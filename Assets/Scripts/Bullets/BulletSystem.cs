using VG.Utilites;

namespace ShootEmUp
{
    [InjectTo]
    public sealed class BulletSystem
    {
        [Inject] private readonly PoolFactory<Bullet> _pool;
        
        public void FlyBulletByArgs(Args args)
        {
            _pool.Get().SetBullet(args);
        }
    }
}