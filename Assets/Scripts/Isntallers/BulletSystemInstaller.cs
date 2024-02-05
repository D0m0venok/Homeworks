using UnityEngine;
using VG.Utilites;

namespace ShootEmUp
{
    public class BulletSystemInstaller : MonoInstaller
    {
        [SerializeField] private Transform _bulletsPoolContainer;
        [SerializeField] private Transform _worldTransform;
        
        public override void Install(DIContainer container)
        {
            container.Install(new BulletSystem(_worldTransform, _bulletsPoolContainer));
        }
    }
}