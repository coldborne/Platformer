using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Mover))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Collection _targetPoints;
        [SerializeField, Min(1f)] private float _arrivalThreshold;
        
        private Mover _mover;
        private Transform _target;

        private void Awake()
        {
            _target = _targetPoints.Current;
            _mover = GetComponent<Mover>();
        }

        private void Update()
        {
            float sqrDistance = (transform.position - _target.position).sqrMagnitude;

            if (sqrDistance < _arrivalThreshold * _arrivalThreshold)
            {
                _target = _targetPoints.MoveNext();
            }

            MoveStep();
        }

        private void MoveStep()
        {
            if (_mover.IsRightOf(_target))
            {
                _mover.Move((float) Directions.Left);
            }
            else
            {
                _mover.Move((float) Directions.Right);
            }
        }
    }
}