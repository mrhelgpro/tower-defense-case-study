using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Gameplay
{
    [CreateAssetMenu(fileName = "Config_Gameplay", menuName = "TowerDefence/Gameplay/GameplayConfig")]
    public class GameplayConfig : ScriptableObject
    {
        [SerializeField] private int _teamCharactersAmount = 5;
        
        [Header("Collections")]
        [SerializeField] private List<TeamIdentifier> _teamIdentifiers = new ();
        [SerializeField] private List<CharacterConfig> _characterConfigs = new ();
        
        public int TeamCharactersAmount => _teamCharactersAmount;
        public List<TeamIdentifier> TeamIdentifiers => _teamIdentifiers;
        
        public TeamIdentifier GetRandomTeamIdentifier()
        {
            return _teamIdentifiers[Random.Range(0, _teamIdentifiers.Count)];;
        }

        public CharacterConfig GetRandomCharacterConfig()
        {
            return _characterConfigs[Random.Range(0, _characterConfigs.Count)];
        }
    }
}
