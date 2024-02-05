using System;
using UnityEngine;
using VG.Utilites;

namespace ShootEmUp
{
    [InjectTo]
    public abstract class Unit : Entity
    {
        [InjectLocal] private Transform _firePoint;
        [InjectLocal] private Rigidbody2D _rigidbody2D;
        
        protected int _hitPoint;
        protected bool _isPlayer;
        protected float _speed;

        protected void Construct()
        {
            Add(new HitPointsComponent(_hitPoint));
            Add(new TeamComponent(_isPlayer));
            Add(new WeaponComponent(_firePoint));
            Add(new MoveComponent(_rigidbody2D, _speed));
        }

        [Serializable]
        public abstract class UnitSettings
        {
            public int HitPoint;
            public bool IsPlayer;
            public float Speed = 5.0f;
        }
    }
}