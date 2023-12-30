using System;
using Extensions;
using VG.Utilites;

namespace ShootEmUp
{
    // public sealed class BulletPool : GameObjectsPool<Bullet>
    // {
    //     public BulletPool(BulletPoolSettings settings) : 
    //         base(DI.Container.Get<Bullet>(), settings.InitialCount)
    //     {
    //     }
    //     protected override Bullet Create()
    //     {
    //         var bullet = base.Create();
    //         DI.Container.InjectTo(bullet);
    //         return bullet;
    //     }
    //
    //     [Serializable]
    //     public class BulletPoolSettings
    //     {
    //         public int InitialCount = 20;
    //     }
    // }
}