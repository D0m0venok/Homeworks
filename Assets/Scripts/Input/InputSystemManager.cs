using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VG.Utilites;

namespace ShootEmUp
{
    public sealed class InputSystemManager : 
        IMoveInput, IFireInput, IFixedUpdate,
        IGameStartListener, IGameFinishListener,
        IGamePauseListener, IGameResumeListener
    {
        public event Action OnFired = delegate {};
        public event Action<Vector2> OnMoved = delegate {};

        private Controls _controls;

        public InputSystemManager()
        {
            ListenersManager.Add(this);
        }
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
        void IFixedUpdate.OnEntityFixedUpdate()
        {
            var moveInput = _controls.Main.Move.ReadValue<float>();
            OnMoved(new Vector2(moveInput, 0) * Time.fixedDeltaTime);
        }

        private Action<InputAction.CallbackContext> SubscribeFire()
        {
            return context => OnFired(); 
        }
            
    }
}