using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletPool : GameObjectsPool<Bullet>
    {
        private readonly GameManager _gameManager;

        public BulletPool(GameManager gameManager, Settings settings, Transform container) : 
            base(settings.BulletPrefab, container, settings.InitialCount)
        {
            _gameManager = gameManager;
        }
        public override Bullet Get(Transform parent = null)
        {
            var obj = base.Get(parent);
            _gameManager.AddListeners(obj.ProvideListeners());
            return obj;
        }
        public override void Put(Bullet obj)
        {
            _gameManager.RemoveListeners(obj.ProvideListeners());
            base.Put(obj);
        }
        protected override Bullet Create()
        {
            var obj = base.Create();
            obj.OnStartGame();
            return obj;
        }

        [Serializable]
        public class Settings
        {
            public int InitialCount = 20;
            public Bullet BulletPrefab;
        }
    }
}