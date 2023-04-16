using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController), typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Attack = Animator.StringToHash("Attack");

        private Animator _animator;
        private CharacterController _characterController;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _animator.SetFloat(Speed, _characterController.velocity.sqrMagnitude);
        }
        
        public void PlayAttack() =>
            _animator.SetTrigger(Attack);
    }
}