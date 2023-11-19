using UnityEngine;

namespace ShootEmUp
{
    public struct Args
    {
        public readonly Vector2 Position;
        public readonly Vector2 Velocity;
        public readonly Color Color;
        public readonly int PhysicsLayer;
        public readonly int Damage;
        public readonly bool IsPlayer;

        public Args(Vector2 position, Vector2 velocity, Color color, PhysicsLayer physicsLayer, int damage, bool isPlayer)
        {
            Position = position;
            Velocity = velocity;
            Color = color;
            PhysicsLayer = (int)physicsLayer;
            Damage = damage;
            IsPlayer = isPlayer;
        }
    }
}