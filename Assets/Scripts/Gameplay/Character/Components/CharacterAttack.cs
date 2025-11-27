using UnityEngine;

namespace TowerDefence.Gameplay
{
    public class CharacterAttack : MonoBehaviour
    {
        private TeamIdentifier _teamIdentifier;
        private float _attackDamage;
        private float _attackRadius;

        public void Configure(TeamIdentifier teamIdentifier, float damage, float radius)
        {
            _teamIdentifier = teamIdentifier;
            _attackDamage = damage;
            _attackRadius = radius;
        }

        public void Attack()
        {
            var attackDamageRuntime = new AttackDamageRuntime(_teamIdentifier, _attackDamage);
            var hitColliders = Physics.OverlapSphere(transform.position, _attackRadius);
            
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.TryGetComponent<Character>(out var character))
                {
                    if (character.TeamIdentifier != _teamIdentifier)
                    {
                        character.TakeDamage(attackDamageRuntime);
                    }
                }
            }
        }
    }
}
