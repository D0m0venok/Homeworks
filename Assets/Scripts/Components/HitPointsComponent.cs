using System;
using VG.Utilites;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : EntityComponent
    {
        private int _hitPoints;
        private readonly int _cacheHitPoint;

        public HitPointsComponent(int hitPoints)
        {
            _cacheHitPoint = _hitPoints = hitPoints;
        }
        public event Action<Unit> OnDeath;
        
        public bool IsHitPointsExists() 
        {
            return _hitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            _hitPoints -= damage;
            if (_hitPoints <= 0)
            {
                OnDeath?.Invoke((Unit)Entity);
                _hitPoints = _cacheHitPoint;
            }
        }
    }
}