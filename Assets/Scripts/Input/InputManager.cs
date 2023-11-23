using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour, 
        IGameStartListener, 
        IGameFinishListener,
        IGamePauseListener,
        IGameResumeListener,
        IGameFixedUpdateListener
    {
        public event Action OnFired = delegate {};
        public event Action<Vector2> OnMoved = delegate {};

        private Controls _controls;
        
        public void OnStartGame()
        {
            _controls = new Controls();

            _controls.Main.Fire.started += SubscribeFire();
            
            _controls.Enable();
        }
        public void OnFinishGame()
        {
            _controls.Disable();
            
            _controls.Main.Fire.started -= SubscribeFire();
        }
        public void OnPauseGame()
        {
            _controls.Disable();
        }
        public void OnResumeGame()
        {
            _controls.Enable();
        }
        public void OnFixedUpdate(float fixedDeltaTime)
        {
            var moveInput = _controls.Main.Move.ReadValue<float>();
            OnMoved(new Vector2(moveInput, 0) * fixedDeltaTime);
        }

        private Action<InputAction.CallbackContext> SubscribeFire()
        {
            return context => OnFired(); 
        }
            
    }
}