using UnityEngine;

namespace Enemies
{
    public class Enemy : Unit
    {
        [SerializeField] private Collection _targetPoints;
        [SerializeField, Min(1f)] private float _arrivalThreshold;

        private Transform _targetPoint;

        protected override void Awake()
        {
            base.Awake();
            _targetPoint = _targetPoints.Current;
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
            if (IsRightOf(_targetPoint))
            {
                Move((float)Directions.Left);
            }
            else
            {
                Move((float)Directions.Right);
            }
        }
    }
}