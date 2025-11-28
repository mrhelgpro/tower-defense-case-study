using UnityEngine;

namespace TowerDefence.Gameplay
{
    public class CharacterAnimator : MonoBehaviour
    {
        private static readonly int IS_MOVE = Animator.StringToHash("isMove");
        private static readonly int ATTACK = Animator.StringToHash("Attack");
        
        [SerializeField] private Animator _animator;
        
        public void PlayMove()
        {
            _animator.SetBool(IS_MOVE, true);
        }

        public void PlayIdle()
        {
            _animator.SetBool(IS_MOVE, false);
        }

        public void PlayAttack()
        {
            _animator.Play(ATTACK);
        }
    }
}
