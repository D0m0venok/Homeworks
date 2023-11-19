using UnityEngine;

namespace ShootEmUp
{
    internal sealed class EnemyPool : GameObjectsPool<Enemy>
    {
        public EnemyPool(Enemy prefab, Transform disabledParent, int initSize, int maxSize) : base(prefab, disabledParent, initSize, maxSize)
        {
        }
    }
}