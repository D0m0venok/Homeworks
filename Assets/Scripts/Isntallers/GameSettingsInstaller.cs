using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    //[CreateAssetMenu(menuName = "Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private EnemyPool.Settings _enemiesPool;
        [SerializeField] private BulletPool.Settings _booletsPool;
        [SerializeField] private EnemyManager.Settings _enemyManager;
        [SerializeField] private PlayerController.Settings _playerController;
        [SerializeField] private Player.PlayerSettings _playerSettings;
        [SerializeField] private Enemy.EnemySettings _enemySettings;

        public override void InstallBindings()
        {
            Container.BindInstance(_enemiesPool);
            Container.BindInstance(_booletsPool);
            Container.BindInstance(_enemyManager);
            Container.BindInstance(_playerController);
            Container.BindInstance(_playerSettings);
            Container.BindInstance(_enemySettings);
        }
    }
}