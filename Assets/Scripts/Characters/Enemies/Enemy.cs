using Characters.Base;
using Logic;
using UnityEngine;

namespace Characters.Enemies
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(PlayerFinder))]
    public class Enemy : Unit
    {
        [Header("Патрулирование")]
        [SerializeField]
        private Collection _targetPoints;

        [SerializeField, Min(1f)] private float _arrivalThreshold;

        private EnemyStateMachineFactory _stateMachineFactory;
        private EnemyStateMachine _stateMachine;

        protected override void Awake()
        {
            base.Awake();

            _stateMachineFactory = new EnemyStateMachineFactory(GetComponent<Mover>(), 
                _targetPoints, 
                _arrivalThreshold,
                GetComponent<PlayerFinder>());
            
            _stateMachine = _stateMachineFactory.Create();
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }

        private void Update()
        {
            _stateMachine.Update();
        }
    }
}