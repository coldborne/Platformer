using System;
using System.Collections.Generic;
using Characters.Base;
using Interfaces;

namespace Characters.Enemies
{
    public sealed class EnemyStateMachine
    {
        private Dictionary<Type, IEnemyState> _states;
        private IEnemyState _currentState;

        public EnemyStateMachine(Mover mover,
            Collection targetPoints,
            float arrivalThreshold,
            PlayerFinder playerFinder)
        {
            _states = new Dictionary<Type, IEnemyState>();

            _states.Add(typeof(PatrollingState),
                new PatrollingState(mover, targetPoints, arrivalThreshold, this, playerFinder));
            _states.Add(typeof(ChasingState), 
                new ChasingState(mover, this, playerFinder));

            _currentState = SetNext<PatrollingState>();
        }

        public IEnemyState SetNext<T>() where T : IEnemyState
        {
            if (_states.TryGetValue(typeof(T), out var state) == false)
            {
                throw new Exception("Неверный тип для состояний врага");
            }
            
            return SetNext(state);
        }

        public void FixedUpdate() => _currentState?.FixedUpdate();
        public void Update() => _currentState?.Update();

        private IEnemyState SetNext(IEnemyState nextState)
        {
            _currentState?.Exit();
            _currentState = nextState;

            _currentState?.Enter();
            return _currentState;
        }
    }
}