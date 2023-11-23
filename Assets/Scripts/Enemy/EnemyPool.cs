using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    internal sealed class EnemyPool : GameObjectsPool<Enemy>
    {
        [SerializeField] private int _initialCount = 7;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Transform _container;
        
        private GameManager _gameManager;
        
        public void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
            Construct(_enemyPrefab, _container, _initialCount, _initialCount);
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