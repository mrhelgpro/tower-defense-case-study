using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TowerDefence.Gameplay
{
    public class PatrolPointsHolder : MonoBehaviour
    {
        [SerializeField] private TeamConfig _teamConfig;
        [SerializeField] private List<Transform> _patrolPoints = new();

        public event Action<TeamIdentifier> OnPatrolPointChanged;
        
        private readonly Dictionary<TeamIdentifier, Vector3> _patrolPointDictionary = new();

        public static PatrolPointsHolder INSTANCE;
        
        private void Awake()
        {
            INSTANCE = this;

            Initialize();
        }

        public void Initialize()
        {
            foreach (var teamIdentifier in _teamConfig.TeamIdentifiers)
            {
                RefreshPatrolPoint(teamIdentifier);
            }
        }

        public void RefreshPatrolPoint(TeamIdentifier teamIdentifier)
        {
            var patrolTransform = _patrolPoints[Random.Range(0, _patrolPoints.Count)];
            var patrolPosition = patrolTransform.position;
            _patrolPointDictionary[teamIdentifier] = patrolPosition;
            
            OnPatrolPointChanged?.Invoke(teamIdentifier);
        }

        public Vector3 GetPatrolPoint(TeamIdentifier teamIdentifier)
        {
            return _patrolPointDictionary[teamIdentifier];
        }
    }
}
