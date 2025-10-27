using System;
using System.Collections;
using Characters.Base;
using Core;
using Interfaces;
using JetBrains.Annotations;
using UnityEngine;

namespace Characters.Players.Abilities
{
    public class VampirismAbility
    {
        private readonly Collider2D[] _overlapBuffer;
        private readonly float _perSecondDamage;
        private readonly float _radius;

        private readonly IHealth _ownerHealth;
        private readonly LayerMask _enemiesMask;
        private readonly ICoroutine _coroutineRunner;

        private readonly float _actionDuration;
        private readonly float _cooldownDuration;
        private float _elapsedTime;

        private Transform _origin;

        private Timer _timer;

        private bool _isActive;
        private bool _isOnCooldown;

        public event Action<float, float, bool> Started;
        public event Action<float, float, bool> CooldownStarted;
        public event Action CooldownFinished;
        public event Action<float> ValueChanged;

        public VampirismAbility(
            IHealth ownerHealth,
            float damagePerSecond,
            float actionDurationSeconds,
            float cooldownDurationSeconds,
            float radius,
            LayerMask enemiesMask,
            Transform origin,
            ICoroutine coroutineRunner)
        {
            int overlapBufferSize = 16;
            _overlapBuffer = new Collider2D[overlapBufferSize];

            _ownerHealth = ownerHealth;
            _perSecondDamage = damagePerSecond;
            _actionDuration = actionDurationSeconds;
            _cooldownDuration = cooldownDurationSeconds;
            _radius = radius;
            _enemiesMask = enemiesMask;
            _origin = origin;
            _timer = new Timer();
            _coroutineRunner = coroutineRunner;
        }

        public void Activate()
        {
            if (_isActive || _isOnCooldown)
            {
                return;
            }

            _coroutineRunner.StartCoroutine(Drain());
        }

        public IEnumerator Drain()
        {
            _isActive = true;
            _elapsedTime = 0;
            _coroutineRunner.StartCoroutine(_timer.DoCountdown(_actionDuration));

            Started?.Invoke(_elapsedTime, _actionDuration, true);

            while (_timer.RemainingTime > 0)
            {
                _elapsedTime = _actionDuration - _timer.RemainingTime;
                ValueChanged?.Invoke(_elapsedTime);
                IHealth targetHealth = FindNearestTarget();

                if (targetHealth != null)
                {
                    float damage = _perSecondDamage * Time.deltaTime;

                    targetHealth.TakeDamage(damage);
                    _ownerHealth.Treat(damage);
                }

                yield return null;
            }

            _elapsedTime = _actionDuration - _timer.RemainingTime;
            ValueChanged?.Invoke(_elapsedTime);

            _isActive = false;
            _isOnCooldown = true;

            _elapsedTime = 0;
            _coroutineRunner.StartCoroutine(_timer.DoCountdown(_cooldownDuration));

            CooldownStarted?.Invoke(_elapsedTime, _cooldownDuration, false);

            while (_timer.RemainingTime > 0)
            {
                _elapsedTime = _timer.RemainingTime;
                ValueChanged?.Invoke(_elapsedTime);

                yield return null;
            }

            _elapsedTime = _timer.RemainingTime;
            ValueChanged?.Invoke(_elapsedTime);

            _isOnCooldown = false;
            CooldownFinished?.Invoke();
        }

        [CanBeNull]
        private IHealth FindNearestTarget()
        {
            Vector2 center = _origin.position;

            int targetCount = Physics2D.OverlapCircleNonAlloc(center, _radius, _overlapBuffer, _enemiesMask);

            if (targetCount == 0)
            {
                return null;
            }

            IHealth target = null;
            float bestDistanceSqrMagnitude = float.PositiveInfinity;

            for (int i = 0; i < targetCount; i++)
            {
                Collider2D candidate = _overlapBuffer[i];

                if (candidate != null)
                {
                    IHealth candidateHealth = null;

                    if (candidate.TryGetComponent(out Unit unit))
                    {
                        candidateHealth = unit.Health;
                    }

                    float distanceSqrMagnitude = ((Vector2)candidate.transform.position - center).sqrMagnitude;

                    if (distanceSqrMagnitude < bestDistanceSqrMagnitude)
                    {
                        bestDistanceSqrMagnitude = distanceSqrMagnitude;
                        target = candidateHealth;
                    }
                }
            }

            return target;
        }
    }
}