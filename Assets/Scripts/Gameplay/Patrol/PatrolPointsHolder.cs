using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TowerDefence.Gameplay
{
    public class PatrolPointsHolder
    {
        public event Action<TeamIdentifier> OnPatrolPointChanged;
        
        private readonly List<Transform> _patrolPoints;
        private readonly List<TeamIdentifier> _teamIdentifiers;
        private readonly Dictionary<TeamIdentifier, Vector3> _patrolPointDictionary = new();

        public PatrolPointsHolder(List<Transform> patrolPoints, List<TeamIdentifier> teamIdentifiers)
        {
            _patrolPoints = patrolPoints;
            _teamIdentifiers = teamIdentifiers;
        }

        public void Initialize()
        {
            foreach (var teamIdentifier in _teamIdentifiers)
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
