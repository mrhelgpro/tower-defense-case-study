using UnityEngine;

namespace TowerDefence.Gameplay
{
    public class BotCharacterController : MonoBehaviour
    {
        [SerializeField] private Character _character;
        
        [Header("Movement Settings")]
        [SerializeField] private float _reachDistance = 1f;
        
        private Vector3 _targetPosition;
        private float _attackTimer;

        private void Start()
        {
            PatrolPointsHolder.INSTANCE.OnPatrolPointChanged += OnPatrolPointChanged;
            _character.OnTeamChanged += OnTeamChanged;
            
            UpdatePatrolPoint();
        }
        
        private void OnDestroy()
        {
            PatrolPointsHolder.INSTANCE.OnPatrolPointChanged -= OnPatrolPointChanged;
            _character.OnTeamChanged -= OnTeamChanged;
        }

        private void Update()
        {
            var distanceToTarget = Vector3.Distance(transform.position, _targetPosition);
            if (distanceToTarget <= _reachDistance)
            {
                PatrolPointsHolder.INSTANCE.RefreshPatrolPoint(_character.TeamIdentifier);
            }
        }
        
        private void UpdatePatrolPoint()
        {
            _targetPosition = PatrolPointsHolder.INSTANCE.GetPatrolPoint(_character.TeamIdentifier);
            _character.MoveToPosition(_targetPosition);
        }
        
        private void OnTeamChanged(Character character)
        {
            UpdatePatrolPoint();
        }
        
        private void OnPatrolPointChanged(TeamIdentifier teamIdentifier)
        {
            if (teamIdentifier == _character.TeamIdentifier)
            {
                UpdatePatrolPoint();
            }
        }
    }
}