using TowerDefence.Core;
using UnityEngine;

namespace TowerDefence.Gameplay
{
    public class PatrolState : IState
    {
        private const float REACH_DISTANCE = 1f;
        
        private readonly IStateMachine _stateMachine;
        private readonly Character _character;
        private readonly PatrolPointsHolder _patrolPointsHolder;
        private readonly TargetTracker _targetTracker;
        private Vector3 _targetPosition;

        public PatrolState(
            IStateMachine stateMachine, 
            Character character, 
            PatrolPointsHolder patrolPointsHolder,
            TargetTracker targetTracker)
        {
            _stateMachine = stateMachine;
            _character = character;
            _patrolPointsHolder = patrolPointsHolder;
            _targetTracker = targetTracker;
        }

        public void OnEnter()
        {
            _patrolPointsHolder.OnPatrolPointChanged += OnPatrolPointChanged;
            _character.OnTeamChanged += OnTeamChanged;
            
            UpdatePatrolPoint();
        }
        
        public void OnExit()
        {
            _patrolPointsHolder.OnPatrolPointChanged -= OnPatrolPointChanged;
            _character.OnTeamChanged -= OnTeamChanged;
        }
        
        public void Tick(float deltaTime)
        {
            if (_targetTracker.TargetCharacter)
            {
                _stateMachine.SetState(new MoveToTargetState(_stateMachine, _character, _patrolPointsHolder, _targetTracker));
                return;
            }

            var distanceToTarget = Vector3.Distance(_character.transform.position, _targetPosition);
            if (distanceToTarget <= REACH_DISTANCE)
            {
                _patrolPointsHolder.RefreshPatrolPoint(_character.TeamIdentifier);
            }
        }
        
        private void UpdatePatrolPoint()
        {
            _targetPosition = _patrolPointsHolder.GetPatrolPoint(_character.TeamIdentifier);
            _character.MoveToPosition(_targetPosition);
        }
        
        private void OnTeamChanged(TeamIdentifier previousTeamIdentifier, TeamIdentifier currentTeamIdentifier)
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
