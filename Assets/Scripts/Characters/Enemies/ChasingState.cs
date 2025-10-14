using Characters.Base;
using Interfaces;
using UnityEngine;

namespace Characters.Enemies
{
    public class ChasingState : IEnemyState
    {
        private readonly Mover _mover;
        private readonly EnemyStateMachine _stateMachine;
        private readonly PlayerFinder _playerFinder;

        private Vector2? _target;

        public ChasingState(Mover mover, EnemyStateMachine stateMachine, PlayerFinder playerFinder)
        {
            _mover = mover;
            _stateMachine = stateMachine;
            _playerFinder = playerFinder;
        }

        public void Enter()
        {
            _target = _playerFinder.PlayerPosition;

            _playerFinder.PlayerFound += GetPlayer;
            _playerFinder.PlayerLost += LosePlayer;
        }

        public void Exit()
        {
            _target = null;

            _playerFinder.PlayerFound -= GetPlayer;
            _playerFinder.PlayerLost -= LosePlayer;
        }

        public void FixedUpdate()
        {
        }

        public void Update()
        {
            _target = _playerFinder.PlayerPosition;
            
            if (_target != null)
            {
                _mover.MoveTowards(_target.Value);
            }
        }

        private void GetPlayer()
        {
            _target = _playerFinder.PlayerPosition;
        }

        private void LosePlayer()
        {
            _stateMachine.SetNext<PatrollingState>();
        }
    }
}