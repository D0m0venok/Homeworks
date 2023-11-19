using UnityEngine;

namespace ShootEmUp
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] private TeamComponent _teamComponent;
        [SerializeField] private WeaponComponent _weaponComponent;
        [SerializeField] private HitPointsComponent _hitPointsComponent;
        [SerializeField] private MoveComponent _moveComponent;
    
        public TeamComponent TeamComponent => _teamComponent;
        public WeaponComponent WeaponComponent => _weaponComponent;
        public HitPointsComponent HitPointsComponent => _hitPointsComponent;
        public MoveComponent MoveComponent => _moveComponent;
    }
}