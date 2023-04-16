using Infrastructure.Services;
using Input;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerAttacker : MonoBehaviour
    {
        [SerializeField] private AxeTriggers _axe;
        [SerializeField] private LayerMask _treeMask;
        [SerializeField] private Color _sphereColor = Color.white;
        [SerializeField] private float _offsetMultiplier;
        [SerializeField] private float _radius;
        [SerializeField] private float _maxCooldown;

        private float _cooldown;
        private PlayerAnimator _animator;
        private IInputService _inputService;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _animator = GetComponent<PlayerAnimator>();
            _cooldown = _maxCooldown;
        }

        private void Update()
        {
            _cooldown -= Time.deltaTime;
            if (!CoolDownIsUp() || !CanAttack())
                return;

            TryAttack();
        }

        private void TryAttack()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position + transform.forward * _offsetMultiplier, _radius, _treeMask);

            if (hits.Length != 0)
            {
                _cooldown = _maxCooldown;
                Attack();
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = _sphereColor;
            Gizmos.DrawSphere(transform.position + transform.forward * _offsetMultiplier, _radius);
        }

        private bool CoolDownIsUp() =>
            _cooldown <= 0f;

        private bool CanAttack() =>
            _inputService.GetAxis == Vector3.zero;

        private void Attack() =>
            _animator.PlayAttack();
        
        public void EnableAxe() =>
            _axe.CanAttack = true;

        public void DisableAxe() =>
            _axe.CanAttack = false;
    }
}