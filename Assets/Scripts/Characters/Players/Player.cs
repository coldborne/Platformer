using Characters.Base;
using Input;
using UnityEngine;

namespace Characters.Players
{
    [RequireComponent(typeof(Jumper))]
    [RequireComponent(typeof(ItemCollector))]
    public class Player : Unit
    {
        [SerializeField] private InputReader _inputReader;

        private Jumper _jumper;
        private ItemCollector _itemCollector;

        private int _money;

        protected override void Awake()
        {
            base.Awake();
            _jumper = GetComponent<Jumper>();
            _itemCollector = GetComponent<ItemCollector>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _inputReader.MoveButtonPressed += Move;
            _inputReader.JumpButtonPressed += Jump;

            _itemCollector.CoinCollected += IncreaseMoney;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _inputReader.MoveButtonPressed -= Move;
            _inputReader.JumpButtonPressed -= Jump;

            _itemCollector.CoinCollected -= IncreaseMoney;
        }

        private void Jump()
        {
            _jumper.Jump();
        }

        private void IncreaseMoney(int value)
        {
            _money += value;
        }
    }
}