using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Gameplay
{
    public class TeamSpawnPointRepository : MonoBehaviour
    {
        [SerializeField] private float _randomRadius = 2;
        [SerializeField] private List<TeamSpawnPoint> _spawnPoints;
        
        private readonly Dictionary<TeamIdentifier, TeamSpawnPoint> _spawnPointLookup = new();

        public void Initialize()
        {
            foreach (var teamSpawnPoint in _spawnPoints)
            {
                _spawnPointLookup.Add(teamSpawnPoint.TeamIdentifier, teamSpawnPoint);
            }
        }

        public Vector3 GetSpawnPoint(TeamIdentifier teamIdentifier)
        {
            if (_spawnPointLookup.TryGetValue(teamIdentifier, out var spawnPoint))
            {
                var basePosition = spawnPoint.transform.position;
                var randomOffset = Random.insideUnitCircle * _randomRadius;
                return basePosition + new Vector3(randomOffset.x, 0, randomOffset.y);
            }
            
            return Vector3.zero;
        }
    }
}
