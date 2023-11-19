using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Unit))]
    public sealed class HitPointsComponent : MonoBehaviour
    {
        [SerializeField] private int _hitPoints;

        private int _cacheHitPoint;
        private Unit _unit;
        public event Action<Unit> OnDeath;

        private void Awake()
        {
            _unit = GetComponent<Unit>();
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