using TowerDefence.Core;
using UnityEngine;

namespace TowerDefence.Gameplay
{
    public class AttackTargetState : IState
    {
        private const float ATTACK_DISTANCE = 1.5f;
        
        private readonly IStateMachine _stateMachine;
        private readonly Character _character;
        private readonly PatrolPointsHolder _patrolPointsHolder;
        private readonly TargetTracker _targetTracker;

        public AttackTargetState(
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
                var distanceToTarget = Vector3.Distance(_character.transform.position, _targetTracker.TargetCharacter.transform.position);
                
                if (distanceToTarget <= ATTACK_DISTANCE)
                {
                    _character.DealDamage();
                }
                else
                {
                    _stateMachine.SetState(new MoveToTargetState(_stateMachine, _character, _patrolPointsHolder, _targetTracker));
                }
            }
            else
            {
                _stateMachine.SetState(new PatrolState(_stateMachine, _character, _patrolPointsHolder, _targetTracker));
            }
        }
    }
}