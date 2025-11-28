using TowerDefence.Core;
using UnityEngine;

namespace TowerDefence.Gameplay
{
    public class BotCharacterController : MonoBehaviour
    {
        private TargetTracker _targetTracker;
        private StateMachine _stateMachine;
        private Character _character;
        private PatrolPointsHolder _patrolPointsHolder;
        private Vector3 _targetPosition;
        private bool _isConstructed;
        
        public void Construct(Character character, PatrolPointsHolder patrolPointsHolder)
        {
            _character = character;
            _patrolPointsHolder = patrolPointsHolder;
            
            _targetTracker  = new TargetTracker(_character);
            
            _stateMachine = new StateMachine();
            _stateMachine.SetState(new PatrolState(_stateMachine, _character, _patrolPointsHolder, _targetTracker));
            
            _isConstructed  = true;
        }
        
        private void Update()
        {
            if (_isConstructed)
            {
                _targetTracker.Tick();
                _stateMachine.Tick(Time.deltaTime);
            }
        }
    }
}