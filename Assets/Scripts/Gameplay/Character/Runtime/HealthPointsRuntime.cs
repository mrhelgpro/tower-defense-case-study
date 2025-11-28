using System;

namespace TowerDefence.Gameplay
{
    public class HealthPointsRuntime
    {
        public event Action OnHealthPointsChanged;
        
        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }
        
        public void Configure(float maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            OnHealthPointsChanged?.Invoke();
        }

        public void TakeDamage(float amount)
        {
            CurrentHealth -= amount;
            OnHealthPointsChanged?.Invoke();
        }
    }
}