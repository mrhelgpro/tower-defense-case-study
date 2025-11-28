using TowerDefence.Core;
using UnityEngine;

namespace TowerDefence.Gameplay
{
    public class MoveToTargetState : IState
    {
        private const float ATTACK_DISTANCE = 1f;
        
        private readonly IStateMachine _stateMachine;
        private readonly Character _character;
        private readonly PatrolPointsHolder _patrolPointsHolder;
        private readonly TargetTracker _targetTracker;

        public MoveToTargetState(
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
        }

        public void OnExit()
        {
        }

        public void Tick(float deltaTime)
        {
            if (_targetTracker.TargetCharacter)
            {
                var targetPosition = _targetTracker.TargetCharacter.transform.position;
                _character.MoveToPosition(targetPosition);
                
                var distanceToTarget = Vector3.Distance(_character.transform.position, targetPosition);
                if (distanceToTarget <= ATTACK_DISTANCE)
                {
                    _stateMachine.SetState(new AttackTargetState(_stateMachine, _character, _patrolPointsHolder, _targetTracker));
                }
            }
            else
            {
                _stateMachine.SetState(new PatrolState(_stateMachine, _character, _patrolPointsHolder, _targetTracker));
            }
        }
    }
}