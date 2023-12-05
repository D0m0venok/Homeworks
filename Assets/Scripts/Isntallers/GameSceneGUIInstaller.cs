using UnityEngine;

namespace ShootEmUp
{
    public class GameSceneGUIInstaller
    {
        [SerializeField] private PauseGameController _pauseGameController;
        [SerializeField] private FinishGameController _finishGameController;
        public void InstallBindings()
        {
            // Container.BindInterfacesAndSelfTo<PauseGameController>().FromInstance(_pauseGameController).AsSingle();
            // Container.Bind<FinishGameController>().FromInstance(_finishGameController).AsSingle();
        }
    }
}