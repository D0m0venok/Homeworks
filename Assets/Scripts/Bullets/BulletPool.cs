using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    internal sealed class BulletPool : GameObjectsPool<Bullet>
    {
        [SerializeField] private int _initialCount = 50;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _container;
        
        private GameManager _gameManager;
        
        public void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
            Construct(_bulletPrefab, _container, _initialCount);
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