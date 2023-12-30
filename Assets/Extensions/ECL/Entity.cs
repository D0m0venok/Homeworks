using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace VG.Utilites
{
    public abstract class Entity : MonoBehaviour, IEntityListener, IManagerListener
    {
        public void InvokeListeners<T>(Action<T> action) where T : IEntityListener
        {
            _listeners.ForEach(action);
        }
        public EntityComponent GetEntityComponent(Type type)
        {
            return _components.TryGetValue(type, out var components) ? components.First() : null;
        }
        public T GetEntityComponent<T>() where T : EntityComponent
        {
            return (T) GetEntityComponent(typeof(T));
        }
        public IEnumerable<EntityComponent> GetEntityComponents(Type type)
        {
            return _components.TryGetValue(type, out var components) ? components : null;
        }
        public IEnumerable<T> GetEntityComponents<T>() where T : EntityComponent
        {
            return (IEnumerable<T>)GetEntityComponents(typeof(T));
        }
        
        protected void Awake()
        {
            _listeners.Add(this);
            GetComponents(this);
            
            _listeners.ForEach<IAwake>(l => l.OnEntityAwake());
        }
        protected void Start()
        {
            _listeners.ForEach<IStart>(l => l.OnEntityStart());
        }
        protected void OnEnable()
        {
            _listeners.ForEach<IEnable>(l => l.OnEntityEnable());
            ListenersManager.Add(this);

            foreach (var components in _components.Values)
            {
                ListenersManager.Add(components);
            }
        }
        protected void OnDisable()
        {
            _listeners.ForEach<IDisable>(l => l.OnEntityDisable());
            ListenersManager.Remove(this);
            
            foreach (var components in _components.Values)
            {
                ListenersManager.Remove(components);
            }
        }
        protected void OnDestroy()
        {
            _listeners.ForEach<IDestroy>(l => l.OnEntityDestroy());
        }
        protected void OnBecameVisible()
        {
            _listeners.ForEach<IBecameVisible>(l => l.OnEntityBecameVisible());
        }
        protected void OnBecameInvisible()
        {
            _listeners.ForEach<IBecameInvisible>(l => l.OnEntityBecameInvisible());
        }
        protected void OnCollisionEnter(Collision other)
        {
            _listeners.ForEach<ICollisionEnter>(l => l.OnEntityCollisionEnter(other));
        }
        protected void OnCollisionStay(Collision other)
        {
            _listeners.ForEach<ICollisionStay>(l => l.OnEntityCollisionStay(other));
        }
        protected void OnCollisionExit(Collision other)
        {
            _listeners.ForEach<ICollisionExit>(l => l.OnEntityCollisionExit(other));
        }
        protected void OnCollisionEnter2D(Collision2D other)
        {
            _listeners.ForEach<ICollisionEnter2D>(l => l.OnEntityCollisionEnter2D(other));
        }
        protected void OnCollisionStay2D(Collision2D other)
        {
            _listeners.ForEach<ICollisionStay2D>(l => l.OnEntityCollisionStay2D(other));
        }
        protected void OnCollisionExit2D(Collision2D other)
        {
            _listeners.ForEach<ICollisionExit2D>(l => l.OnEntityCollisionExit2D(other));
        }

        private void GetComponents(object obj)
        {
            foreach (var fieldInfo in obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (!(fieldInfo.GetValue(obj) is EntityComponent component)) 
                    continue;

                component.Init(this);
                
                _listeners.Add(component);

                var type = component.GetType();
                if(!_components.ContainsKey(type))
                    _components.Add(type, new List<EntityComponent>());
                _components[type].Add(component);

                GetComponents(component);
            }
        }
        
        private readonly Dictionary<Type, List<EntityComponent>> _components = new Dictionary<Type, List<EntityComponent>>();
        private readonly Listeners<IEntityListener> _listeners = new Listeners<IEntityListener>();
    }
}