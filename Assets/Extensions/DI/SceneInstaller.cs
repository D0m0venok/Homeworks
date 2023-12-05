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

            if (_resourcesPaths.Length == 0)
                return;

            foreach (var path in _resourcesPaths.Distinct())
            {
                InstallPrefabs(path);
                InstallScriptables(path);
            }
            
            Resources.UnloadUnusedAssets();

            DIContainer.Install(_collection);
            
            foreach (var (type, monoBehaviour) in _injects)
            {
                DIContainer.InjectTo(type, monoBehaviour);
            }
            _injects.Clear();
        }
        private void OnDestroy()
        {
            DIContainer.Remove(_collection);
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
            _collection.Add(type, obj, id);
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

            switch (attr.Type)
            {
                case InstallType.Instance:
                    Install(type, obj, attr.Id);
                    break;
                case InstallType.Factory:
                    var factoryType = typeof(Factory<>).MakeGenericType(type);
                    Install(factoryType, factoryType.GetConstructor(new []{type})?.Invoke(new object[]{obj}), attr.Id);
                    break;
                case InstallType.PoolFactory:
                    var poolFactoryType = typeof(PoolFactory<>).MakeGenericType(type);
                    Install(poolFactoryType, poolFactoryType.GetConstructor(new []{type})?.Invoke(new object[]{obj}), attr.Id);
                    break;
                    break;
            }
        }
        private void InstallSceneObjects()
        {
            foreach (var o in FindObjectsOfType<MonoBehaviour>(true))
            {
                var type = o.GetType();

                InstallMono(type, o);

                if (o is IInstaller installer)
                    _collection.Add(installer.Process());

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

        private readonly DICollection _collection = new DICollection();
        private readonly List<Tuple<Type, MonoBehaviour>> _injects = new List<Tuple<Type, MonoBehaviour>>();
    }
}