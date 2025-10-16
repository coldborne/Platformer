using Core;
using Interfaces;
using UnityEngine;

namespace Characters.Base
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Attacker : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private int _attackCooldownTime;

        private Timer _timer;
        private bool _canAttack;

        private void Awake()
        {
            _timer = new Timer();
            ResetAttackCooldown();
        }

        private void OnEnable()
        {
            _timer.IsOver += ResetAttackCooldown;
        }

        private void OnDisable()
        {
            _timer.IsOver -= ResetAttackCooldown;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Attack(collision);
        }

        private void Attack(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamageable target) && _canAttack)
            {
                target.TakeDamage(_damage);
                _canAttack = false;
                StartCoroutine(_timer.DoCountdown(_attackCooldownTime));
            }
        }

        private void ResetAttackCooldown()
        {
            _canAttack = true;
        }
    }
}