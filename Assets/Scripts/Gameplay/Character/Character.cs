using System;
using UnityEngine;


namespace TowerDefence.Gameplay
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterMovement _characterMovement;
        [SerializeField] private CharacterAttack _characterAttack;
        [SerializeField] private CharacterSkin _characterSkin;
        [SerializeField] private CharacterAnimator _characterAnimator;

        public event Action<TeamIdentifier,TeamIdentifier> OnTeamChanged;
        
        public HealthPointsRuntime HealthPointsRuntime { get; } = new();
        public CharacterConfig CharacterConfig { get; private set; }
        public TeamIdentifier TeamIdentifier { get; private set; }
        public float AttackCooldownTime { get; private set; }

        public void Construct(CharacterConfig characterConfig, TeamIdentifier teamIdentifier)
        {
            CharacterConfig = characterConfig;
            
            Configure(teamIdentifier);
        }
        
        public void MoveToPosition(Vector3 position)
        {
            _characterMovement.MoveTo(position);
            _characterAnimator.PlayMove();
        }
        
        public void MoveToDirection(Vector3 direction)
        {
            _characterMovement.MoveInDirection(direction);
            _characterAnimator.PlayMove();
        }

        public void MoveStop()
        {
            _characterMovement.Stop();
            _characterAnimator.PlayIdle();
        }

        public void DealDamage()
        {
            var currentTime = Time.time;

            if (currentTime > AttackCooldownTime)
            {
                _characterAttack.Attack();
                _characterAnimator.PlayAttack();
            
                var attackCooldown = 1 / CharacterConfig.AttackSpeed;

                AttackCooldownTime = currentTime + attackCooldown;
            }
        }

        public void TakeDamage(AttackDamageRuntime attackDamageRuntime)
        {
            HealthPointsRuntime.TakeDamage(attackDamageRuntime.Damage);
            _characterSkin.ShowDamageReceived();

            if (HealthPointsRuntime.CurrentHealth <= 0)
            {
                Configure(attackDamageRuntime.Attacker);
            }
        }

        private void Configure(TeamIdentifier currentTeamIdentifier)
        {
            var previousTeamIdentifier = TeamIdentifier;
            TeamIdentifier = currentTeamIdentifier;
            
            HealthPointsRuntime.Configure(CharacterConfig.HealthPoint);
            _characterMovement.Configure(CharacterConfig.MoveSpeed);
            _characterAttack.Configure(TeamIdentifier, CharacterConfig.AttackDamage, CharacterConfig.AttackRadius);
            _characterSkin.ShowTeamColor(TeamIdentifier.Color);
            
            OnTeamChanged?.Invoke(previousTeamIdentifier, currentTeamIdentifier);
        }
    }
}