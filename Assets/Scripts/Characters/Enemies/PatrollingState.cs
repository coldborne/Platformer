using Characters.Base;
using Interfaces;
using Logic;
using UnityEngine;

namespace Characters.Enemies
{
    public class PatrollingState : IEnemyState
    {
        private readonly Mover _mover;

        private readonly Collection _targetPoints;
        private readonly float _arrivalThreshold;

        private readonly EnemyStateMachine _stateMachine;
        private readonly PlayerFinder _playerFinder;

        private Transform _target;

        public PatrollingState(
            Mover mover,
            Collection targetPoints,
            float arrivalThreshold,
            EnemyStateMachine stateMachine, 
            PlayerFinder playerFinder)
        {
            _mover = mover;
            _targetPoints = targetPoints;
            _arrivalThreshold = arrivalThreshold;
            _stateMachine = stateMachine;
            _playerFinder = playerFinder;
        }

        public void Enter()
        {
            _target = _targetPoints.Current;
            _playerFinder.PlayerFound += ChangeToChasingState;
        }

        public void Exit()
        {
            _target = null;
            _playerFinder.PlayerFound -= ChangeToChasingState;
        }

        public void FixedUpdate()
        {
            
        }

        public void Update()
        {
            Patrol();
        }

        private void Patrol()
        {
            float sqrDistance = (_mover.transform.position - _target.position).sqrMagnitude;

            if (sqrDistance < _arrivalThreshold * _arrivalThreshold)
            {
                _target = _targetPoints.MoveNext();
            }

            _mover.MoveTowards(_target.position);
        }

        private void ChangeToChasingState()
        {
            _stateMachine.SetNext<ChasingState>();
        }
    }
}