using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Unit))]
    public sealed class HitPointsComponent : MonoBehaviour, IGameAttachListener
    {
        [SerializeField] private Unit _unit;
        [SerializeField] private int _hitPoints;

        private int _cacheHitPoint;
        public event Action<Unit> OnDeath;

        public void Attach()
        {
            _cacheHitPoint = _hitPoints;
        }
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