using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour
    {
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private CharacterController _characterController;

        private float _horizontalDirection;
        private bool _requestMove;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _requestMove = true;

            if (Input.GetKey(KeyCode.LeftArrow))
                _horizontalDirection = -1;
            else if (Input.GetKey(KeyCode.RightArrow))
                _horizontalDirection = 1;
            else
                _horizontalDirection = 0;
        }
        
        private void FixedUpdate()
        {
            _moveComponent.MoveByRigidbodyVelocity(new Vector2(_horizontalDirection, 0) * Time.fixedDeltaTime);
            
            if(!_requestMove)
                return;
            
            _characterController.Fire();
            _requestMove = false;
        }
    }
}