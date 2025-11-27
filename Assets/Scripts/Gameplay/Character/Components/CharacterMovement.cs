using UnityEngine;
using UnityEngine.AI;

namespace TowerDefence.Gameplay
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;

        private float _speed;
        
        public void Configure(float speed)
        {
            _speed = speed;
            _navMeshAgent.speed = speed;
        }
        
        public void MoveTo(Vector3 point)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.updatePosition = true;
            _navMeshAgent.updateRotation = true;
            _navMeshAgent.SetDestination(point);
        }
        
        public void MoveInDirection(Vector3 direction)
        {
            if (direction.magnitude > 0.01f)
            {
                _navMeshAgent.ResetPath();
                _navMeshAgent.updateRotation = true;
                _navMeshAgent.updatePosition = true;
                
                _navMeshAgent.velocity = direction.normalized * _speed;
            }
        }

        public void Stop()
        {
            _navMeshAgent.isStopped = true;
            _navMeshAgent.velocity = Vector3.zero;
        }
    }
}
