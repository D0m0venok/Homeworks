using System;
using VG.Utilites;

namespace ShootEmUp
{
    [InstallMono, InjectTo]
    public sealed class Player : Unit, IStart
    {
        [Inject] private PlayerSettings _playerSettings;

        void IStart.OnStart()
        {
            _isPlayer = _playerSettings.IsPlayer;
            _speed = _playerSettings.Speed;
            _hitPoint = _playerSettings.HitPoint;
            
            Construct();
        }

        [Serializable]
        public class PlayerSettings : UnitSettings
        {
        }
    }
}