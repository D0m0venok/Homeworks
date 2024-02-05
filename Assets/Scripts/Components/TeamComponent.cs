using VG.Utilites;

namespace ShootEmUp
{
    public sealed class TeamComponent : EntityComponent
    {
        public TeamComponent(bool isPlayer)
        {
            IsPlayer = isPlayer;
        }

        public readonly bool IsPlayer;
    }
}