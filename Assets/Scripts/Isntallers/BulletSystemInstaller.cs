using UnityEngine;

namespace ShootEmUp
{
    public class BulletSystemInstaller
    {
        [SerializeField] private Transform _bulletsPoolContainer;
        [SerializeField] private Transform _worldTransform;

        public void InstallBindings()
        {
            //Container.Bind<BulletPool>().AsSingle().WithArguments(_bulletsPoolContainer);
            //Container.Bind<BulletSystem>().AsSingle().WithArguments(_worldTransform);
        }
    }
}