using UnityEngine;

namespace ShootEmUp
{
    internal class BulletPool : GameObjectsPool<Bullet>
    {
        private readonly GameManager _gameManager;
        
        public BulletPool(Bullet prefab, Transform disabledParent, int initSize, GameManager gameManager) : base(prefab, disabledParent, initSize)
        {
            _gameManager = gameManager;
        }
        
        public override Bullet Get(Transform parent = null)
        {
            var obj = base.Get(parent);
            _gameManager.AddListener(obj);
            return obj;
        }
        public override void Put(Bullet obj)
        {
            _gameManager.RemoveListener(obj);
            base.Put(obj);
        }
    }
}