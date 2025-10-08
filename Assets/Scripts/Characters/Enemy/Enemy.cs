using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class Enemy : Unit
    {
        private enum State
        {
            Patrolling,
            Chasing
        }

        [Header("Патрулирование")] [SerializeField]
        private Collection _targetPoints;

        [SerializeField, Min(1f)] private float _arrivalThreshold;

        [Header("Преследование")] [SerializeField]
        private float _loseTargetDelay;

        private Transform _targetPoint;

        private Transform _target;
        private Coroutine _losePlayerCoroutine;

        private WaitForSeconds _waitForSeconds;

        private State _state;

        protected override void Awake()
        {
            base.Awake();
            _targetPoint = _targetPoints.Current;
            _state = State.Patrolling;

            _waitForSeconds = new WaitForSeconds(_loseTargetDelay);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent<Player>(out _))
            {
                _target = collider.transform;
                _state = State.Chasing;

                if (_losePlayerCoroutine != null)
                {
                    StopCoroutine(_losePlayerCoroutine);
                    _losePlayerCoroutine = null;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.TryGetComponent<Player>(out _))
            {
                if (_losePlayerCoroutine != null)
                {
                    StopCoroutine(_losePlayerCoroutine);
                }

                _losePlayerCoroutine = StartCoroutine(LosePlayerCountDown());
            }
        }

        private void Update()
        {
            switch (_state)
            {
                case State.Patrolling:
                    Patrol();
                    break;

                case State.Chasing:
                    ChaseTarget();
                    break;
            }
        }

        private void Patrol()
        {
            float sqrDistance = (transform.position - _targetPoint.position).sqrMagnitude;

            if (sqrDistance < _arrivalThreshold * _arrivalThreshold)
            {
                _targetPoint = _targetPoints.MoveNext();
            }

            MoveTowards(_targetPoint);
        }

        private void MoveTowards(Transform target)
        {
            if (IsRightOf(target))
            {
                Move((float)Directions.Left);
            }
            else
            {
                Move((float)Directions.Right);
            }
        }

        private void ChaseTarget()
        {
            MoveTowards(_target);
        }

        private IEnumerator LosePlayerCountDown()
        {
            yield return _waitForSeconds;

            _target = null;
            _state = State.Patrolling;
            _losePlayerCoroutine = null;
        }
    }
}