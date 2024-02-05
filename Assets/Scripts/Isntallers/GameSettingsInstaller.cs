using UnityEngine;
using VG.Utilites;

namespace ShootEmUp
{
    //[CreateAssetMenu(menuName = "Game Settings")]
    public class GameSettingsInstaller : ScriptableInstaller
    {
        [SerializeField] private EnemyManager.Settings _enemyManager;
        [SerializeField] private PlayerController.Settings _playerController;
        [SerializeField] private Player.PlayerSettings _playerSettings;
        [SerializeField] private Enemy.EnemySettings _enemySettings;
        
        public override void Install(DIContainer container)
        {
            container.Install(_enemyManager);
            container.Install(_playerController);
            container.Install(_playerSettings);
            container.Install(_enemySettings);
        }
    }
}