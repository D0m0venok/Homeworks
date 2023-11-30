using System;
using UnityEngine;

namespace ShootEmUp
{
    public abstract class Unit : MonoBehaviour, IGameStartListener
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        
        protected int _hitPoint;
        protected bool _isPlayer;
        protected float _speed;

        private TeamComponent _teamComponent;
        private HitPointsComponent _hitPointsComponent;
        private WeaponComponent _weaponComponent;
        private MoveComponent _moveComponent;
        
        public virtual void OnStartGame()
        {
            _hitPointsComponent = new HitPointsComponent(this, _hitPoint);
            _teamComponent = new TeamComponent(_isPlayer);
            _weaponComponent = new WeaponComponent(_firePoint);
            _moveComponent = new MoveComponent(_rigidbody2D, _speed);
        }
        
        public TeamComponent TeamComponent => _teamComponent;
        public WeaponComponent WeaponComponent => _weaponComponent;
        public HitPointsComponent HitPointsComponent => _hitPointsComponent;
        public MoveComponent MoveComponent => _moveComponent;
        
        [Serializable]
        public abstract class UnitSettings
        {
            public int HitPoint;
            public bool IsPlayer;
            public float Speed = 5.0f;
        }
    }
}