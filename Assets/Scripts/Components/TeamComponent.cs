namespace ShootEmUp
{
    public sealed class TeamComponent
    {
        public TeamComponent(bool isPlayer)
        {
            IsPlayer = isPlayer;
        }

        public readonly bool IsPlayer;
    }
}