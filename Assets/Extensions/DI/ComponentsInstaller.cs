using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VG.Utilites
{
    [AddComponentMenu("DI/Components Installer")]
    public class ComponentsInstaller :  MonoBehaviour
    {
        [SerializeField] 
        private string _id;
        [SerializeField] 
        private Component[] _installs;

        private void OnValidate()
        {
            if (_installs != null && _installs.Length > 0)
            {
                _installs = _installs.Distinct().ToArray();
                return;
            }

            var mono = GetComponent<MonoBehaviour>();
            if(!(mono is ComponentsInstaller))
                _installs = new Component[] { mono };
        }
        
        public string Id { get { return _id; } }
        public IEnumerable<Component> Installs { get { return _installs; } }
    }
}