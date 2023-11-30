using System;

namespace ShootEmUp
{
    public sealed class HitPointsComponent
    {
        private readonly Unit _unit;
        private int _hitPoints;
        private readonly int _cacheHitPoint;

        public HitPointsComponent(Unit unit, int hitPoints)
        {
            _unit = unit;
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
                OnDeath?.Invoke(_unit);
                _hitPoints = _cacheHitPoint;
            }
        }
    }
}