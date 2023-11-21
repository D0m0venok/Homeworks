using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] private Player _player; 
        [SerializeField] private GameController _gameController;
        [SerializeField] private GameManager _gameManager;
        [Space]
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;
        [Space]
        [SerializeField] private InputFireManager _inputFireManager;
        [SerializeField] private InputMoveManager _inputMoveManager;

        private void Awake()
        {
            _player.HitPointsComponent.OnDeath += OnDeath;
            _inputFireManager.OnFired += OnFired;
            _inputMoveManager.OnMoved += _player.MoveComponent.MoveByRigidbodyVelocity;
            _gameManager.AddListener(_player.MoveComponent);
        }
        private void OnDestroy()
        {
            _player.HitPointsComponent.OnDeath -= OnDeath;
            _inputFireManager.OnFired -= OnFired;
            _inputMoveManager.OnMoved -= _player.MoveComponent.MoveByRigidbodyVelocity;
            _gameManager.RemoveListener(_player.MoveComponent);
        }

        private void OnFired()
        {
            _bulletSystem.FlyBulletByArgs(new Args
                (_player.WeaponComponent.Position, _player.WeaponComponent.Rotation * Vector3.up * _bulletConfig.Speed, 
                    _bulletConfig.Color, _bulletConfig.Layer, _bulletConfig.Damage, true));
        }
        private void OnDeath(Unit unit)
        {
            _gameController.FinishGame();
        }
    }
}