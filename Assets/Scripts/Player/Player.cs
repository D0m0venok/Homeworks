using System;

namespace ShootEmUp
{
    public sealed class Player : Unit
    {
        public void Construct(PlayerSettings playerSettings)
        {
            _isPlayer = playerSettings.IsPlayer;
            _speed = playerSettings.Speed;
            _hitPoint = playerSettings.HitPoint;
        }
        
        [Serializable]
        public class PlayerSettings : UnitSettings
        {
        }
    }
}