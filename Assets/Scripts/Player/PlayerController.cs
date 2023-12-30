using System;
using UnityEngine;
using VG.Utilites;

namespace ShootEmUp
{
    [InjectTo]
    public sealed class PlayerController : IGameStartListener, IGameFinishListener
    {
        [Inject] private readonly Player _player;
        [Inject] private readonly IMoveInput _moveInput;
        [Inject] private readonly IFireInput _fireInput;
        [Inject] private readonly FinishGameController _finishGameController;
        [Inject] private readonly BulletSystem _bulletSystem;
        [Inject] private readonly Settings _settings;

        public PlayerController()
        {
            ListenersManager.Add(this);
        }
        // public PlayerController(Player player, IMoveInput moveInput, IFireInput fireInput, 
        //     FinishGameController finishGameController, BulletSystem bulletSystem, Settings settings)
        // {
        //     _player = player;
        //     _moveInput = moveInput;
        //     _fireInput = fireInput;
        //     _finishGameController = finishGameController;
        //     _bulletSystem = bulletSystem;
        //     _settings = settings.BulletSettings;
        // }
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
                (_player.WeaponComponent.Position, _player.WeaponComponent.Rotation * Vector3.up * _settings.BulletSettings.Speed, 
                    _settings.BulletSettings.Color, _settings.BulletSettings.PhysicsLayer, _settings.BulletSettings.Damage, true));
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