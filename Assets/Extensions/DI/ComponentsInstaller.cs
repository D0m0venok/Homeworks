using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VG.Utilites
{
    [AddComponentMenu("DI/Components Installer")]
    public class ComponentsInstaller :  MonoBehaviour
    {
        private enum IdType
        {
            None,
            FromName,
            Custom,
        }
        
        [SerializeField] 
        private IdType _idType;
        [SerializeField]
        private string _id;
        [SerializeField] 
        private Component[] _installs;

        private void OnValidate()
        {
            if (_installs == null || _installs.Length == 0) 
                return;
            
            _installs = _installs.Distinct().ToArray();
        }

        public string Id
        {
            get
            {
                return _idType switch
                {
                    IdType.None => null,
                    IdType.FromName => name,
                    IdType.Custom => _id,
                    _ => null
                };
            }
        }
        public IEnumerable<Component> Installs { get { return _installs; } }
    }
}