using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameObject _character; 
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;

        private HitPointsComponent _hitPointsComponent;
        private WeaponComponent _weaponComponent;

        private void Awake()
        {
            _hitPointsComponent = _character.GetComponent<HitPointsComponent>();
            _weaponComponent = _character.GetComponent<WeaponComponent>();
        }
        private void OnEnable()
        {
            _hitPointsComponent.CharacterDeath += OnCharacterDeath;
        }
        private void OnDisable()
        {
            _hitPointsComponent.CharacterDeath -= OnCharacterDeath;
        }

        public void Fire()
        {
            _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                IsPlayer = true,
                PhysicsLayer = (int) _bulletConfig.Layer,
                Color = _bulletConfig.Color,
                Damage = _bulletConfig.Damage,
                Position = _weaponComponent.Position,
                Velocity = _weaponComponent.Rotation * Vector3.up * _bulletConfig.Speed
            });
        }
        
        private void OnCharacterDeath(GameObject _) => _gameManager.FinishGame();
    }
}