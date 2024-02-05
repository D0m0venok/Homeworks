using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VG.Utilites
{
    public abstract class Entity : MonoBehaviour, IEntityListener, IManagerListener
    {
        public void InvokeListeners<T>(Action<T> action) where T : IEntityListener
        {
            _listeners.ForEach(action);
        }
        public void Add(Type type, EntityComponent component)
        {
            component.Init(this);
                
            _listeners.Add(component);

            if(!_components.ContainsKey(type))
                _components.Add(type, new HashSet<EntityComponent>());
            _components[type].Add(component);
            
            if(_wasEnable && gameObject.activeInHierarchy)
                ListenersManager.Add(component);
            
            if(component is IAwake awake)
                awake.OnAwake();
            
            if(component is IStart start)
                _starts.Add(start);
        }
        public void Add<T>(T component) where T : EntityComponent
        {
            Add(typeof(T), component);
        }
        public EntityComponent Add(Type type)
        {
            var component = (EntityComponent)type.GetConstructor(new Type[0])?.Invoke(null);
            Add(component);
            return component;
        }
        public T Add<T>() where T : EntityComponent, new ()
        {
            var component = new T();
            Add(component);
            return component;
        }
        public void Remove(Type type, EntityComponent component)
        {
            if (_components.TryGetValue(type, out var set))
                set.Remove(component);
        }
        public void Remove<T>(T component) where T : EntityComponent
        {
            Remove(typeof(T), component);
        }
        public void RemoveAll(Type type)
        {
            if (_components.TryGetValue(type, out var set))
                set.Clear();
        }
        public void RemoveAll<T>() where T : EntityComponent
        {
            RemoveAll(typeof(T));
        }
        public EntityComponent Get(Type type)
        {
            return _components.TryGetValue(type, out var components) ? components.First() : null;
        }
        public T Get<T>() where T : EntityComponent
        {
            return (T) Get(typeof(T));
        }
        public bool TryGet(Type type, out EntityComponent component)
        {
            component = Get(type);
            return component != null;
        }
        public bool TryGet<T>(out T component) where T : EntityComponent
        {
            component = Get<T>();
            return component != null;
        }
        public IEnumerable<EntityComponent> GetAll(Type type)
        {
            return _components.TryGetValue(type, out var components) ? components : null;
        }
        public IEnumerable<T> GetAll<T>() where T : EntityComponent
        {
            return (IEnumerable<T>)GetAll(typeof(T));
        }
        
        private void Awake()
        {
            _listeners.Add(this);
            //GetComponents(this);
            
            //_listeners.ForEach<IAwake>(l => l.OnEntityAwake());
            
            if(this is IAwake awake)
                awake.OnAwake();
        }
        private void Start()
        {
            if(this is IStart start)
                start.OnStart();
            //_listeners.ForEach<IStart>(l => l.OnEntityStart());
        }
        private void Update()
        {
            if(_starts.Count == 0)
                return;
            
            _starts.ForEach(s => s.OnStart());
            _starts.Clear();
        }
        private void LateUpdate()
        {
            
        }
        private void FixedUpdate()
        {
            
        }
        private void OnEnable()
        {
            _listeners.ForEach<IEnable>(l => l.OnEntityEnable());
            ListenersManager.Add(this);

            foreach (var components in _components.Values)
            {
                ListenersManager.Add(components);
            }
            
            if(!_wasEnable)
                _wasEnable = true;
        }
        private void OnDisable()
        {
            _listeners.ForEach<IDisable>(l => l.OnEntityDisable());
            ListenersManager.Remove(this);
            
            foreach (var components in _components.Values)
            {
                ListenersManager.Remove(components);
            }
        }
        private void OnDestroy()
        {
            _listeners.ForEach<IDestroy>(l => l.OnEntityDestroy());
            
            foreach (var components in _components.Values)
            {
                ListenersManager.Remove(components);
            }
            _components.Clear();
        }
        private void OnBecameVisible()
        {
            _listeners.ForEach<IBecameVisible>(l => l.OnEntityBecameVisible());
        }
        private void OnBecameInvisible()
        {
            _listeners.ForEach<IBecameInvisible>(l => l.OnEntityBecameInvisible());
        }
        private void OnCollisionEnter(Collision other)
        {
            _listeners.ForEach<ICollisionEnter>(l => l.OnEntityCollisionEnter(other));
        }
        private void OnCollisionStay(Collision other)
        {
            _listeners.ForEach<ICollisionStay>(l => l.OnEntityCollisionStay(other));
        }
        private void OnCollisionExit(Collision other)
        {
            _listeners.ForEach<ICollisionExit>(l => l.OnEntityCollisionExit(other));
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            _listeners.ForEach<ICollisionEnter2D>(l => l.OnEntityCollisionEnter2D(other));
        }
        private void OnCollisionStay2D(Collision2D other)
        {
            _listeners.ForEach<ICollisionStay2D>(l => l.OnEntityCollisionStay2D(other));
        }
        private void OnCollisionExit2D(Collision2D other)
        {
            _listeners.ForEach<ICollisionExit2D>(l => l.OnEntityCollisionExit2D(other));
        }

        private bool _wasEnable;
        private readonly Dictionary<Type, HashSet<EntityComponent>> _components = new Dictionary<Type, HashSet<EntityComponent>>();
        private readonly Listeners<IEntityListener> _listeners = new Listeners<IEntityListener>();
        private readonly List<IStart> _starts = new List<IStart>();
    }
}