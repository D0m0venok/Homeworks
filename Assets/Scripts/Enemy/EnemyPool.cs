using UnityEngine;

namespace ShootEmUp
{
    internal sealed class EnemyPool : GameObjectsPool<Enemy>
    {
        private readonly GameManager _gameManager;
        
        public EnemyPool(Enemy prefab, Transform disabledParent, int initSize, int maxSize, GameManager gameManager) : base(prefab, disabledParent, initSize, maxSize)
        {
            _gameManager = gameManager;
        }
        
        public override Enemy Get(Transform parent = null)
        {
            var obj = base.Get(parent);
            _gameManager.AddListeners(obj.ProvideListeners());
            return obj;
        }
        public override void Put(Enemy obj)
        {
            _gameManager.RemoveListeners(obj.ProvideListeners());
            base.Put(obj);
        }
    }
}