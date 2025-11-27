using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Gameplay
{
    [CreateAssetMenu(fileName = "Config_Team", menuName = "TowerDefence/Gameplay/TeamConfig")]
    public class TeamConfig : ScriptableObject
    {
        [SerializeField] private List<TeamIdentifier> _teamIdentifiers = new ();
        
        public List<TeamIdentifier> TeamIdentifiers => _teamIdentifiers;
    }
}
