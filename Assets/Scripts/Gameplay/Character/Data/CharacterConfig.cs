using UnityEngine;

namespace TowerDefence.Gameplay
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "TowerDefence/Gameplay/CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
        [SerializeField] private Character _characterPrefab;
        
        [Header("Race")]
        [SerializeField] private CharacterRaceType _raceType;
        
        [Header("Stats")]
        [SerializeField] private int _healthPoint;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _attackDamage;
        [SerializeField] private float _attackRadius;
        
        public Character CharacterPrefab => _characterPrefab;
        public CharacterRaceType RaceType => _raceType;
        public int HealthPoint => _healthPoint;
        public float MoveSpeed => _moveSpeed;
        public float AttackSpeed => _attackSpeed;
        public float AttackDamage => _attackDamage;
        public float AttackRadius => _attackRadius;
    }
}
