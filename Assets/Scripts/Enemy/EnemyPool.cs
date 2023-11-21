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
            AddToListeners(obj);
            return obj;
        }
        public override void Put(Enemy obj)
        {
            RemoveFromListeners(obj);
            base.Put(obj);
        }

        private void AddToListeners(Enemy obj)
        {
            _gameManager.AddListener(obj.AttackAgent);
            _gameManager.AddListener(obj.MoveAgent);
            _gameManager.AddListener(obj.MoveComponent);
            _gameManager.AddListener(obj);
        }
        private void RemoveFromListeners(Enemy obj)
        {
            _gameManager.RemoveListener(obj.AttackAgent);
            _gameManager.RemoveListener(obj.MoveAgent);
            _gameManager.RemoveListener(obj.MoveComponent);
            _gameManager.RemoveListener(obj);
        }
    }
}