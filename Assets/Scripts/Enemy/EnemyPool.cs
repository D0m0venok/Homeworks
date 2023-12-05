using System;
using UnityEngine;
using VG.Utilites;

namespace ShootEmUp
{
    public sealed class EnemyPool : GameObjectsPool<Enemy>
    {
        [Inject] private readonly GameManager _gameManager;
        
        public EnemyPool(GameManager gameManager,  Settings settings, Transform container) : 
            base(settings.EnemyPrefab, container, settings.InitialCount, settings.MaxCount)
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