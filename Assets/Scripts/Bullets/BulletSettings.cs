using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class BulletSettings
    {
        public PhysicsLayer PhysicsLayer;
        public Color Color;
        public int Damage;
        public float Speed;
    }
}