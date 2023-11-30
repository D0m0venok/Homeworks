using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyPool : GameObjectsPool<Enemy>
    {
        private readonly GameManager _gameManager;
        
        public EnemyPool(GameManager gameManager, DiContainer diContainer, Settings settings, Transform container) : 
            base(settings.EnemyPrefab, diContainer, container, settings.InitialCount, settings.MaxCount)
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
        protected override Enemy Create()
        {
            var obj = base.Create();

            _diContainer.Inject(obj);
            obj.OnStartGame();
            return obj;
        }

        [Serializable]
        public class Settings
        {
            public int InitialCount = 7;
            public int MaxCount = 7;
            public Enemy EnemyPrefab;
        }
    }
}