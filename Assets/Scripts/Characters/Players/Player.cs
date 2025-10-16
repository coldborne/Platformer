using System.Collections;
using Characters.Base;
using Characters.Players.Abilities;
using Input;
using UnityEngine;

namespace Characters.Players
{
    [RequireComponent(typeof(Jumper))]
    [RequireComponent(typeof(ItemCollector))]
    public class Player : Unit
    {
        [SerializeField] private InputReader _inputReader;

        [Header("Параметры способности")]
        [SerializeField]
        private int _perSecondDamage = 24;

        [SerializeField] private float _durationSeconds = 6f;
        [SerializeField] private float _cooldownSeconds = 4f;

        [Header("Поиск целей")]
        [SerializeField]
        private float _radius = 2.5f;

        [SerializeField] private LayerMask _enemiesMask;

        private Jumper _jumper;
        private ItemCollector _itemCollector;

        private VampirismAbility _vampirismAbility;
        private int _money;

        private bool _isActive;
        private bool _isOnCooldown;
        private WaitForSeconds _waitCooldown;

        protected override void Awake()
        {
            base.Awake();
            _jumper = GetComponent<Jumper>();
            _itemCollector = GetComponent<ItemCollector>();

            _vampirismAbility = new VampirismAbility(
                Health,
                _perSecondDamage,
                _durationSeconds,
                _radius,
                _enemiesMask,
                transform);

            _waitCooldown = new WaitForSeconds(_cooldownSeconds);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _inputReader.MoveButtonPressed += Move;
            _inputReader.JumpButtonPressed += Jump;
            _inputReader.AbilityActivationPressed += TryActivateAbility;

            _itemCollector.CoinCollected += IncreaseMoney;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _inputReader.MoveButtonPressed -= Move;
            _inputReader.JumpButtonPressed -= Jump;
            _inputReader.AbilityActivationPressed -= TryActivateAbility;

            _itemCollector.CoinCollected -= IncreaseMoney;
        }

        private void Jump()
        {
            _jumper.Jump();
        }

        private void TryActivateAbility()
        {
            if (_isActive || _isOnCooldown)
            {
                return;
            }

            StartCoroutine(ActivateAbility());
        }

        private IEnumerator ActivateAbility()
        {
            _isActive = true;

            yield return StartCoroutine(_vampirismAbility.Drain());

            _isActive = false;

            _isOnCooldown = true;
            yield return _waitCooldown;
            _isOnCooldown = false;
        }

        private void IncreaseMoney(int value)
        {
            _money += value;
        }
    }
}