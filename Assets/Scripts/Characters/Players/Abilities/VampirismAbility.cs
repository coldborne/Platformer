using System.Collections;
using Characters.Base;
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

        private readonly float _actionDuration;

        private readonly IHealth _ownerHealth;
        private readonly LayerMask _enemiesMask;

        private Transform _origin;

        public VampirismAbility(
            IHealth ownerHealth,
            float damagePerSecond,
            float actionDurationSeconds,
            float radius,
            LayerMask enemiesMask,
            Transform origin)
        {
            int overlapBufferSize = 16;
            _overlapBuffer = new Collider2D[overlapBufferSize];

            _ownerHealth = ownerHealth;
            _perSecondDamage = damagePerSecond;
            _actionDuration = actionDurationSeconds;
            _radius = radius;
            _enemiesMask = enemiesMask;
            _origin = origin;
        }

        public IEnumerator Drain()
        {
            float elapsedTime = 0f;

            while (elapsedTime < _actionDuration)
            {
                elapsedTime += Time.deltaTime;
                IHealth targetHealth = FindNearestTarget();

                if (targetHealth != null)
                {
                    float damage = _perSecondDamage * Time.deltaTime;

                    targetHealth.TakeDamage(damage);
                    _ownerHealth.Treat(damage);
                }

                yield return null;
            }
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