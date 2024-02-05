using UnityEngine;
using VG.Utilites;

namespace ShootEmUp
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private Transform _levelBackground;
        [SerializeField] private LevelBackground.Params _levelBackgroundParameters;
        [SerializeField] private Transform[] _levelBounds;
        
        public override void Install(DIContainer container)
        {
            container.Install(new LevelBackground(_levelBackground, _levelBackgroundParameters));
            container.Install(new LevelBounds(_levelBounds));
            
            container.Install<PlayerController>();
            //container.Install<InputSystemManager>();
            var input = new InputSystemManager();
            container.Install<IMoveInput>(input);
            container.Install<IFireInput>(input);
        }
    }
}
