using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class BulletSystemInstaller : MonoInstaller
    {
        [SerializeField] private Transform _bulletsPoolContainer;
        [SerializeField] private Transform _worldTransform;

        public override void InstallBindings()
        {
            Container.Bind<BulletPool>().AsSingle().WithArguments(_bulletsPoolContainer);
            Container.Bind<BulletSystem>().AsSingle().WithArguments(_worldTransform);
        }
    }
}