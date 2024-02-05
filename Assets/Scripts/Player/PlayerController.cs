using System;
using UnityEngine;
using VG.Utilites;

namespace ShootEmUp
{
    [InjectTo]
    public sealed class PlayerController : Listener, IGameStartListener, IGameFinishListener
    {
        [Inject] private readonly Player _player;
        [Inject] private readonly IMoveInput _moveInput;
        [Inject] private readonly IFireInput _fireInput;
        [Inject] private readonly FinishGameController _finishGameController;
        [Inject] private readonly BulletSystem _bulletSystem;
        [Inject] private readonly Settings _settings;

        public void OnStartGame()
        {
            _player.Get<HitPointsComponent>().OnDeath += OnDeath;
            _moveInput.OnMoved += _player.Get<MoveComponent>().MoveByRigidbodyVelocity;
            _fireInput.OnFired += OnFired;
        }
        public void OnFinishGame()
        {
            _player.Get<HitPointsComponent>().OnDeath -= OnDeath;
            _moveInput.OnMoved += _player.Get<MoveComponent>().MoveByRigidbodyVelocity;
            _fireInput.OnFired += OnFired;
        }
        private void OnFired()
        {
            var weaponComponent = _player.Get<WeaponComponent>();
            _bulletSystem.FlyBulletByArgs(new Args
                (weaponComponent.Position, weaponComponent.Rotation * Vector3.up * _settings.BulletSettings.Speed, 
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