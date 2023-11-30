using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent
    {
        private readonly Transform _firePoint;

        public WeaponComponent(Transform firePoint)
        {
            _firePoint = firePoint;
        }
        
        public Vector2 Position => _firePoint.position;
        public Quaternion Rotation => _firePoint.rotation;
    }
}