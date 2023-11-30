using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerController : IGameStartListener, IGameFinishListener
    {
        private readonly Player _player;
        private readonly IMoveInput _moveInput;
        private readonly IFireInput _fireInput;
        private readonly FinishGameController _finishGameController;
        private readonly BulletSystem _bulletSystem;
        private readonly BulletSettings _bulletSettings;
        
        public PlayerController(Player player, IMoveInput moveInput, IFireInput fireInput, 
            FinishGameController finishGameController, BulletSystem bulletSystem, Settings settings)
        {
            _player = player;
            _player.OnStartGame();
            
            _moveInput = moveInput;
            _fireInput = fireInput;
            _finishGameController = finishGameController;
            _bulletSystem = bulletSystem;
            _bulletSettings = settings.BulletSettings;
        }
        public void OnStartGame()
        {
            _player.HitPointsComponent.OnDeath += OnDeath;
            _moveInput.OnMoved += _player.MoveComponent.MoveByRigidbodyVelocity;
            _fireInput.OnFired += OnFired;
        }
        public void OnFinishGame()
        {
            _player.HitPointsComponent.OnDeath -= OnDeath;
            _moveInput.OnMoved += _player.MoveComponent.MoveByRigidbodyVelocity;
            _fireInput.OnFired += OnFired;
        }
        private void OnFired()
        {
            _bulletSystem.FlyBulletByArgs(new Args
                (_player.WeaponComponent.Position, _player.WeaponComponent.Rotation * Vector3.up * _bulletSettings.Speed, 
                    _bulletSettings.Color, _bulletSettings.PhysicsLayer, _bulletSettings.Damage, true));
        }
        private void OnDeath(Unit unit)
        {
            _finishGameController.FinishGame();
        }
        
        [Serializable]
        public class Settings
        {
            public BulletSettings BulletSettings;
        }
    }
}