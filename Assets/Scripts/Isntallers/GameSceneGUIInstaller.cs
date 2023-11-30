using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class GameSceneGUIInstaller : MonoInstaller
    {
        [SerializeField] private PauseGameController _pauseGameController;
        [SerializeField] private FinishGameController _finishGameController;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PauseGameController>().FromInstance(_pauseGameController).AsSingle();
            Container.Bind<FinishGameController>().FromInstance(_finishGameController).AsSingle();
        }
    }
}