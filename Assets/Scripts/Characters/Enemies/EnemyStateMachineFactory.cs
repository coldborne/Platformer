using Characters.Base;
using Logic;

namespace Characters.Enemies
{
    public class EnemyStateMachineFactory
    {
        private Mover _mover;
        private Collection _targetPoints;
        private float _arrivalThreshold;
        private PlayerFinder _playerFinder;
        
        public EnemyStateMachineFactory(Mover mover, Collection targetPoints, float arrivalThreshold, PlayerFinder playerFinder)
        {
            _mover = mover;
            _targetPoints = targetPoints;
            _arrivalThreshold = arrivalThreshold;
            _playerFinder = playerFinder;
        }

        public EnemyStateMachine Create()
        {
            return new EnemyStateMachine(_mover, _targetPoints, _arrivalThreshold, _playerFinder);
        }
    }
}