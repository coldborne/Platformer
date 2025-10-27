using System;
using Characters.Base;
using Characters.Players.Abilities;
using Input;
using Interfaces;
using Logic;
using UnityEngine;

namespace Characters.Players
{
    [DisallowMultipleComponent]
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
        [SerializeField] private float _vampirismAbilityRadius = 2.5f;
        [SerializeField] private LayerMask _enemiesMask;

        private Jumper _jumper;
        private ItemCollector _itemCollector;

        private VampirismAbility _vampirismAbility;
        private int _money;

        public event Action<float> AbilityTimeChanged;
        public event Action<float, float, bool> AbilityStarted;
        public event Action<float, float, bool> AbilityCooldownStarted;
        
        public event Action AbilityCooldownFinished;
        
        public float VampirismAbilityRadius => _vampirismAbilityRadius;

        protected override void Awake()
        {
            base.Awake();
            _jumper = GetComponent<Jumper>();
            _itemCollector = GetComponent<ItemCollector>();

            GameObject coroutineRunnerObject = CreateCoroutineRunnerObject();
            ICoroutine coroutineRunner = coroutineRunnerObject.GetComponent<CoroutineRunner>();

            _vampirismAbility = new VampirismAbility(
                Health,
                _perSecondDamage,
                _durationSeconds,
                _cooldownSeconds,
                VampirismAbilityRadius,
                _enemiesMask,
                transform,
                coroutineRunner);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _inputReader.MoveButtonPressed += Move;
            _inputReader.JumpButtonPressed += Jump;
            _inputReader.AbilityActivationPressed += TryActivateAbility;

            _itemCollector.CoinCollected += IncreaseMoney;

            _vampirismAbility.ValueChanged += GetAbilityTime;
            _vampirismAbility.Started += GetAbilityData;
            _vampirismAbility.CooldownStarted += GetAbilityCooldownData;
            _vampirismAbility.CooldownFinished += ReportAbilityCooldownFinished;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _inputReader.MoveButtonPressed -= Move;
            _inputReader.JumpButtonPressed -= Jump;
            _inputReader.AbilityActivationPressed -= TryActivateAbility;

            _itemCollector.CoinCollected -= IncreaseMoney;

            _vampirismAbility.ValueChanged -= GetAbilityTime;
            _vampirismAbility.Started -= GetAbilityData;
            _vampirismAbility.CooldownStarted -= GetAbilityCooldownData;
            _vampirismAbility.CooldownFinished -= ReportAbilityCooldownFinished;
        }

        private void GetAbilityData(float startedTime, float endTime, bool isAscending)
        {
            AbilityStarted?.Invoke(startedTime, endTime, isAscending);
        }

        private void GetAbilityCooldownData(float startedTime, float endTime, bool isAscending)
        {
            AbilityCooldownStarted?.Invoke(startedTime, endTime, isAscending);
        }

        private void GetAbilityTime(float time)
        {
            AbilityTimeChanged?.Invoke(time);
        }

        private void ReportAbilityCooldownFinished()
        {
            AbilityCooldownFinished?.Invoke();
        }

        private void Jump()
        {
            _jumper.Jump();
        }

        private void TryActivateAbility()
        {
            _vampirismAbility.Activate();
        }

        private void IncreaseMoney(int value)
        {
            _money += value;
        }

        private GameObject CreateCoroutineRunnerObject()
        {
            GameObject coroutineRunnerObject = new GameObject("CoroutineRunner");
            coroutineRunnerObject.AddComponent<CoroutineRunner>();

            return coroutineRunnerObject;
        }
    }
}