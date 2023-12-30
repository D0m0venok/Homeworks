using System;
using UnityEngine;
using VG.Utilites;

namespace ShootEmUp
{
    public abstract class Unit : Entity, IAwake
    {
        [InjectLocal] private Transform _firePoint;
        [InjectLocal] private Rigidbody2D _rigidbody2D;
        
        protected int _hitPoint;
        protected bool _isPlayer;
        protected float _speed;

        public virtual void OnEntityAwake()
        {
            HitPointsComponent = new HitPointsComponent(this, _hitPoint);
            TeamComponent = new TeamComponent(_isPlayer);
            WeaponComponent = new WeaponComponent(_firePoint);
            MoveComponent = new MoveComponent(_rigidbody2D, _speed);
        }

        public TeamComponent TeamComponent { get; private set; }
        public WeaponComponent WeaponComponent { get; private set; }
        public HitPointsComponent HitPointsComponent { get; private set; }
        public MoveComponent MoveComponent { get; private set; }
        


        [Serializable]
        public abstract class UnitSettings
        {
            public int HitPoint;
            public bool IsPlayer;
            public float Speed = 5.0f;
        }
    }
}