using System;
using VG.Utilites;

namespace ShootEmUp
{
    [InjectTo]
    public sealed class Player : Unit
    {
        [Inject] private PlayerSettings _playerSettings;

        public override void OnEntityAwake()
        {
            _isPlayer = _playerSettings.IsPlayer;
            _speed = _playerSettings.Speed;
            _hitPoint = _playerSettings.HitPoint;
            
            base.OnEntityAwake();
        }

        [Serializable]
        public class PlayerSettings : UnitSettings
        {
        }
    }
}