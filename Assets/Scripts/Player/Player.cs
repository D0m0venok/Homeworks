using System;
using Zenject;

namespace ShootEmUp
{
    public sealed class Player : Unit
    {
        [Inject]
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