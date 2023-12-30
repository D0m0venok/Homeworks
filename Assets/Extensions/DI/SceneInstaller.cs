using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace VG.Utilites
{
    [DefaultExecutionOrder(-1000)]
    [DisallowMultipleComponent]
    public sealed class SceneInstaller : MonoBehaviour
    {
        private static bool _inited;
        
        [SerializeField] 
        private string[] _resourcesPaths;
        
        private void Awake()
        {
            if (_inited)
            {
                Debug.LogWarning($"Deleted duplicated {nameof(SceneInstaller)} {name}");
                Destroy(gameObject);
                return;
            }
            
            _inited = true;

            InstallSceneObjects();

            if (_resourcesPaths.Length > 0)
            {
                foreach (var path in _resourcesPaths.Distinct())
                {
                    InstallPrefabs(path);
                    InstallScriptables(path);
                }

                Resources.UnloadUnusedAssets();
            }

            DI.Container.Install(_container);
            
            foreach (var (type, monoBehaviour) in _injects)
            {
                DI.Container.InjectTo(type, monoBehaviour);
            }
            _injects.Clear();
        }
        private void OnDestroy()
        {
            DI.Container.Remove(_container);
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("GameObject/DI Scene Installer", false, 11)]
        private static void Create()
        {
            var installer = FindObjectOfType<SceneInstaller>();
            if (installer != null)
            {
                Debug.LogWarning(nameof(SceneInstaller) + ": is already exists");
                return;
            }
        
            new GameObject(nameof(SceneInstaller), typeof(SceneInstaller)).transform.SetParent(UnityEditor.Selection.activeTransform, false);
        }
#endif
        
        private void Install(Type type, object obj, string id)
        {
            _container.Install(type, obj, id);
        }
        private void InstallMono(Type type, Object obj)
        {
            if (obj is ComponentsInstaller ci)
            {
                foreach (var install in ci.Installs)
                {
                    Install(install.GetType(), install, ci.Id);
                }
                return;
            }
            
            var attr = type.GetCustomAttribute<InstallMonoAttribute>();
            if(attr == null)
                return;

            var nameId = attr.IdFromName ? obj.name : null; 
            
            switch (attr.Type)
            {
                case InstallType.Instance:
                    Install(type, obj, nameId);
                    break;
                case InstallType.Factory:
                    var factoryType = typeof(Factory<>).MakeGenericType(type);
                    Install(factoryType, factoryType.GetConstructor(new []{type})?.Invoke(new object[]{obj}), nameId);
                    break;
                case InstallType.PoolFactory:
                    var poolFactoryType = typeof(PoolFactory<>).MakeGenericType(type);
                    Install(poolFactoryType, poolFactoryType.GetConstructor(new []{type, typeof(int), typeof(int)})?.Invoke(new object[]{obj, attr.PoolInitSize, attr.PoolMaxSize}), nameId);
                    break;
            }
        }
        private void InstallSceneObjects()
        {
            foreach (var o in FindObjectsOfType<MonoBehaviour>(true))
            {
                var type = o.GetType();

                InstallMono(type, o);

                if (o is MonoInstaller installer)
                    installer.Install(_container);

                if(type.GetCustomAttribute<InjectToAttribute>() != null)
                    _injects.Add(new Tuple<Type, MonoBehaviour>(type, o));
            }
        }
        private void InstallPrefabs(string path)
        {
            foreach (var o in Resources.LoadAll<GameObject>(path + "/").SelectMany(go => go.GetComponents<MonoBehaviour>()))
            {
                InstallMono(o.GetType(), o);
            }
        }
        private void InstallScriptables(string path)
        {
            foreach (var o in Resources.LoadAll<ScriptableObject>(path + "/"))
            {
                var type = o.GetType();
                var attr = type.GetCustomAttribute<InstallScriptableAttribute>();
                if (attr != null)
                    Install(type, o, attr.Id);
            }
        }

        private readonly DIContainer _container = new DIContainer();
        private readonly List<Tuple<Type, MonoBehaviour>> _injects = new List<Tuple<Type, MonoBehaviour>>();
    }
}