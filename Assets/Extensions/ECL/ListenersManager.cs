using System;
using System.Collections.Generic;
using UnityEngine;

namespace VG.Utilites
{
    public sealed class ListenersManager : MonoBehaviour
    {
        private static readonly ListenersManager _instance;
        private static readonly Listeners<IManagerListener> _listeners = new Listeners<IManagerListener>();
        private static int _state;
        
        static ListenersManager()
        {
            _instance = new GameObject(nameof(ListenersManager)).AddComponent<ListenersManager>();
            DontDestroyOnLoad(_instance);
        }

        public static bool IsUpdatesEnabled { get { return _instance.enabled; } set { _instance.enabled = value; } }

        public static void Add(IManagerListener listener)
        {
            _listeners.Add(listener);
        }
        public static void Add(IEnumerable<IManagerListener> listeners)
        {
            _listeners.Add(listeners);
        }
        public static void Remove(IManagerListener listener)
        {
            _listeners.Remove(listener);
        }
        public static void Remove(IEnumerable<IManagerListener> listeners)
        {
            _listeners.Remove(listeners);
        }
        public static void Invoke<T>(Action<T> action) where T : IManagerListener
        {
            _listeners.ForEach(action);
        }

        private void Update()
        {
            _listeners.ForEach<IUpdate>(l => l.OnEntityUpdate());
        }
        private void LateUpdate()
        {
            _listeners.ForEach<ILateUpdate>(l => l.OnEntityLateUpdate());
        }
        private void FixedUpdate()
        {
            _listeners.ForEach<IFixedUpdate>(l => l.OnEntityFixedUpdate());
        }
    }
}