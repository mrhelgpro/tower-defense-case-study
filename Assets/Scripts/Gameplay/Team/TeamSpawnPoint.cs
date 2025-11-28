using UnityEngine;

namespace TowerDefence.Gameplay
{
    public class TeamSpawnPoint : MonoBehaviour
    {
        [SerializeField] private TeamIdentifier _teamIdentifier;
        
        public TeamIdentifier TeamIdentifier => _teamIdentifier;
    }
}
