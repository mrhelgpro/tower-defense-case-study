using UnityEngine;

namespace TowerDefence.Gameplay
{
    public class BotCharacterController : MonoBehaviour
    {
        private const float REACH_DISTANCE = 1f;

        private Character _character;
        private PatrolPointsHolder _patrolPointsHolder;
        private Vector3 _targetPosition;
        private bool _isConstructed;
        
        public void Construct(Character character, PatrolPointsHolder patrolPointsHolder)
        {
            _character = character;
            _patrolPointsHolder = patrolPointsHolder;
            
            _patrolPointsHolder.OnPatrolPointChanged += OnPatrolPointChanged;
            _character.OnTeamChanged += OnTeamChanged;
            
            UpdatePatrolPoint();
            
            _isConstructed = true;
        }
        
        private void OnDestroy()
        {
            _patrolPointsHolder.OnPatrolPointChanged -= OnPatrolPointChanged;
            _character.OnTeamChanged -= OnTeamChanged;
        }
        
        private void Update()
        {
            if (_isConstructed)
            {
                var distanceToTarget = Vector3.Distance(transform.position, _targetPosition);
                if (distanceToTarget <= REACH_DISTANCE)
                {
                    _patrolPointsHolder.RefreshPatrolPoint(_character.TeamIdentifier);
                }
            }
        }
        
        private void UpdatePatrolPoint()
        {
            _targetPosition = _patrolPointsHolder.GetPatrolPoint(_character.TeamIdentifier);
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