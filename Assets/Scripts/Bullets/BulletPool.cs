using UnityEngine;

namespace ShootEmUp
{
    internal class BulletPool : GameObjectsPool<Bullet>
    {
        public BulletPool(Bullet prefab, Transform disabledParent, int initSize) : base(prefab, disabledParent, initSize)
        {
        }
    }
}