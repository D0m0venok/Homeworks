using UnityEngine;

namespace ShootEmUp
{
    public sealed class TeamComponent : MonoBehaviour
    {
        [SerializeField] private bool _isPlayer;

        public bool IsPlayer => _isPlayer;
    }
}