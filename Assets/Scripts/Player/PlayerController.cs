using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShootEmUp
{
    public sealed class PlayerController : MonoBehaviour, 
        IGameStartListener, 
        IGameFinishListener
    {
        [SerializeField] private Player _player; 
        [SerializeField] private FinishGameController _finishGameController;
        [Space]
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;
        [Space]
        [SerializeField] private InputManager _inputManager;

        public void OnStartGame()
        {
            _player.HitPointsComponent.OnDeath += OnDeath;
            _inputManager.OnFired += OnFired;
            _inputManager.OnMoved += _player.MoveComponent.MoveByRigidbodyVelocity;
        }
        public void OnFinishGame()
        {
            _player.HitPointsComponent.OnDeath -= OnDeath;
            _inputManager.OnFired += OnFired;
            _inputManager.OnMoved += _player.MoveComponent.MoveByRigidbodyVelocity;
        }
        private void OnFired()
        {
            _bulletSystem.FlyBulletByArgs(new Args
                (_player.WeaponComponent.Position, _player.WeaponComponent.Rotation * Vector3.up * _bulletConfig.Speed, 
                    _bulletConfig.Color, _bulletConfig.Layer, _bulletConfig.Damage, true));
        }
        private void OnDeath(Unit unit)
        {
            _finishGameController.FinishGame();
        }
    }
}