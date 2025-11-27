namespace TowerDefence.Gameplay
{
    public class AttackDamageRuntime
    {
        public TeamIdentifier Attacker { get; }
        public float Damage { get; }
        
        public AttackDamageRuntime(TeamIdentifier attacker, float damage)
        {
            Attacker = attacker;
            Damage = damage;
        }
    }
}
