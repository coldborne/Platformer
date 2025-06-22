using System;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Collection _targetPoints;
        [SerializeField, Min(1f)] private float _arrivalThreshold;

        [SerializeField] private int _damage;
        [SerializeField] private int _attackCooldownTime;

        private Mover _mover;
        private Transform _targetPoint;

        private Timer _timer;

        private bool _canAttack;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _targetPoint = _targetPoints.Current;

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

        private void ResetAttackCooldown()
        {
            _canAttack = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player player) && _canAttack)
            {
                player.TakeDamage(_damage);
                _canAttack = false;
                StartCoroutine(_timer.DoCountdown(_attackCooldownTime));
            }
        }

        private void Update()
        {
            float sqrDistance = (transform.position - _targetPoint.position).sqrMagnitude;

            if (sqrDistance < _arrivalThreshold * _arrivalThreshold)
            {
                _targetPoint = _targetPoints.MoveNext();
            }

            MoveStep();
        }

        private void MoveStep()
        {
            if (_mover.IsRightOf(_targetPoint))
            {
                _mover.Move((float)Directions.Left);
            }
            else
            {
                _mover.Move((float)Directions.Right);
            }
        }
    }
}