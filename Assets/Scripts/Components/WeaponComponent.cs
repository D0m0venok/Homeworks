using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        
        public Vector2 Position => _firePoint.position;
        public Quaternion Rotation => _firePoint.rotation;
    }
}